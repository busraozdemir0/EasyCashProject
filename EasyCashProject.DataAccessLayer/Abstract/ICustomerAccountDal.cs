using EasyCashProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashProject.DataAccessLayer.Abstract
{
    public interface ICustomerAccountDal : IGenericDal<CustomerAccount>
    {
        List<CustomerAccount> GetCustomerAccountsList(int id); // Sisteme login olan kullanicinin tum hesaplarini listeleyecek
        List<CustomerAccount> GetCustomerAccountsListByMyCurrency(int userId, string myCurrency); // Gelen my currency (TL, USD, EUR) degerine ve sisteme giris yapan kullanicinin id'sine gore hesaplari listeleme
    }
}
