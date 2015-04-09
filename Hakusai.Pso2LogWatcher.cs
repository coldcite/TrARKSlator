using NLog;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Hakusai.Csv;
using Hakusai.IO;

namespace Hakusai.Pso2
{
    /// <summary>
    /// PSO2関連の名前空間
    /// </summary>
    /// <remarks>
    /// <para>PSO2のログフォルダを監視し、チャットログ1行分の更新をイベントとして受け取る
    /// 処理ができるクラスが入っています。例のような使い方です。</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// using (IPso2LogWatcherFactory factory = new Pso2LogWatcherFactory())
    /// using (IPso2LogWatcher watcher = factory.CreatePso2LogWatcher())
    /// {
    ///     // イベントにコールバックを登録
    ///     watcher.Pso2LogEvent += (sender, e) =>
    ///     {
    ///         // コンソールに"白菜: &lt;GUILD&gt; こんにちは"のような出力をしています
    ///         Console.WriteLine("{0}: &lt;{1}&gt; {2}", e.From, e.SendTo, e.Message);
    ///     };
    ///     watcher.Start(); // 監視開始
    ///     Console.WriteLine("Press \'q\' to quit.");
    ///     while (Console.Read() != 'q') ;
    ///     watcher.Stop(); // 監視終了
    /// }
    /// </code>
    /// </example>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    class NamespaceDoc
    {
    }

    /// <summary>
    /// PSO2のログ1行分のデータ
    /// </summary>
    /// <remarks>
    /// <para><see cref="Pso2LogWatcher.Pso2LogEvent"/>のイベント通知で引数に使用されるデータで
    /// PSO2のチャットログ1行分のデータが入っています。</para>
    /// <para>チャットログはC:\Users\ユーザー名\Documents\SEGA\PHANTASYSTARONLINE2\logなど
    /// (マイドキュメントから辿れる場所)にあるはずなので、細かい部分は自分で見て確かめると
    /// いいかと思います。</para>
    /// <para>チャットログファイルのフォーマットはTABで区切られた文字列が固定個数入った行が
    /// 並ぶテキストファイルで、俗にCSVとかTSVとか呼ばれる形式です。表データなのでExcelとかで
    /// 読むことができます。</para>
    /// </remarks>
    public class Pso2LogEventArgs : EventArgs
    {
        /// <summary>
        /// 1列目のデータ
        /// </summary>
        /// <remarks>きっと時間</remarks>
        public string Time;
        /// <summary>
        /// 2列目のデータ
        /// </summary>
        /// <remarks>きっとメッセージID</remarks>
        public string MessageID;
        /// <summary>
        /// 3列目のデータ
        /// </summary>
        /// <remarks>きっとパーティ(PARTY)/チーム(GUILD)/オープン(PUBLIC)/ウィスパー(REPLY)などチャットの種類</remarks>
        public string SendTo;
        /// <summary>
        /// 4列目のデータ
        /// </summary>
        /// <remarks>きっと送信者のID</remarks>
        public string FromID;
        /// <summary>
        /// 5列目のデータ
        /// </summary>
        /// <remarks>きっと送信者の名前。きっと3列目のデータでキャラとアカウントが変わる</remarks>
        public string From;
        /// <summary>
        /// 6列目のデータ
        /// </summary>
        /// <remarks>メッセージそのもの。オプション(/ci7とか)の整形はしていない。ロビアクや他人のオートワードとかも入る</remarks>
        public string Message;
    }

    /// <summary>
    /// PSO2のログの更新を監視し通知するインターフェース
    /// </summary>
    /// <remarks>
    /// <para>PSO2のログフォルダを監視し、チャットログに何か書き込まれるたびにイベントを通知することができるインターフェースです。
    /// 使い方は名前空間の説明(<see cref="Hakusai.Pso2"/>)につけた例を見れば分かると思います。</para>
    /// </remarks>
    public interface IPso2LogWatcher : IDisposable
    {
        /// <summary>
        /// ログ1行分の更新イベント
        /// </summary>
        /// <remarks>
        /// 通知が必要な人はStart()する前に+=してください。
        /// </remarks>
        event EventHandler<Pso2LogEventArgs> Pso2LogEvent;

        /// <summary>
        /// ログ更新の監視を開始する
        /// </summary>
        /// <remarks>
        /// 開始後制御は戻りますが、監視処理は走っていて、<see cref="Pso2LogEvent"/>イベントなどが発生します。
        /// 監視処理が必要なくなったら<see cref="Stop()"/>してください。
        /// 一度停止するともう同じオブジェクトでStart()することはできません。
        /// </remarks>
        void Start();

        /// <summary>
        /// ログ更新の監視を開始する(テスト用)
        /// </summary>
        /// <param name="dir">PSO2のログ監視ディレクトリ</param>
        /// <remarks>
        /// テストの都合で監視ディレクトリを変更できるようにしたメソッドです
        /// </remarks>
        void Start(String dir);

