using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PALBBR.Data
{
    public class DeliveryNotes : ViewModelBase
    {
        private string _id;
        private string _client;
        private DateTime _date;
        private int _deliveryId;

        public string Id
        {
            get => _id;
            set => Set(ref _id, value);
        }
        public string Client
        {
            get => _client;
            set => Set(ref _client, value);
        }

        public DateTime Date
        {
            get => _date;
            set => Set(ref _date, value);
        }
        
        public int DeliveryId
        {
            get => _deliveryId;
            set => Set(ref _deliveryId, value);
        }   
    }
}
