using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Xaml;
using SmartHunter.Core;
using SmartHunter.Game;
using SmartHunter.Game.Data.ViewModels;
using SmartHunter.Game.Helpers;
using SmartHunter.Ui.Windows;
using XamlReader = System.Windows.Markup.XamlReader;

namespace SmartHunter
{
    public partial class App : Application
    {
        MhwOverlay m_Overlay;
        FileContainer m_SkinFile;

        string m_LastSkinFileName;

        protected override void OnStartup(StartupEventArgs e)
        {
            //var culture = new System.Globalization.CultureInfo("es-ES");
            //System.Globalization.CultureInfo.CurrentCulture = culture;
            //System.Globalization.CultureInfo.CurrentUICulture = culture;

            // Initialize the console view model first thing so we can see any problems that may occur
            var consoleViewModel = ConsoleViewModel.Instance;

            Log.WriteLine($"Started {Assembly.GetExecutingAssembly().GetName().Version}");
            //Log.WriteLine($"Culture: {System.Globalization.CultureInfo.CurrentCulture.Name}");

            SetPerMonitorDpiAwareness();

            ConfigHelper.EnsureConfigs();
            ConfigHelper.Main.Loaded += Config_Loaded;

            m_SkinFile = new FileContainer(ConfigHelper.Main.Values.SkinFileName);
            m_SkinFile.Changed += (s1, e1) => { LoadSkin(); };
            LoadSkin();

            try
            {
                string[] files = Directory.GetFiles(".");
                foreach (string file in files)
                {
                    if (Path.GetExtension(file).Equals(".exe") && file.Contains("SmartHunter_"))
                    {
                        File.Delete(file);
                    }
                }
            }
            catch
            {
                
            }

            m_Overlay = new MhwOverlay(new ConsoleWindow(), new TeamWidgetWindow(), new MonsterWidgetWindow(), new PlayerWidgetWindow(), new DebugWidgetWindow());

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.WriteLine("Ended");
            base.OnExit(e);
        }

        private void Config_Loaded(object sender, EventArgs e)
        {
            if (m_LastSkinFileName != ConfigHelper.Main.Values.SkinFileName)
            {
                LoadSkin();
            }
        }

        void LoadSkin()
        {
            if (ConfigHelper.Main.Values.Debug.UseInternalSkin)
            {
                return;
            }

            var skinFileName = ConfigHelper.Main.Values.SkinFileName;
            if (!File.Exists(FileContainer.GetFullPathFileName(skinFileName)))
            {
                Log.WriteLine($"Failed to load skin file '{skinFileName}'");
                return;
            }

            m_SkinFile.TryChangeFileName(skinFileName);

            try
            {
                ResourceDictionary resourceDictionary = null;

                using (var streamReader = new StreamReader(m_SkinFile.FullPathFileName, Encoding.UTF8))
                {
                    var xmlReaderSettings = new XamlXmlReaderSettings
                    {
                        LocalAssembly = Assembly.GetExecutingAssembly()
                    };

                    using (var xamlReader = new XamlXmlReader(streamReader.BaseStream, XamlReader.GetWpfSchemaContext(), xmlReaderSettings))
                    {
                        resourceDictionary = XamlReader.Load(xamlReader) as ResourceDictionary;
                    }
                }

                if (resourceDictionary != null)
                {
                    Resources.MergedDictionaries.Clear();
                    Resources.MergedDictionaries.Add(resourceDictionary);

                    if (m_Overlay != null)
                    {
                        m_Overlay.RefreshWidgetsLayout();
                    }

                    Log.WriteLine($"{m_SkinFile.FileName} loaded");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

            m_LastSkinFileName = skinFileName;            
        }

        void SetPerMonitorDpiAwareness()
        {
            // Win 8.1 added support for per monitor dpi
            if (Environment.OSVersion.Version >= new Version(6, 3, 0))
            {
                // Win 10 creators update added support for per monitor v2
                if (Environment.OSVersion.Version >= new Version(10, 0, 15063))
                {
                    WindowsApi.SetProcessDpiAwarenessContext((int)WindowsApi.DPI_AWARENESS_CONTEXT.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
                }
                else
                {
                    WindowsApi.SetProcessDpiAwareness(WindowsApi.PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
                }
            }
            else
            {
                WindowsApi.SetProcessDPIAware();
            }
        }
    }
}
