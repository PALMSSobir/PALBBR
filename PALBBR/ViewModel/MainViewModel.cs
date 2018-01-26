using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PALBBR.Data;


namespace PALBBR.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _customerNumber;
        private ObservableCollection<LinenTest> _lines;
        private ListBox _itemsListBox;
        private LinenTest _selectedItem;

        private OleDbConnection Conn;

        public string CustomerNumber
        {
            get => _customerNumber;
            set => Set(ref _customerNumber, value);
        }

        public LinenTest SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }
        public ListBox ItemsListBox
        {
            get => _itemsListBox;
            set => Set(ref _itemsListBox, value);
        }   
        public RelayCommand<object> NumberCommand { get; set; }
        public RelayCommand<Object> DeleteCommand { get; set; }
        public RelayCommand PrintCommand { get; set; }
        public RelayCommand Increase { get; set; }

        public ObservableCollection<LinenTest> Lines
        {
            get => _lines;
            set => Set(ref _lines, value);
        }
        public string checkConnection { get; set; }

        public MainViewModel()
        {
            //loadLinen();

            NumberCommand = new RelayCommand<object>(x => AddNumber(x));
            DeleteCommand = new RelayCommand<object>(x => DeleteNumber());

            List<LinenTest> items = new List<LinenTest>();
            items.Add(new LinenTest() { Name = "Pillow", Qty = "0"});
            items.Add(new LinenTest() { Name = "Bed Sheet", Qty = "4" });
            items.Add(new LinenTest() { Name = "Shirt", Qty = "0" });
            items.Add(new LinenTest() { Name = "Trouser", Qty = "0" });
            items.Add(new LinenTest() { Name = "Towel (small)", Qty = "0" });
            items.Add(new LinenTest() { Name = "Towel (big)", Qty = "1" });
            items.Add(new LinenTest() { Name = "Blanket", Qty = "0" });
            items.Add(new LinenTest() { Name = "Socks (2 pair)", Qty = "0" });
            items.Add(new LinenTest() { Name = "Underwear (2 Nos)", Qty = "3" });

            Lines = new ObservableCollection<LinenTest>(items);


            Conn = new OleDbConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["Conection"].ToString();
            Conn.Open();
            checkConnection = "est kontakt";
            Conn.Close();
        }
        private void AddNumber(object x)
        {
            if(SelectedItem != null)
            {
                SelectedItem.Qty = $"{SelectedItem.Qty}{x}";
            }
            else
            {
                if (x == null) return;
                CustomerNumber = $"{CustomerNumber}{x}";
            }
        }

        private void DeleteNumber()
        {
            if (SelectedItem != null)
            {
                SelectedItem.Qty = null;
            }
            else
            {
                if (CustomerNumber == null) return;
                CustomerNumber = null;
            }
        }
    }
}