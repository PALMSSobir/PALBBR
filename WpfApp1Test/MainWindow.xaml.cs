using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Printing;

namespace WpfApp1Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            PrintXtraReport();
        }

        private void PrintXtraReport()
        {
            var xtraReport = new XtraReport2();

            var window = new DocumentPreviewWindow { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            window.PreviewControl.DocumentSource = xtraReport;
            xtraReport.CreateDocument(true);
            window.ShowDialog();
        }
    }
}
