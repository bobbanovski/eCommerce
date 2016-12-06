﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.DAL.Data;
using eCommerce.Model;

namespace eCommerce.DAL.Repositories
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(DataContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
