using Microsoft.AspNetCore.Identity;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Data
{
    public class MovieDataInitializer
    {
        private readonly MovieContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public MovieDataInitializer(MovieContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //seeding the database with movies, see DBContext         
                Customer customer = new Customer { Email = "web4@web4.com", FirstName = "web4", LastName = "web4" };
                _dbContext.Customers.Add(customer);
                await CreateUser(customer.Email, "1Gelukkig.geennetbeans");
                Customer student = new Customer { Email = "test@test.com", FirstName = "Student", LastName = "Hogent" };
                _dbContext.Customers.Add(student);
                student.AddFavoriteMovie(_dbContext.Movies.First());
                await CreateUser(student.Email, ".Password1");
                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
