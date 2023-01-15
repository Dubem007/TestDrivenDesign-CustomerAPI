using CloudCustomers.Logic.Models;

namespace CloudCustomers.UnitTests.Helpers
{
    static class UserFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {
                Id = 1,
                Name = "Jhon",
                Email = "some.email@example.com",
                Address = new Address
                {
                    Street = "Second Avenue",
                    City = "San Jose",
                    ZipCode = "50723"
                }
            },
            new User
            {
                Id = 2,
                Name = "Carlos",
                Email = "some.carlos@example.com",
                Address = new Address
                {
                    Street = "Second Avenue",
                    City = "San Jose",
                    ZipCode = "50723"
                }
            },
            new User
            {
                Id = 3,
                Name = "Maria",
                Email = "some.maria@example.com",
                Address = new Address
                {
                    Street = "Second Avenue",
                    City = "San Jose",
                    ZipCode = "50723"
                }
            }
        };
    }
}
