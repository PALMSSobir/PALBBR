using System;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using Microsoft.Practices.ServiceLocation;

namespace PALBBR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            DispatcherUnhandledException += OnDispatcherUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2016WhiteName;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var message = e.Exception.Message;
            if (e.Exception.InnerException != null)
                message = $"{message}{Environment.NewLine}{e.Exception.InnerException.Message}";

            e.Handled = true;
        }
    }
}
