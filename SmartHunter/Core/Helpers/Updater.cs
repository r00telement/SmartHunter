using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Core.Helpers
{
    public class Updater
    {
        private readonly List<UpdateNode> _needUpdates = new List<UpdateNode>();
        private readonly string _apiEndpoint = "https://api.github.com/repos/gabrielefilipp/SmartHunter/commits?path=";
        private readonly string _apiRaw = "https://github.com/gabrielefilipp/SmartHunter/raw";
        private readonly string _dummyUserAgent = "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        public bool CheckForUpdates(bool forceCheck = false)
        {
            if (_needUpdates.Count == 0 || forceCheck == true)
            {
                try
                {
                    using var client = new WebClient();
                    using var stream = client.OpenRead("http://google.com/generate_204");// check connection (maybe pointless?)
                    var branch = "master";
                    var files = new string[5] { "SmartHunter/Game/Config/MemoryConfig.cs", "SmartHunter/Game/Config/PlayerDataConfig.cs", "SmartHunter/Game/Config/MonsterDataConfig.cs", "SmartHunter/Game/Config/LocalizationConfig.cs", "SmartHunter/bin/x64/Debug/SmartHunter.exe" };
                    foreach (var file in files)
                    {
                        var apiUrl = $"{_apiEndpoint}{file}";
                        var name = Path.GetFileName(file);
                        var nameWithNoExtension = Path.GetFileNameWithoutExtension(name);

                        client.Dispose();
                        client.Headers["User-Agent"] = _dummyUserAgent;

                        var apiResponseStr = client.DownloadString(apiUrl);
                        var json = JArray.Parse(apiResponseStr);
                        if (json.Count > 0)
                        {
                            var lastCommit = (JObject)json[0];
                            var hash = (string)lastCommit["sha"];
                            if (!ConfigHelper.Versions.Values.GetType().GetField(nameWithNoExtension).GetValue(ConfigHelper.Versions.Values).Equals(hash))
                            {
                                Log.WriteLine($"Found a new version of '{name}'");
                                _needUpdates.Add(new UpdateNode(hash, name, $"{_apiRaw}/{branch}/{file}"));
                            }
                        }
                    }
                }
                catch
                {
                    Log.WriteLine($"An error has occured while searching for updates... Resuming the normal flow of the application!");
                    return false;
                }
            }
            return _needUpdates.Count > 0;
        }

        public bool DownloadUpdates()
        {
            try
            {
                using var client = new WebClient();
                while (_needUpdates.Count > 0)
                {
                    var node = _needUpdates.First();
                    var hash = node.Hash;
                    var name = node.FileName;
                    var url = node.DownloadUrl;
                    var nameWithNoExtension = Path.GetFileNameWithoutExtension(name);
                    Log.WriteLine($"Downloading file '{name}'");
                    client.Dispose();
                    client.Headers["User-Agent"] = _dummyUserAgent;
                    if (Path.GetExtension(name).Equals(".exe"))
                    {
                        client.DownloadFile(url, $"{nameWithNoExtension}_{hash}.exe");
                    }
                    else
                    {
                        client.DownloadFile(url, name);
                    }
                    ConfigHelper.Versions.Values.GetType().GetField(nameWithNoExtension).SetValue(ConfigHelper.Versions.Values, hash);
                    ConfigHelper.Versions.Save();
                    _needUpdates.Remove(node);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private class UpdateNode
        {
            public string Hash { get; }
            public string FileName { get; }
            public string DownloadUrl { get; }

            public UpdateNode(string h, string name, string url)
            {
                Hash = h;
                FileName = name;
                DownloadUrl = url;
            }
        }
    }
}
