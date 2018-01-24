using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PALBBR.Data;


namespace PALBBR.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private string _customerNumber;
        private ObservableCollection<LinenTest> _lines;

        public string CustomerNumber
        {
            get => _customerNumber;
            set => Set(ref _customerNumber, value);
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

        public MainViewModel()
        {

            NumberCommand = new RelayCommand<object>(x => AddNumber(x));
            DeleteCommand = new RelayCommand<object>(x => DeleteNumber(x));
            Increase = new RelayCommand(IncreaseQty);

            List<LinenTest> items = new List<LinenTest>();
            items.Add(new LinenTest() { Name = "Pillow", Qty = 0});
            items.Add(new LinenTest() { Name = "Bed Sheet", Qty = 4 });
            items.Add(new LinenTest() { Name = "Shirt", Qty = 0 });
            items.Add(new LinenTest() { Name = "Trouser", Qty = 6 });
            items.Add(new LinenTest() { Name = "Towel (small)", Qty = 0 });
            items.Add(new LinenTest() { Name = "Towel (big)", Qty = 1 });
            items.Add(new LinenTest() { Name = "Blanket", Qty = 0 });
            items.Add(new LinenTest() { Name = "Socks (2 pair)", Qty = 0 });
            items.Add(new LinenTest() { Name = "Underwear (2 Nos)", Qty = 3 });

            Lines = new ObservableCollection<LinenTest>(items);
        }

        private void IncreaseQty()
        {
            
        }

        private void AddNumber(object x)
        {
            if (x == null) return;

            CustomerNumber = $"{CustomerNumber}{x}";
        }

        private void DeleteNumber(object x)
        {
            if (CustomerNumber == null) return;
            CustomerNumber = null;
        }
    }
}