using CarWorkshop.Domain.Entities;
using CarWorkshop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure.Seeders
{
    public class CarWorkshopSeeder
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopSeeder(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seeder()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.CarWorkshops.Any())
                {
                    var model = new Domain.Entities.CarWorkshop()
                    {
                        Name = "Servis Mazda ASO",
                        Description = "Service description",
                        ContactDetails = new()
                        {
                            City = "Olsztyn",
                            Street = "Szewska 2",
                            PostalCode = "10-288",
                            PhoneNumber = "+48674885997"
                        }
                    };
                    model.EncodeName();
                    _dbContext.CarWorkshops.Add(model);
                    await _dbContext.SaveChangesAsync();
                }
            }
    }
    }
    
}
