using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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

            for (int keyIndex = 0; keyIndex < _fileNames.Length; keyIndex++)
            {
                string fileName = _fileNames[keyIndex];
                if (Regex.IsMatch(fileName, $@"^*{path}.*-*.({extension})") ||
                    Regex.IsMatch(fileName, $@"^*{path}.*.({extension})"))
                {
                    string caltureName = Helper.GetCultureNameFromFile(fileName);
                    localizedStringStore[caltureName] = streamFunc(caltureName, _virtualFileReader.Read(fileName));
                }
            }

            return localizedStringStore;
        }
    }
}
