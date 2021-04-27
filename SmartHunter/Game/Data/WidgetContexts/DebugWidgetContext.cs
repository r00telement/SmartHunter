using System;
using System.Security.Cryptography;
using System.Text;
using SmartHunter.Core;
using SmartHunter.Core.Data;
using SmartHunter.Core.Helpers;
using SmartHunter.Game.Helpers;

namespace SmartHunter.Game.Data.WidgetContexts
{
    public class DebugWidgetContext : WidgetContext
    {
        public Game CurrentGame { get; set; }

        private bool networkOperationDone = true;
        private int lastNetworkOperationTime = 0;
        private string OutdatedLobbyID = "";
        public void UpdateCurrentGame(string playerName, string weaponString, string sessionID, string sessionHostName, string lobbyID, string lobbyHostName, bool isExpedition)
        {
            bool wasHost = CurrentGame.IsCurrentPlayerLobbyHost();
            CurrentGame.CurrentPlayerName = playerName;
            CurrentGame.CurrentWeaponString = weaponString;
            CurrentGame.SessionID = sessionID;
            CurrentGame.SessionHostPlayerName = sessionHostName;
            CurrentGame.LobbyID = lobbyID;
            CurrentGame.LobbyHostPlayerName = lobbyHostName;
            CurrentGame.IsPlayerInExpedition = isExpedition;
            CurrentGame.IsValid = true;
            if (ConfigHelper.Main.Values.Overlay.MonsterWidget.UseNetworkServer && ServerManager.Instance.IsServerOline == 1)
            {
                if (CurrentGame.IsPlayerOnline() && CurrentGame.IsPlayerInLobby())
                {
                    if (CurrentGame.IsCurrentPlayerLobbyHost() && !wasHost)
                    {
                        ServerManager.Instance.RequestCommadWithHandler(ServerManager.Command.ELEVATE, CurrentGame.key, null, true, 0, null);
                    }
                    if (!CurrentGame.LobbyID.Equals(OutdatedLobbyID))
                    {
                        SHA1 sha = new SHA1CryptoServiceProvider();
                        CurrentGame.key = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(CurrentGame.SessionID + CurrentGame.LobbyID)));
                        if (CurrentGame.helloDone)
                        {
                            ServerManager.Instance.RequestCommadWithHandler(ServerManager.Command.DONE, Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(CurrentGame.SessionID + OutdatedLobbyID))), null, wasHost, 0, null, (result, ping) =>
                            {
                                if (result != null)
                                {
                                    if (!result["status"].ToString().Equals("error"))
                                    {
                                        Core.Log.WriteLine($"Left lobby with id '{result["result"]}'");
                                    }
                                    else if (result["status"].ToString().Equals("error"))
                                    {
                                        if (result["result"].ToString().Equals("v"))
                                        {
                                            ServerManager.Instance.IsServerOline = -1;
                                            Log.WriteLine("A new version is available, please update if you want to use the server!");
                                        }else if (result["result"].ToString().Equals("dev"))
                                        {
                                            ServerManager.Instance.IsServerOline = -1;
                                            Log.WriteLine("The server is under maintenance!");
                                        }
                                    }
                                }
                            });
                        }
                        CurrentGame.helloDone = false;
                        OutdatedLobbyID = CurrentGame.LobbyID;
                    }
                    if (!CurrentGame.helloDone && networkOperationDone && DateTime.Now.Second - (lastNetworkOperationTime > DateTime.Now.Second ? lastNetworkOperationTime - 60 : lastNetworkOperationTime) >= 5)
                    {
                        networkOperationDone = false;
                        ServerManager.Instance.RequestCommadWithHandler(ServerManager.Command.HELLO, CurrentGame.key, null, CurrentGame.IsCurrentPlayerLobbyHost(), 0, null, (result, ping) =>
                        {
                            if (result != null && result["status"].ToString().Equals("ok"))
                            {
                                if (CurrentGame.key.Equals(result["result"].ToString()))
                                {
                                    Log.WriteLine($"Joined lobby with id '{result["result"]}'");
                                    CurrentGame.helloDone = true;
                                }
                                else
                                {
                                    ServerManager.Instance.RequestCommadWithHandler(ServerManager.Command.DONE, result["result"].ToString(), null, wasHost, 0, null, (result, ping) =>
                                    {
                                        if (result != null && !result["status"].ToString().Equals("error"))
                                        {
                                            Log.WriteLine($"Left lobby with id '{result["result"]}'");
                                        }
                                    });
                                }
                            }
                            else
                            {
                                if (result != null && result["status"].ToString().Equals("error"))
                                {
                                    if (result["result"].ToString().Equals("v"))
                                    {
                                        ServerManager.Instance.IsServerOline = -1;
                                        Log.WriteLine("A new version is available, please update if you want to use the server!");
                                    }
                                    else if (result["result"].ToString().Equals("dev"))
                                    {
                                        ServerManager.Instance.IsServerOline = -1;
                                        Log.WriteLine("The server is under maintenance!");
                                    }
                                }
                                CurrentGame.helloDone = false;
                            }
                            networkOperationDone = true;
                            lastNetworkOperationTime = DateTime.Now.Second;
                        }, (error) =>
                        {
                            networkOperationDone = true;
                            lastNetworkOperationTime = DateTime.Now.Second;
                        });
                    }
                    else if (CurrentGame.helloDone && (!CurrentGame.checkDone || (CurrentGame.IsPlayerInExpedition && !CurrentGame.playersCheckDone)) && networkOperationDone && !(CurrentGame.IsCurrentPlayerLobbyHost() && CurrentGame.IsPlayerAlone()) && DateTime.Now.Second - (lastNetworkOperationTime > DateTime.Now.Second ? lastNetworkOperationTime - 60 : lastNetworkOperationTime) >= 5)
                    {
                        networkOperationDone = false;
                        ServerManager.Instance.RequestCommadWithHandler(ServerManager.Command.CHECK, CurrentGame.key, null, CurrentGame.IsCurrentPlayerLobbyHost(), 0, null, (result, ping) =>
                        {
                            if (result != null && result["status"].ToString().Equals("ok"))
                            {
                                if (CurrentGame.IsCurrentPlayerLobbyHost())
                                {
                                    CurrentGame.checkDone = result["result"]["players"].ToString().Equals("true");
                                }
                                else
                                {
                                    CurrentGame.checkDone = result["result"]["host"].ToString().Equals("true");
                                }
                                CurrentGame.playersCheckDone = result["result"]["players"].ToString().Equals("true");
                            }
                            else if (result != null && result["status"].ToString().Equals("error"))
                            {
                                if (result["result"].ToString().Equals("0"))
                                {
                                    CurrentGame.helloDone = false;
                                    CurrentGame.checkDone = false;
                                }
                                else if (result["result"].ToString().Equals("v"))
                                {
                                    ServerManager.Instance.IsServerOline = -1;
                                    Log.WriteLine("A new version is available, please update if you want to use the server!");
                                }
                                else if (result["result"].ToString().Equals("dev"))
                                {
                                    ServerManager.Instance.IsServerOline = -1;
                                    Log.WriteLine("The server is under maintenance!");
                                }
                            }
                            else
                            {
                                CurrentGame.checkDone = false;
                            }
                            networkOperationDone = true;
                            lastNetworkOperationTime = DateTime.Now.Second;
                        }, (error) =>
                        {
                            CurrentGame.checkDone = false;
                            networkOperationDone = true;
                            lastNetworkOperationTime = DateTime.Now.Second;
                        });
                    }
                }
                else
                {
                    if (CurrentGame.helloDone)
                    {
                        ServerManager.Instance.RequestCommadWithHandler(ServerManager.Command.DONE, CurrentGame.key, null, wasHost, 0, null, (result, ping) =>
                        {
                            if (result != null && !result["status"].ToString().Equals("error"))
                            {
                                Log.WriteLine($"Left lobby with id '{result["result"]}'");
                            }else if (result != null && result["status"].ToString().Equals("error"))
                            {
                                if (result["result"].ToString().Equals("v"))
                                {
                                    ServerManager.Instance.IsServerOline = -1;
                                    Log.WriteLine("A new version is available, please update if you want to use the server!");
                                }
                                else if (result["result"].ToString().Equals("dev"))
                                {
                                    ServerManager.Instance.IsServerOline = -1;
                                    Log.WriteLine("The server is under maintenance!");
                                }
                            }
                            ServerManager.Instance.PrintStats();
                            ServerManager.Instance.ResetStats();
                        }, (error) =>
                        {
                            ServerManager.Instance.PrintStats();
                            ServerManager.Instance.ResetStats();
                        });
                    }
                    CurrentGame.key = "";
                    CurrentGame.helloDone = false;
                    CurrentGame.checkDone = false;
                    networkOperationDone = true;
                }
            }
        }
        public DebugWidgetContext()
        {
            CurrentGame = new Game();
        }

        public override void UpdateFromConfig()
        {
            base.UpdateFromConfig();
        }
    }
}
