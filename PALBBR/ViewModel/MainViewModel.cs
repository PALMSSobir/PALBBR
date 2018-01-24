using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PALBBR.Data;


namespace PALBBR.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private int _customerNumber;

        public int CustomerNumber
        {
            get => _customerNumber;
            set => Set(ref _customerNumber, value);
        }
        public RelayCommand Add1Command { get; }

        public MainViewModel()
        {

            Add1Command = new RelayCommand(Add1);

            List<LinenTest> LinenList = new List<LinenTest>();
            LinenList.Add(new LinenTest() { Name = "Pillow", Qty = 0});
            LinenList.Add(new LinenTest() { Name = "Bed Sheet", Qty = 4 });
            LinenList.Add(new LinenTest() { Name = "Shirt", Qty = 0 });
            LinenList.Add(new LinenTest() { Name = "Trouser", Qty = 6 });
            LinenList.Add(new LinenTest() { Name = "Towel (small)", Qty = 0 });
            LinenList.Add(new LinenTest() { Name = "Towel (big)", Qty = 1 });
            LinenList.Add(new LinenTest() { Name = "Blanket", Qty = 0 });
            LinenList.Add(new LinenTest() { Name = "Socks (2 pair)", Qty = 0 });
            LinenList.Add(new LinenTest() { Name = "Underwear (2 Nos)", Qty = 3 });
        }


        private void Add1()
        {
            if (Qty > 0)
                Qty = Qty * 10 + 1;
            else
                Qty = 1;
        }

    }
}