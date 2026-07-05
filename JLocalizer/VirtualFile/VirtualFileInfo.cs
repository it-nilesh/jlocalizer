using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace JLocalizer.VirtualFile
{
    internal class VirtualFileInfo
    {
        private readonly VirtualFileReader _virtualFileReader;
        private string[] _fileNames;

        public VirtualFileInfo(Assembly assembly)
        {
            _virtualFileReader = new VirtualFileReader(assembly);
        }

        public VirtualFileInfo Get(params string[] extension)
        {
            if (extension.Length > 0)
            {
                _fileNames = _virtualFileReader
                    .GetFileNames()
                    .Where(x => extension.Any(y => x.EndsWith(y)))
                    .ToArray();
            }
            else
            {
                _fileNames = _virtualFileReader.GetFileNames();
            }

            return this;
        }

        public IReadOnlyDictionary<string, IJLocalizationStore> Read(string path, string extension, Func<string, Stream, IJLocalizationStore> streamFunc)
        {
            var localizedStringStore = new Dictionary<string, IJLocalizationStore>(StringComparer.OrdinalIgnoreCase);
            var suffix = "." + extension.TrimStart('.');

            for (int keyIndex = 0; keyIndex < _fileNames.Length; keyIndex++)
            {
                string fileName = _fileNames[keyIndex];
                if (IsLocalizationResource(fileName, path, suffix))
                {
                    string cultureName = Helper.GetCultureNameFromFile(fileName);
                    using (var stream = _virtualFileReader.Read(fileName))
                    {
                        localizedStringStore[cultureName] = streamFunc(cultureName, stream);
                    }
                }
            }

            return localizedStringStore;
        }

        private static bool IsLocalizationResource(string fileName, string path, string suffix)
        {
            return fileName.EndsWith(suffix, StringComparison.OrdinalIgnoreCase) &&
                   fileName.IndexOf(path, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