        /// <summary>
        /// ログ更新の監視を停止する
        /// </summary>
        /// <remarks>
        /// 監視処理が停止したら制御が戻る完全同期型のメソッドです。
        /// </remarks>
        void Stop();
    }

    /// <summary>
    /// PSO2のログの更新を監視し通知するIPso2LogWatcher実装クラス
    /// </summary>
    /// <remarks>
    /// <para>使い方については<see cref="Hakusai.Pso2"/>を参照してください</para>
    /// <para>このクラスではデバッグ/テスト目的でNLogによるログ出力をしています。アプリケーションでNLog設定ファイル、
    /// NLog.configを設置しておくことでログ出力が可能になります(なければどこにも出力しない)。プロジェクト内の
    /// NLog.configはサンプルです。インスタンスの生成には<see cref="Pso2LogWatcherFactory"/>を使ってください</para>
    /// </remarks>
    public class Pso2LogWatcher : IPso2LogWatcher, IDisposable
    {
        private static readonly string MY_DOCUMENT = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static readonly string PSO2_LOG_RELATIVE_DIR = Path.Combine(new string[] { "SEGA", "PHANTASYSTARONLINE2", "log" });
        private static readonly string PSO2_LOG_DIR = Path.Combine(MY_DOCUMENT, PSO2_LOG_RELATIVE_DIR);

        /// <summary>
        /// テスト用
        /// </summary>
        /// <remarks>
        /// PSO2ログディレクトリが入ってます。
        /// </remarks>
        public static String DefaultLogDir
        {
            get { return PSO2_LOG_DIR; }
        }

        private static readonly string LOG_FILE_PATTERN = "ChatLog*_*.txt";
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private FileSystemWatcher _watcher = null;
        private string _tailpath = null;
        private Stream _stream = null;
        private Task<int>[] _current = new Task<int>[] { null };

        /// <summary>
        /// ログ1行分の更新イベント
        /// </summary>
        /// <remarks>
        /// 通知が必要な人はStart()する前に+=してください。
        /// </remarks>
        public event EventHandler<Pso2LogEventArgs> Pso2LogEvent;

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPso2LogEvent(Pso2LogEventArgs e)
        {
            var handler = Pso2LogEvent;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// ログ更新の監視を開始する
        /// </summary>
        /// <remarks>
        /// ログの格納フォルダは自動的に決めて、そこのログファイルの作成と更新を監視する処理を開始します。
        /// 
        /// 開始後制御は戻りますが、監視処理は走っていて、<see cref="Pso2LogEvent"/>イベントなどが発生します。
        /// 監視処理が必要なくなったら<see cref="Stop()"/>してください。
        /// 一度停止するともう同じオブジェクトでStart()することはできません。
        /// </remarks>
        public void Start()
        {
            Start(DefaultLogDir);
        }

        /// <summary>
        /// ログ更新の監視を開始する(テスト用)
        /// </summary>
        /// <param name="dir">PSO2のログ監視ディレクトリ</param>
        /// <remarks>
        /// テストの都合で監視ディレクトリを変更できるようにしたメソッドです
        /// </remarks>
        public void Start(String dir)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(typeof(Pso2LogWatcher).FullName);
            }
            if (_watcher != null)
            {
                throw new InvalidOperationException("既に開始しています");
            }

            _logger.Info("ディレクトリ{0}でのファイル作成監視を開始します。", dir);
            _watcher = new FileSystemWatcher(dir, LOG_FILE_PATTERN);
            _watcher.Created += new FileSystemEventHandler(OnCreated);

            _watcher.EnableRaisingEvents = true;

            string[] files = Directory.GetFiles(dir, LOG_FILE_PATTERN);
            if (files.Length > 0)
            {
                string file = files.Last();
                if (file != null)
                {
                    PrintContent(file, true);
                }
            }
        }

        /// <summary>
        /// ログ更新の監視を停止する
        /// </summary>
        /// <remarks>
        /// 監視処理が停止したら制御が戻る完全同期型のメソッドです。
        /// </remarks>
        public void Stop()
        {
            Dispose();
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            _logger.Debug("OnCreated(): {0}", e.Name);
            PrintContent(e.FullPath);
        }

