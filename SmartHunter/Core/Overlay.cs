using SmartHunter.Core.Helpers;
using SmartHunter.Core.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace SmartHunter.Core
{
    public abstract class Overlay
    {
        KeyboardInput m_KeyboardInput;

        Window m_MainWindow;
        protected WidgetWindow[] WidgetWindows { get; private set; }

        protected virtual bool ShowWindows { get { return false; } }

        public Overlay(Window mainWindow, params WidgetWindow[] widgetWindows)
        {
            m_MainWindow = mainWindow;
            WidgetWindows = widgetWindows;

            m_MainWindow.Loaded += MainWindow_Loaded;
            m_MainWindow.Closing += MainWindow_Closing;

            m_KeyboardInput = new KeyboardInput();
            m_KeyboardInput.InputReceived += KeyboardInput_InputReceived;

            m_MainWindow.Show();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateWidgetsFromConfig();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var widgetWindow in WidgetWindows)
            {
                widgetWindow.Close();
            }
        }

        private void ToggleWidgetWindow(WidgetWindow widgetWindow)
        {
            if (widgetWindow.Visibility != Visibility.Visible)
            {
                widgetWindow.Owner = m_MainWindow;
                widgetWindow.Show();
                widgetWindow.Owner = null;

                if (!ShowWindows)
                {
                    WindowHelper.SetTopMostTransparent(widgetWindow);
                }
            }
            else
            {
                widgetWindow.Hide();
            }
        }

        public void UpdateWidgetsFromConfig()
        {
            for (int index = 0; index < WidgetWindows.Length; ++index)
            {
                var widgetWindow = WidgetWindows[index];

                // Recreate the window using the appropriate settings if the ShowWindows option is toggled
                bool shouldChangeWindowState = (ShowWindows && widgetWindow.IsConfiguredForLayered) || (!ShowWindows && !widgetWindow.IsConfiguredForLayered);
                if (shouldChangeWindowState)
                {
                    widgetWindow.Close();

                    widgetWindow = Activator.CreateInstance(widgetWindow.GetType(), false) as WidgetWindow;
                    WidgetWindows[index] = widgetWindow;

                    if (ShowWindows)
                    {
                        widgetWindow.ConfigureForSolid();
                    }
                    else
                    {
                        widgetWindow.ConfigureForLayered();
                    }
                }

                widgetWindow.Widget.UpdateFromConfig();

                if ((widgetWindow.Widget.IsVisible && widgetWindow.Visibility != Visibility.Visible) ||
                    (!widgetWindow.Widget.IsVisible && widgetWindow.Visibility == Visibility.Visible))
                {
                    ToggleWidgetWindow(widgetWindow);
                }
            }
        }

        public void RefreshWidgetsLayout()
        {
            foreach (var widgetWindow in WidgetWindows)
            {
                var dataContext = widgetWindow.DataContext;
                widgetWindow.DataContext = null;
                widgetWindow.DataContext = dataContext;
            }
        }

        private void KeyboardInput_InputReceived(object sender, KeyboardInputEventArgs e)
        {
            InputReceived(e.Key, e.IsDown);
        }

        abstract protected void InputReceived(Key key, bool down);
    }
}