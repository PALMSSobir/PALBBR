using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace PALBBR.Data
{
    public class LinenList : ViewModelBase
    {
        private string _id;
        private string _name;
        private string _qty;

        public string Id
        {
            get => _id;
            set => Set(ref _id, value);
        }
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
