using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace PALBBR.Data
{
    public class LinenTest : ViewModelBase
    {
        private string _name;
        private string _qty;

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Qty
        {
            get => _qty;
            set => Set(ref _qty, value);
        }   
    }
}
