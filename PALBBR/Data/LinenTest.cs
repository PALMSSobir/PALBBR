using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace PALBBR.Data
{
    public class LinenTest : ViewModelBase
    {
        private string _name;
        private int _qty;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public int Qty
        {
            get => _qty;
            set => Set(ref _qty, value);
        }   
    }
}
