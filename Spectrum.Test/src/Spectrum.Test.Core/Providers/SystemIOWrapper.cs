using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Test.Core.Providers
{
    [ExcludeFromCodeCoverage]
    public class SystemIOWrapper : ISystemIO
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public IStreamReader OpenText(string path)
        {
            var reader = File.OpenText(path);

            return new StreamReaderWrapper(reader);
        }

        public IStreamWriter CreateText(string path)
        {
            var writer = File.CreateText(path);
            return new StreamWriterWrapper(writer);
        }
    }

    [ExcludeFromCodeCoverage]
    public class StreamReaderWrapper : IStreamReader
    {
        private readonly StreamReader _streamReader;

        public StreamReaderWrapper(StreamReader streamReader)
        {
            _streamReader = streamReader ?? throw new ArgumentNullException(nameof(streamReader));
        }
        public void Dispose()
        {
            _streamReader.Dispose();
        }

        public async Task<string> ReadToEndAsync()
        {
            return await _streamReader.ReadToEndAsync();
        }
    }

    [ExcludeFromCodeCoverage]
    public class StreamWriterWrapper : IStreamWriter
    {
        private readonly StreamWriter _streamWriter;

        public StreamWriterWrapper(StreamWriter streamWriter)
        {
            _streamWriter = streamWriter ?? throw new ArgumentNullException(nameof(streamWriter));
        }
        public void Dispose()
        {
            _streamWriter.Dispose();
        }

        public async Task WriteAsync(string content)
        {
            await _streamWriter.WriteAsync(content);
        }
    }
}
