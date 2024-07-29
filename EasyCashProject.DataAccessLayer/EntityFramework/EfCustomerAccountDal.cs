using EasyCashProject.DataAccessLayer.Abstract;
using EasyCashProject.DataAccessLayer.Concrete;
using EasyCashProject.DataAccessLayer.Repositories;
using EasyCashProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashProject.DataAccessLayer.EntityFramework
{
    public class EfCustomerAccountDal : GenericRepository<CustomerAccount>, ICustomerAccountDal
    {
        public List<CustomerAccount> GetCustomerAccountsList(int id)
        {
            using var context = new Context();
            var values = context.CustomerAccounts.Where(x => x.AppUserID == id).ToList();
            return values;

        }

        public List<CustomerAccount> GetCustomerAccountsListByMyCurrency(int userId, string myCurrency)
        {
            using var context = new Context();
            var userMyCurrencyAccounts = context.CustomerAccounts
                .Where(x => x.AppUserID == userId && x.CustomerAccountCurrency == myCurrency).ToList();
            return userMyCurrencyAccounts;

        }
    }
}
