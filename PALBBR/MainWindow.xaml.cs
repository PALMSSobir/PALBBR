using System.Configuration;
using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PALBBR

{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox == null || !textbox.IsFocused) return;

            ItemsListBox.SelectedItem = null;
        }


    }
}
