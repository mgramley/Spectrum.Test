using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spectrum.Test.Core.Providers
{
    public interface IFileProvider
    {
        /// <summary>
        /// Gets the string content of a file
        /// </summary>
        /// <param name="fileName">the name of the file to load</param>
        /// <returns></returns>
        Task<string> GetFile(string fileName);

        /// <summary>
        /// Gets an object from the file specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="optionalSettings"></param>
        /// <returns></returns>
        Task<T> GetFile<T>(string fileName, JsonSerializerSettings optionalSettings = null) where T : class;

        /// <summary>
        /// Writes the string contents provided to the file
        /// </summary>
        /// <param name="fileName">the name of the file to write to</param>
        /// <param name="fileContent">the content of the file</param>
        /// <returns></returns>
        Task WriteFile(string fileName, string fileContent);

        /// <summary>
        /// Writes an object to json at the specified file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="objectToSerialize"></param>
        /// <param name="optionalSettings"></param>
        /// <returns></returns>
        Task WriteFile<T>(string fileName, T objectToSerialize, JsonSerializerSettings optionalSettings = null) where T : class;
    }
}
