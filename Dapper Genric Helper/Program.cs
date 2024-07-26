using System;
using System.Linq;

namespace DapperExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Your connection string - SQL SERVER";
            var userHelper = new GenericDatabaseHelper<User>(connectionString);

            // Create the Users table
            userHelper.CreateTable();

            // Insert some users
            userHelper.Insert(new User { Name = "John Doe", Age = 30 });
            userHelper.Insert(new User { Name = "Jane Doe", Age = 25 });

            // Retrieve and display all users
            var users = userHelper.GetAll().ToList();
            Console.WriteLine("Users after insertion:");
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Age: {user.Age}");
            }

            // Update a user
            var userToUpdate = users.First();
            userToUpdate.Age = 35;
            userHelper.Update(userToUpdate);

            // Retrieve and display the updated user
            var updatedUser = userHelper.GetById(userToUpdate.Id);
            Console.WriteLine("\nUser after update:");
            Console.WriteLine($"Id: {updatedUser.Id}, Name: {updatedUser.Name}, Age: {updatedUser.Age}");

            // Delete a user
            userHelper.Delete(userToUpdate.Id);

            // Retrieve and display all users after deletion
            users = userHelper.GetAll().ToList();
            Console.WriteLine("\nUsers after deletion:");
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Age: {user.Age}");
            }
        }
    }
}
