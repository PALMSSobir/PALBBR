using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PALBBR.Data;


namespace PALBBR.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _customerNumber = "0";
        private ObservableCollection<LinenList> _linens;
        private LinenList _selectedItem;
        private DeliveryNotes _deliveryNotes;
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
        public DeliveryNotes DeliveryNotes
        {
            get => _deliveryNotes;
            set => Set(ref _deliveryNotes, value);
        }

        public RelayCommand<object> NumberCommand { get; set; }
        public RelayCommand<Object> DeleteCommand { get; set; }
        public RelayCommand<LinenList> PrintCommand { get; set; }


        public MainViewModel()
        {
            NumberCommand = new RelayCommand<object>(x => AddNumber(x));
            DeleteCommand = new RelayCommand<object>(x => DeleteNumber());
            PrintCommand = new RelayCommand<LinenList>(x=> PrintBill());

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
                items.Add(new LinenList(){Id = item.Id, Name = item.Name, Qty = item.Qty});
            }

            Conn.Close();
            Linens = new ObservableCollection<LinenList>(items);
        }

        
        private void PrintBill()
        {
            Conn = new OleDbConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["Conection"].ToString();

            Conn.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = Conn;

            var newId = Guid.NewGuid();

            string query = "INSERT INTO DeliveryNotes (Client, PrintDate, DeliveryID) VALUES('" + CustomerNumber + "','" + DateTime.Now + "', '" + newId + "' )";
            command.CommandText = query;
            MessageBox.Show("Data Saved "+ CustomerNumber);
            command.ExecuteNonQuery();

            int qty;

            foreach (var item in Linens.Where(x => int.TryParse(x.Qty, out qty) && qty > 0))
            {

                string query2 = "Insert Into Transaction (ID, LinenId, Quantity) Values('77','88','99')";
                //string query2 = "Insert Into Transaction (ID, LinenId, Quantity) Values('" + newId + "','" + item.Id + "','" + item.Qty + "')";
                command.CommandText = query2;
            }
            command.ExecuteNonQuery();

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
// TODO:  ��� �� ����� ��������� �������� Print ���� ���� ������.��� ��� � ��������� � ������ �������. ����� ValidationContainer. �� ���� �� ���������� �� DevExpress �� ������ ������� �� ��������

//��� ����� ������ �������� ��������� � ������ AddNumber

//���� ��������� ����� ������ ��� ���� �� ������ �� ������

//����� ��� �������� � CanExecute � ����� ������ ����� �������������

//��� ��� ����������, ���������� ������ � �� ������� � ��������� ����� RepositoryOleDb, ���������������� ��� � ViewModelLocator

//������������� �� ����� ������������� ��������� IRepository

//Andrey, 1:51 PM
//������ ������ �� ��:

//���� ������

//var item = new LinenList();

//��������� � ���� while() { }

//�� �� ����� ����� ��������� ��� ���� ������ ��� ���������� � ���������

//������ �� ��� ��������� ��������, ��� � ������ ������ ���� ���� � �� �� ��������.

//�� ��������� ���� � ��� �� ������ � ��������� � ��� ���� ��� ������ �� �� ������ �������� �������, � ���������� � ��� � ������ ��� ���� ������� ���������������� n ��� �� ���������� ������� �� ��������� ������ � ��

//������ � �� ����� �������� � ���� try catch finaly

//� finaly ��������� ����������

//��� �� ����� ���������� ��� ��� ������

//https://stackoverflow.com/questions/12081111/getting-values-back-from-oledbdatareader-reading-from-access-database

//Andrey, 1:56 PM
//using �����������, ��� ����� ������ ����� Dispose, � ����� ������ �� �� ���������� � ����� ���� ������ ������, ���������� ����� ������ ��� ������ � ������

//using ����� ��� reader � ��� connection.��� connection ����� try catch finaly, using ����� �� �����, � ����� finaly ������� conn.Close()
    //