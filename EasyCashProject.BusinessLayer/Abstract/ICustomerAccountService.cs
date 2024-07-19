﻿using EasyCashProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashProject.BusinessLayer.Abstract
{
    public interface ICustomerAccountService : IGenericService<CustomerAccount>
    {
        List<CustomerAccount> TGetCustomerAccountsList(int id); // Sisteme login olan kullanicinin tum hesaplarini listeleyecek
    }
}
