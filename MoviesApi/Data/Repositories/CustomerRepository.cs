using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MovieContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(MovieContext dbContext)
        {
            _context = dbContext;
            _customers = dbContext.Customers;
        }

        public Customer GetBy(string email)
        {
            return _customers.Include(c => c.Favorites).ThenInclude(f => f.Movie).ThenInclude(r => r.Actors).SingleOrDefault(c => c.Email == email);
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }
        public void Update(Customer customer)
        {
            _context.Update(customer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
