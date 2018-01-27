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
        private ObservableCollection<LinenList> _linens;
        private LinenList _selectedItem;
        private OleDbConnection Conn;

        public string CustomerNumber
        {
            get => _customerNumber;
            set => Set(ref _customerNumber, value);
        }
        public LinenList SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        public ObservableCollection<LinenList> Linens
        {
            get => _linens;
            set => Set(ref _linens, value);
        }

        public RelayCommand<object> NumberCommand { get; set; }
        public RelayCommand<Object> DeleteCommand { get; set; }
        public RelayCommand PrintCommand { get; set; }


        public MainViewModel()
        {
            NumberCommand = new RelayCommand<object>(x => AddNumber(x));
            DeleteCommand = new RelayCommand<object>(x => DeleteNumber());

            List<LinenList> items = new List<LinenList>();

            Conn = new OleDbConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["Conection"].ToString();

            Conn.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = Conn;
            string query = "Select * From LinenList";
            command.CommandText = query;
            OleDbDataReader reader = command.ExecuteReader();

            var item = new LinenList();

            while (reader.Read())
            {
                item.Name = reader["LinenName"].ToString();
                item.Id = reader["ID"].ToString();
                item.Qty = "0";
                items.Add(item);
            }

            Conn.Close();
            Linens = new ObservableCollection<LinenList>(items);
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