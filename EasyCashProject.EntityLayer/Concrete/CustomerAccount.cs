using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashProject.EntityLayer.Concrete
{
    // Musterinin hesap bilgilerini icerecek tablo
    public class CustomerAccount
    {
        public int CustomerAccountID { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountCurrency { get; set; } // Hesabin Döviz bilgisi
        public decimal CustomerAccountBalance { get; set; } // Hesap bakiyesi
        public string BankBranch { get; set; }  // Banka subesi
        public int AppUserID { get; set; } // Bu hesap kime ait?
        public AppUser AppUser { get; set; }

        // Para transferi icin iliskiler
        public List<CustomerAccountProcess> CustomerSender { get; set; }
        public List<CustomerAccountProcess> CustomerReceiver { get; set; }
    }
}