        private Task<int> _PrintContent(StreamReader sin)
        {
            return Task<int>.Factory.StartNew(() =>
            {
                int result = 1;
                Task t = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        _logger.Debug("CSVパース開始");
                        CsvParser.Parse(
                            sin,
                            (string[] columns) =>
                            {
                                if (columns.Length == 6)
                                {
                                    Pso2LogEventArgs args = new Pso2LogEventArgs();
                                    args.Time = columns[0];
                                    args.MessageID = columns[1];
                                    args.SendTo = columns[2];
                                    args.FromID = columns[3];
                                    args.From = columns[4];
                                    args.Message = columns[5];

                                    _logger.Debug("{0}: {1}", args.From, args.Message);

                                    OnPso2LogEvent(args);
                                }
                            },
                            '\t');
                        _logger.Debug("CSVパース完了");
                        result = 0;
                    }
                    catch (ObjectDisposedException ex)
                    {
                        // これが出てしまうのは仕方ないモデル
                        _logger.Debug(ex);
                    }
                    catch (IOException ex)
                    {
                        _logger.Error(ex);
                    }
                });
                t.Wait();
                return result;
            });
        }

        private Task PrintContent(string path, bool onlyAppend = false)
        {
            return Task.Factory.StartNew(() =>
            {
                _logger.Info("ファイル{0}の内容監視要求を受け付けました。", path);
                Task<int> haveToWait = null;
                bool samepath = false;
                lock (_current)
                {
                    if (_current[0] != null)
                    {
                        if (!_current[0].IsCompleted)
                        {
                            _logger.Debug("既存の処理が実行中です。");
                            if (path != _tailpath)
                            {
                                _logger.Debug("対象が違うので既存の処理に停止を要求しました。");
                                _stream.Close();
                            }
                            else
                            {
                                _logger.Debug("対象は同じようです。 => N/A");
                                samepath = true;
                            }
                        }
                        haveToWait = _current[0];
                    }
                }
                if (haveToWait != null)
                {
                    _logger.Debug("既存処理の終了を待ちます。");
                    haveToWait.Wait();
                    int result = haveToWait.Result;
                    _logger.Debug("既存処理の終了を確認しました。");
                    if (samepath)
                    {
                        if (result == 0)
                        {
                            _logger.Debug("既存処理は正常終了のようです。新しい要求は破棄します。");
                            return;
                        }
                        else
                        {
                            _logger.Debug("既存処理は異常終了のようです。");
                        }
                    }
                }

                StreamReader sin = null;
                try
                {
                    lock (_current)
                    {
                        _logger.Debug("対象ファイルパスの処理を開始する。");
                        _tailpath = path;
                        Stream ifs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite);
                        _stream = new TailFollowStream(ifs, onlyAppend);
                        sin = new StreamReader(_stream, Encoding.Unicode);
                        _current[0] = _PrintContent(sin);
                    }
                    _current[0].Wait();
                }
                catch (FileNotFoundException ex)
                {
                    _logger.Error(ex);
                }
                finally
                {
                    lock (_current)
                    {
                        _logger.Debug("対象ファイルパスの処理が完了しました。");
                        if (sin != null)
                            sin.Dispose();
                        _tailpath = null;
                        _current[0] = null;
                    }
                }
            });
        }

        /// <summary>
        /// IDisposableのDispose実装
        /// </summary>
        /// <remarks>
        /// usingなどが使えるようにするための実装で、<see cref="Stop()"/>の中身です。
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;

        /// <summary>
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_watcher != null)
                    {
                        _watcher.Dispose();
                    }
                    lock (_current)
                    {
                        if (_stream != null)
                        {
                            _stream.Close();
                            if (_current[0] != null)
                            {
                                _current[0].Wait();
                                _current[0] = null;
                            }
                            _stream = null;
                        }
                    }
                }
                // アンマネージリソースは特にないけどあればここで開放するらしい
                _watcher = null;
                _tailpath = null;

                _disposed = true;
            }
        }

        /// <summary>
        /// ファイナライザ
        /// </summary>
        ~Pso2LogWatcher()
        {
            Dispose(false);
        }
    }

    /// <summary>
    /// IPso2LogWatcher実装オブジェクトを生成するFactory interfaceです
    /// </summary>
    public interface IPso2LogWatcherFactory : IDisposable
    {
        /// <summary>
        /// IPso2LogWatcherを実装したクラスのインスタンスを生成して返します
        /// </summary>
        /// <returns> IPso2LogWatcherを実装したクラスのインスタンス</returns>
        IPso2LogWatcher CreatePso2LogWatcher();
    }

    /// <summary>
    /// Pso2LogWatcherFactoryのインスタンスを生成するIPso2LogWatcherFactory実装クラス
    /// </summary>
    /// <remarks>Pso2LogWatcherFactoryのnewではなく、なるべくこちらで生成してください</remarks>
    public class Pso2LogWatcherFactory : IPso2LogWatcherFactory
    {
        /// <summary>
        /// Pso2LogWatcherFactoryのインスタンスを生成し、IPso2LogWatcherオブジェクトとして返します
        /// </summary>
        /// <returns>生成されたオブジェクト</returns>
        public IPso2LogWatcher CreatePso2LogWatcher()
        {
            return new Pso2LogWatcher();
        }

        /// <summary>
        /// このクラスでは空の実装が入っているDispose()
        /// </summary>
        /// <remarks>真面目にDisposeを書いてないので派生クラスは作らない方が賢明です</remarks>
        public void Dispose()
        { }
    }
}
