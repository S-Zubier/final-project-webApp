using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Category> Categories { get;  set; }
        public IRepository<Product> Products { get;  set; }
        public IRepository<Order> Orders { get;  set; }
        public IRepository<User> Users { get;  set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Categories = new Repository<Category>(_context);
            Products = new Repository<Product>(_context);
            Orders = new Repository<Order>(_context);
            Users = new Repository<User>(_context);
        }

        public void Save() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
