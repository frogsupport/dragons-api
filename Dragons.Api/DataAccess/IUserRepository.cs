using Dragons.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Dragons.Api.DataAccess
{
    public interface IUserRepository
    {
        // Gets all dragons from the database and returns them
        Task<IEnumerable<User>> ListAllUsersAsync();

        // Gets the dragon specified by its dragon id
        Task<User> GetUserByUsernameAsync(string username);

        //// Inserts a single dragon into the database
        //Task<bool> InsertUserAsync(User user);

        //// Updates a dragon
        //Task<bool> UpdateUserAsync(User user);

        //// Deletes a dragon based on its dragon id
        //Task<bool> DeleteUserAsync(Guid userId);
    }
}
