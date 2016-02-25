using System;
using System.IO;
using System.Threading;

namespace Hakusai.IO
{
    /// <summary>
    /// IO関連の名前空間
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
    class NamespaceDoc
    {
    }

    /// <summary>
    /// tail -f(--follow)のような読み込み専用ストリーム
    /// </summary>
    /// <remarks>
    /// <para>入力ストリームがEOFに到達してもリードし続けるので外から能動的にCloseしない限り読み込み終わりません。
    /// </para>
    /// <para>本来は非同期メソッドを実装すべきですが、よく知らないので実装してません。
    /// .NETのベースクラスの実装はあるので、呼べて動くかもしれませんがテストはしてません。
    /// </para>
    /// </remarks>
    /// <example><code>
    /// using (Stream s =
    ///     new FileStream(
    ///         "sample.txt",
    ///         FileMode.Open,
    ///         FileAccess.Read,
    ///         FileShare.Delete | FileShare.ReadWrite
    ///     )
    /// )
    /// using (TailFollowStream tail = new TailFollowStream(s))
    /// using (TextReader reader = new StreamReader(tail))
    /// {
    ///     var job = Task.Run(() =>
    ///     {
    ///         String line;
    ///         while ((line = reader.ReadLine()) != null)
    ///         {
    ///             Console.WriteLine(line);
    ///         }
    ///     });
    ///     Console.WriteLine("Press \'q\' to quit.");
    ///     while (Console.Read() != 'q');
    ///     s.Close();
    ///     job.Wait();
    /// }
    /// </code></example>
    public class TailFollowStream : Stream
    {
        private TailFollowStream() { }

        private Stream _in = null;
        private readonly int _time = 500;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="s">入力ストリーム(シーク可能)</param>
        /// <param name="fromEnd">終端から読むか</param>
        public TailFollowStream(Stream s, bool fromEnd = false)
        {
            if ((s != null) && s.CanRead && s.CanSeek)
            {
                _in = s;
                if (fromEnd)
                {
                    _in.Seek(0, SeekOrigin.End);
                }
            }
            else
            {
                throw new ArgumentException("不適切なストリームが指定されました。");
            }
        }

        /// <summary>
        /// 書き込みはできません
        /// </summary>
        /// <param name="buffer">-</param>
        /// <param name="offset">-</param>
        /// <param name="count">-</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        private enum State
        {
            RunningDisposable,  // 実行中だがDispose可能(Read処理中ではない)
            Running,            // 実行中(でRead処理中)
            Stopping,           // 停止中
            Disposable,         // Dispose可能もしくはDisposed
        };

        class StateObj
        {
            public State Value = State.RunningDisposable;
        }
        private StateObj _state = new StateObj();

        /// <summary>
        /// 派生元の説明参照(<see cref="System.IO.Stream.Read"/>)
        /// </summary>
        /// <remarks>唯一の違いはEOFでも0を返さず何か読めるまで定期的に何度でもリトライするという点です。</remarks>
        /// <param name="buffer">派生元の説明参照(<see cref="System.IO.Stream.Read"/>)</param>
        /// <param name="offset">派生元の説明参照(<see cref="System.IO.Stream.Read"/>)</param>
        /// <param name="count">派生元の説明参照(<see cref="System.IO.Stream.Read"/>)</param>
        /// <returns>派生元の説明参照(<see cref="System.IO.Stream.Read"/>)</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            lock (_state)
            {
                if (_state.Value != State.RunningDisposable)
                {
                    throw new ObjectDisposedException(typeof(TailFollowStream).FullName);
                }
                _state.Value = State.Running;
            }
            int len = 0;
            try
            {
                long pos = _in.Position;
                do
                {
                    len = _in.Read(buffer, offset, count);
                    pos += len;
                    if (len == 0)
                    {
                        // EOFだったら最終位置にシークし直して規定時間wait
                        _in.Seek(pos, SeekOrigin.Begin);
                        lock (_state)
                        {
                            if (_state.Value == State.Running)
                            {
                                if (Monitor.Wait(_state, _time))
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                } while (len == 0);
            }
            catch (ObjectDisposedException)
            {
                // Read時にこれが出るのは仕方ないモデル
            }
            finally
            {
                lock (_state)
                {
                    if (_state.Value == State.Stopping)
                    {
                        _state.Value = State.Disposable;
                        Monitor.PulseAll(_state);
                    }
                    else if (_state.Value == State.Running)
                    {
                        _state.Value = State.RunningDisposable;
                        Monitor.PulseAll(_state);
                    }

                }
            }
            return len;
        }

        /// <summary>
        /// 書き込むことはできません
        /// </summary>
        /// <param name="value">-</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 書き込むことはできません
        /// </summary>
        public override void Flush()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// シークすることはできません
        /// </summary>
        /// <param name="offset">-</param>
        /// <param name="origin">-</param>
        /// <returns>-</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// シークすることはできません
        /// </summary>
        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// シークすることはできません
        /// </summary>
        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// 書き込めません
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// シークできません
        /// </summary>
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <summary>
        /// 読み込めます
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// 派生クラスではこれだけでいいはず
        /// しかし何度も呼ばれるのはなぜだ
        /// </summary>
        /// <param name="disposing">-</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    lock (_state)
                    {
                        if (_state.Value == State.Stopping || _state.Value == State.Disposable)
                        {
                            return;
                        }
                        else if (_state.Value == State.RunningDisposable)
                        {
                            _state.Value = State.Disposable;
                            Monitor.PulseAll(_state);
                        }
                        else
                        {
                            _state.Value = State.Stopping;
                            Monitor.PulseAll(_state);
                            do
                            {
                                Monitor.Wait(_state);
                            } while (_state.Value == State.Stopping);
                        }
                    }
                    _in.Dispose();
                    lock (_state)
                    {
                        _state.Value = State.Disposable;
                        Monitor.PulseAll(_state);
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }
}
