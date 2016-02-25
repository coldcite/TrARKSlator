using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hakusai.Csv
{
    /// <summary>
    /// CSV関連の名前空間
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    class NamespaceDoc
    {
    }

    /// <summary>
    /// 1レコードずつコールバックを返すことができる低機能なCSV Parser
    /// </summary>
    /// <remarks>
    /// <para>1レコードずつコールバックを返すことができる低機能なCSV Parserです。</para>
    /// <para>まずエラー処理がないので、不正なフォーマットを検出することなどができません。
    /// 一度読み込み始めると強制的に入力ファイルを閉じるなどしないと処理が戻ってきません。
    /// 必要最低限な機能しかありませんが低機能な分作りは単純です。
    /// </para>
    /// <list type="bullet">
    /// <item><description>フィールドセパレータFS(デリミタ)変更可能</description></item>
    /// <item><description>レコードセパレータRS変更可能</description></item>
    /// <item><description>エスケープ文字変更可能</description></item>
    /// <item><description>nullは表現できない</description></item>
    /// <item><description>不正な入力は続行できるよう適当に解釈されて進む</description></item>
    /// </list>
    /// <para>ただしRSはCRLFでもLFでも受け付けるような曖昧な形で定義されており(RS=RS1{0..1}RS2)
    /// 厳密なパースはできません。</para>
    /// <example>
    /// <code>
    /// using(TextReader reader = new StreamReader(File.Open("sample.csv", FileMode.Open)))
    /// {
    /// 	CsvParser.Parse(
    /// 		reader,
    /// 		(strings) =>
    /// 		{
    /// 			if (strings.Length >= 3)
    /// 			{
    /// 				Console.WriteLine("column[2]: {0}", strings[2]);
    /// 			}
    /// 		});
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    public class CsvParser
    {
        private enum ParseState
        {
            NormalChar,
            Quoted,
            Quoted2,
            RecordSeparator1
        };

        /// <summary>
        /// 指定されたテキストストリームを読み取ってCSVとしてパースします
        /// </summary>
        /// <remarks>
        /// <para>指定されたテキストストリームを指定されたフォーマットを前提に解析し、
        /// 1レコード抽出されたら指定されたコールバックをEOFまで随時呼び出します。
        /// なおレコードセパレータFSはRS1の次にRS2が並ぶかRS2が単独で出現したものとします。
        /// エスケープ文字でエスケープされると、解除されるまでFS/RS1/RS2は通常文字扱いと
        /// なります。エスケープされる間、連続するエスケープ文字は単独のエスケープ文字と
        /// して処理されます。エスケープの解除条件は単独のエスケープ文字出現です。
        /// </para>
        /// </remarks>
        /// <param name="sin">入力テキストストリーム</param>
        /// <param name="action">カラムデータが順に入った文字列配列を引数に1レコード分の入力がコールバックされるdelegate</param>
        /// <param name="FS">フィールドセパレータ文字</param>
        /// <param name="QUOTE">エスケープ/引用文字</param>
        /// <param name="RS1">レコードセパレータ文字1</param>
        /// <param name="RS2">レコードセパレータ文字2</param>
        public static void Parse(
            TextReader sin,
            Action<string[]> action,
            char FS = ',',
            char QUOTE = '\"',
            char RS1 = '\r',
            char RS2 = '\n')
        {
            StringBuilder sb = new StringBuilder(1024);
            ParseState state = ParseState.NormalChar;
            List<String> fields = new List<String>();
            char[] buff = new char[4096];
            int readLen;

            do
            {
                readLen = sin.Read(buff, 0, buff.Length);
                int pos = 0;
                do
                {
                    char ch = '\0';
                    if (readLen > pos)
                        ch = buff[pos++];

                    switch (state)
                    {
                        case ParseState.NormalChar:
                            if (ch == FS)
                            {
                                fields.Add(sb.ToString());
                                sb.Clear();
                            }
                            else if (ch == QUOTE)
                            {
                                state = ParseState.Quoted;
                            }
                            else if (ch == RS1)
                            {
                                state = ParseState.RecordSeparator1;
                            }
                            else if (ch == RS2)
                            {
                                fields.Add(sb.ToString());
                                sb.Clear();
                                string[] strings = fields.ToArray();
                                fields.Clear();
                                action(strings);
                            }
                            else if (readLen <= 0)
                            {
                                if (sb.Length > 0)
                                {
                                    fields.Add(sb.ToString());
                                    sb.Clear();
                                }
                                if (fields.Count > 0)
                                {
                                    string[] strings = fields.ToArray();
                                    fields.Clear();
                                    action(strings);
                                }
                            }
                            else
                            {
                                sb.Append(ch);
                            }
                            break;

                        case ParseState.RecordSeparator1:
                            if (ch == FS)
                            {
                                sb.Append(RS1);
                                fields.Add(sb.ToString());
                                sb.Clear();
                                state = ParseState.NormalChar;
                            }
                            else if (ch == QUOTE)
                            {
                                sb.Append(RS1);
                                state = ParseState.Quoted;
                            }
                            else if (ch == RS1)
                            {
                                sb.Append(RS1);
                            }
                            else if (ch == RS2)
                            {
                                fields.Add(sb.ToString());
                                sb.Clear();
                                string[] strings = fields.ToArray();
                                fields.Clear();
                                action(strings);
                                state = ParseState.NormalChar;
                            }
                            else if (readLen <= 0)
                            {
                                sb.Append(RS1);
                                fields.Add(sb.ToString());
                                sb.Clear();
                                string[] strings = fields.ToArray();
                                fields.Clear();
                                action(strings);
                            }
                            else
                            {
                                sb.Append(RS1);
                                sb.Append(ch);
                                state = ParseState.NormalChar;
                            }
                            break;

                        case ParseState.Quoted:
                            if (ch == QUOTE)
                            {
                                state = ParseState.Quoted2;
                            }
                            else if (readLen <= 0)
                            {
                                sb.Append(QUOTE);
                                fields.Add(sb.ToString());
                                sb.Clear();
                                string[] strings = fields.ToArray();
                                fields.Clear();
                                action(strings);
                            }
                            else
                            {
                                sb.Append(ch);
                            }
                            break;

                        case ParseState.Quoted2:
                            if (ch == FS)
                            {
                                fields.Add(sb.ToString());
                                sb.Clear();
                                state = ParseState.NormalChar;
                            }
                            else if (ch == QUOTE)
                            {
                                sb.Append(QUOTE);
                                state = ParseState.Quoted;
                            }
                            else if (ch == RS1)
                            {
                                state = ParseState.RecordSeparator1;
                            }
                            else if (ch == RS2)
                            {
                                fields.Add(sb.ToString());
                                sb.Clear();
                                string[] strings = fields.ToArray();
                                fields.Clear();
                                action(strings);
                                state = ParseState.NormalChar;
                            }
                            else if (readLen <= 0)
                            {
                                if (sb.Length > 0)
                                {
                                    fields.Add(sb.ToString());
                                    sb.Clear();
                                }
                                if (fields.Count > 0)
                                {
                                    string[] strings = fields.ToArray();
                                    fields.Clear();
                                    action(strings);
                                }
                            }
                            else
                            {
                                sb.Append(ch);
                                state = ParseState.NormalChar;
                            }
                            break;

                        //default:
                        //    throw new ApplicationException("ここには来ない");
                    }
                } while (readLen > pos);
            } while (readLen > 0);
        }
    }
}
