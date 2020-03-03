using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Core.Helpers
{
    public sealed class ServerManager
    {
        private static readonly string apiEndpoint = "https://peppatime.altervista.org/smarthunter.php";

        public enum Command
        {
            ALIVE,
            HELLO,
            DONE,
            CHECK,
            PUSH,
            PULL
        }

        public int IsServerOline = 0;

        private string Version
        {
            get
            {
                return "1.0";
            }
        }
        private static string commandToStr(Command cmd)
        {
            if (cmd == Command.ALIVE)
            {
                return "alive";
            }
            else if (cmd == Command.HELLO)
            {
                return "hello";
            }else if (cmd == Command.DONE)
            {
                return "done";
            }
            else if (cmd == Command.CHECK)
            {
                return "check";
            }
            else if (cmd == Command.PUSH)
            {
                return "push";
            }
            else if (cmd == Command.PULL)
            {
                return "pull";
            }
            return "";
        }

        private static readonly ServerManager instance = new ServerManager();

        public static Dictionary<Command, long[]> Stats { get; set; }

        static ServerManager()
        {
            Stats = new Dictionary<Command, long[]>();
            foreach (var cmd in (Command[]) Enum.GetValues(typeof(Command)))
            {
                long[] s = new long[5];
                for (int i = 0; i < 5; i++)
                {
                    s[i] = 0;
                }
                Stats[cmd] = s;
            }
        }

        public void ResetStats()
        {
            foreach (var cmd in (Command[])Enum.GetValues(typeof(Command)))
            {
                for (int i = 0; i < 5; i++)
                {
                    Stats[cmd][i] = 0;
                }
            }
        }

        private string ConvertSize(long bytes)
        {
            string tmp = "byte";
            if (bytes >= 1024)
            {
                bytes /= 1024;
                tmp = "KB";
            }
            if (bytes >= 1024)
            {
                bytes /= 1024;
                tmp = "MB";
            }
            if (bytes >= 1024)
            {
                bytes /= 1024;
                tmp = "GB";
            }
            return $"{bytes} {tmp}";
        }

        public void PrintStats()
        {
            long total = 0;
            long failed = 0;
            long sent = 0;
            long received = 0;
            long ping = 0;
            foreach (var cmd in (Command[])Enum.GetValues(typeof(Command)))
            {
                total += Stats[cmd][0];
                failed += Stats[cmd][1];
                sent += Stats[cmd][2];
                received += Stats[cmd][3];
                ping += Stats[cmd][4];
                if (ConfigHelper.Main.Values.Debug.ShowServerLogs)
                {
                    Log.WriteLine($"Sent {Stats[cmd][0]} {commandToStr(cmd).ToUpper()}, failed {Stats[cmd][1]}, sent {ConvertSize(Stats[cmd][2])}, received {ConvertSize(Stats[cmd][3])} with an average response time of {Stats[cmd][4] / (Stats[cmd][0] > 0 ? Stats[cmd][0] : 1)} ms");
                }
            }
            Log.WriteLine($"Total network operations {total}, failed {failed}, sent {ConvertSize(sent)}, received {ConvertSize(received)} with average an response time of {ping / (total > 0 ? total : 1)} ms");
        }

        private ServerManager()
        {

        }

        public static ServerManager Instance
        {
            get
            {
                return instance;
            }
        }

        public async void RequestCommadWithHandler(Command cmd, string key, bool isHost, string data, Action<JObject, long> callback = null, Action<Exception> onError = null)
        {
            try
            {
                using var client = new HttpClient();
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                string command = commandToStr(cmd);
                parameters.Add("command", command);
                parameters.Add("key", key);
                parameters.Add("host", isHost ? "true" : "false");
                parameters.Add("data", data);
                parameters.Add("version", Version);

                var stringContent = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");

                Stats[cmd][0]++;
                Stats[cmd][2] += (long)stringContent.Headers.ContentLength;


                if (ConfigHelper.Main.Values.Debug.ShowServerLogs)
                {
                    if (cmd == Command.PUSH)
                    {
                        Log.WriteLine($"Sending {command.ToUpper()} with data of size {stringContent.Headers.ContentLength} byte");
                    }
                    else
                    {
                        Log.WriteLine($"Sending {command.ToUpper()} with parameters {await stringContent.ReadAsStringAsync()}");
                    }
                }

                Stopwatch stpw = new Stopwatch();
                stpw.Start();

                HttpResponseMessage response = await client.PostAsync(apiEndpoint, stringContent);

                stpw.Stop();
                Stats[cmd][4] += stpw.ElapsedMilliseconds;

                if (response.IsSuccessStatusCode)
                {
                    Stats[cmd][3] += (long)response.Content.Headers.ContentLength;
                    string r = await response.Content.ReadAsStringAsync();
                    if (ConfigHelper.Main.Values.Debug.ShowServerLogs)
                    {
                        if (cmd == Command.PULL)
                        {
                            Log.WriteLine($"Received {command.ToUpper()} with response of size {response.Content.Headers.ContentLength} byte");
                        }
                        else
                        {
                            Log.WriteLine($"Received {command.ToUpper()} with response {r}");
                        }
                    }
                    if (callback != null)
                    {
                        callback(JObject.Parse(r), stpw.ElapsedMilliseconds);
                    }
                }
                else
                {
                    Stats[cmd][1]++;
                    if (ConfigHelper.Main.Values.Debug.ShowServerLogs)
                    {
                        Log.WriteLine($"Received {command.ToUpper()} with error code {response.StatusCode}");
                    }
                    if (callback != null)
                    {
                        callback(null, 0);
                    }
                }
            }
            catch (Exception e)
            {
                Stats[cmd][1]++;
                if (ConfigHelper.Main.Values.Debug.ShowServerLogs)
                {
                    Log.WriteLine($"An error has occured while sending the request: {e.Message}");
                }
                onError(e);
            }
        }
    }
}
