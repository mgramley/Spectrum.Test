using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Spectrum.Test.Core.Providers
{
    public abstract class FileProvider : IFileProvider
    {
        private readonly ISystemIO _systemIO;
        protected abstract string LocalDirectoryPath { get; }

        protected FileProvider(ISystemIO systemIO)
        {
            _systemIO = systemIO ?? throw new ArgumentNullException(nameof(systemIO));
        }

        public async Task<string> GetFile(string fileName)
        {
            try
            {
                var filePath = Path.Combine(LocalDirectoryPath, fileName);

                if (!_systemIO.FileExists(filePath))
                    return null;

                string fileText;
                using (var reader = _systemIO.OpenText(filePath))
                {
                    fileText = await reader.ReadToEndAsync();
                }

                return fileText;
            }
            catch (Exception e)
            {
                //TODO: Insert logging
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<T> GetFile<T>(string fileName, JsonSerializerSettings optionalSettings = null) where T : class
        {
            try
            {
                var jsonContent = await GetFile(fileName);

                if (string.IsNullOrWhiteSpace(jsonContent))
                    return null;

                if (optionalSettings == null)
                {
                    optionalSettings = new JsonSerializerSettings();
                }

                return JsonConvert.DeserializeObject<T>(jsonContent, optionalSettings);
            }
            catch (Exception e)
            {
                //TODO: Insert logging
                Console.WriteLine(e);
                return null;
            }

        }

        public async Task WriteFile<T>(string fileName, T objectToSerialize, JsonSerializerSettings optionalSettings = null) where T : class
        {
            try
            {
                if (optionalSettings == null)
                {
                    optionalSettings = new JsonSerializerSettings();
                }

                var stringContentToWrite = JsonConvert.SerializeObject(objectToSerialize, optionalSettings);

                await WriteFile(fileName, stringContentToWrite);
            }
            // missing a test for this exception. Not sure how to force JsonConvert.SerializeObject to explode, and I'm feeling too lazy to wrap the class into an interface I can mock 
            catch (Exception e)
            {
                //TODO: Insert logging
                Console.WriteLine(e);
            }
        }

        public async Task WriteFile(string fileName, string fileContent)
        {
            try
            {
                var filePath = Path.Combine(LocalDirectoryPath, fileName);
                using (var writer = _systemIO.CreateText(filePath))
                {
                    await writer.WriteAsync(fileContent);
                }
            }
            catch (Exception e)
            {
                //TODO: Insert logging
                Console.WriteLine(e);
            }
        }
    }
}
