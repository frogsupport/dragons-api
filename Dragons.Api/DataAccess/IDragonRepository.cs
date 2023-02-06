using Dragons.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dragons.Api.DataAccess
{
    public interface IDragonRepository
    {
        // Gets all dragons from the database and returns them
        Task<IEnumerable<Dragon>> ListAllDragonsAsync();

        // Gets the dragon specified by its dragon id
        Task<Dragon> GetDragonByIdAsync(Guid dragonId);

        // Inserts a single dragon into the database
        Task<bool> InsertDragonAsync(Dragon dragon);

        // Updates a dragon
        Task<bool> UpdateDragonAsync(Dragon dragon);

        // Deletes a dragon based on its dragon id
        Task<bool> DeleteDragonAsync(Guid dragonId);
    }
}
