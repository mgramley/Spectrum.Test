using System;
using System.Threading.Tasks;

namespace Spectrum.Test.Core.Providers
{
    public interface ISystemIO
    {
        bool FileExists(string path);

        IStreamReader OpenText(string path);

        IStreamWriter CreateText(string path);
    }

    public interface IStreamReader : IDisposable
    {
        Task<string> ReadToEndAsync();
    }

    public interface IStreamWriter : IDisposable
    {
        Task WriteAsync(string content);
    }
}
