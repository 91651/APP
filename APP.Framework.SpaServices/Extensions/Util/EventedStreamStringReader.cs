using System;
using System.Text;

namespace APP.Framework.SpaServices.NodeServices.Util
{
    internal class EventedStreamStringReader : IDisposable
    {
        private EventedStreamReader _eventedStreamReader;
        private bool _isDisposed;
        private StringBuilder _stringBuilder = new StringBuilder();

        public EventedStreamStringReader(EventedStreamReader eventedStreamReader)
        {
            _eventedStreamReader = eventedStreamReader
                ?? throw new ArgumentNullException(nameof(eventedStreamReader));
            _eventedStreamReader.OnReceivedLine += OnReceivedLine;
        }

        public string ReadAsString() => _stringBuilder.ToString();

        private void OnReceivedLine(string line) => _stringBuilder.AppendLine(line);

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _eventedStreamReader.OnReceivedLine -= OnReceivedLine;
                _isDisposed = true;
            }
        }
    }
}
