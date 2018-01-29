using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Printing;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PALBBR.Data;
using PALBBR.Reports;
using DevExpress.XtraReports.UI;


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
            //PrintCommand = new RelayCommand<LinenList>(x=> SaveBill());
            PrintCommand = new RelayCommand<LinenList>(x => Print());

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
                items.Add(new LinenList() {Id = item.Id, Name = item.Name, Qty = item.Qty});
            }

            Conn.Close();
            Linens = new ObservableCollection<LinenList>(items);
        }

        private void Print()
        {
            var guid = SaveBill();

            var id = GetBillId(guid.ToString());

            PrintXtraReport(id);
        }

        private string GetBillId(string guid)
        {
            var conn = new OleDbConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["Conection"].ToString();
            conn.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = conn;
            string query = $"Select * From DeliveryNotes Where DeliveryID = '{guid}'";
            command.CommandText = query;
            OleDbDataReader reader = command.ExecuteReader();
            reader.Read();

            return reader["ID"].ToString();
        }

        private void PrintXtraReport(string billNumber)
        {
            var report = new TestReport();
            report.Created = DateTime.Now;
            report.CustomerNumber = CustomerNumber;
            report.BillNumber = billNumber;

            int index = 0;
            int qty;
            foreach (var item in Linens.Where(x => int.TryParse(x.Qty, out qty) && qty > 0))
            {
                report.Items.Add(new TestReportItem
                {
                    Index = ++index,
                    Name = item.Name,
                    Quantity = int.Parse(item.Qty)
                });
            }

            var xtraReport = new TestXtraReport();
            xtraReport.Initialize(report);

            xtraReport.PrintingSystem.StartPrint += new DevExpress.XtraPrinting.PrintDocumentEventHandler(PrintingSystem_StartPrint);
            xtraReport.Print();

            return;

            var window = new DocumentPreviewWindow { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            window.PreviewControl.DocumentSource = xtraReport;
            xtraReport.CreateDocument(true);

            window.ShowDialog();
        }

        private void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
        {
            e.PrintDocument.PrinterSettings.Copies = 3;
        }

        private Guid SaveBill()
        {
            Conn = new OleDbConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["Conection"].ToString();

            Conn.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = Conn;

            var newId = Guid.NewGuid();

            string query = "INSERT INTO DeliveryNotes (Client, PrintDate, DeliveryID) VALUES('" + CustomerNumber + "','" + DateTime.Now + "', '" + newId + "' )";
            command.CommandText = query;
            //MessageBox.Show("Data Saved "+ CustomerNumber);
            var result = command.ExecuteScalar();
            Conn.Close();

            SaveItem(newId);

            return newId;
        }

        public void SaveItem(Guid newId)
        {

            Conn = new OleDbConnection();
            Conn.ConnectionString = ConfigurationManager.ConnectionStrings["Conection"].ToString();

            Conn.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = Conn;
            int qty;
            string a1 = "544";
            foreach (var item in Linens.Where(x => int.TryParse(x.Qty, out qty) && qty > 0))
            {

                string query2 = "Insert Into Transactions (DeliveryId, LinenId, Qty) Values('" + newId + "','" + item.Id + "','" + item.Qty + "')";
                //string query2 = "Insert Into Transaction (ID, LinenId, Quantity) Values('" + a1 + "','" + a1 + "','" + a1 + "')";
                //string query2 = "Insert Into Transaction (ID, LinenId, Quantity) Values('5','5','6')";
                //string query2 = "Insert Into DeliveryNotes (PrintDate, Client, DeliveryId) Values('" + DateTime.Now + "','" + CustomerNumber + "','" + item.Qty + "')";
                command.CommandText = query2;
                command.ExecuteNonQuery();
            }

            Conn.Close();
        }

        public void PrintReceipe()
        {
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
// TODO:  так же можно запретить нажимать Print если есть ошибки.Так как я показывал в другом проекте. через ValidationContainer. Но если Вы избавитесь от DevExpress то данный вариант не подойдет

//или можно просто добавить валидацию в методе AddNumber

//если результат будет больше чем макс то ничего не делать

//можно это добавить в CanExecute и тогда кнопки будут блокироваться

//как все заработает, попробуйте работу с БД вынести в отдельный класс RepositoryOleDb, зарегистрировать его в ViewModelLocator

//сответственно он будет реализовывать интерфейс IRepository

//Andrey, 1:51 PM
//чтение данных из БД:

//если строку

//var item = new LinenList();

//перенести в блок while() { }

//то не нужно будет создавать еще один объект при добавлении в коллекцию

//видимо вы так пофиксили проблему, что в каждой записи были одни и те же значения.

//вы добавляли один и тот же объект в коллекцию и при этом при чтении из бд меняли значения свойств, в результате у вас в списке был один элемент продублированный n раз со значениями свойств из последней записи в БД

//работу с БД нужно обернуть в блок try catch finaly

//а finaly закрывать коннектиан

//так же можно посмотреть как тут делают

//https://stackoverflow.com/questions/12081111/getting-values-back-from-oledbdatareader-reading-from-access-database

//Andrey, 1:56 PM
//using гарантирует, что будет вызван метод Dispose, в вашем случае он не вызывается и могут быть утечки памяти, приложение будет кушать все больше и больше

//using нужен для reader и для connection.для connection лучше try catch finaly, using тогда не нужен, в блоке finaly сделать conn.Close()
    //