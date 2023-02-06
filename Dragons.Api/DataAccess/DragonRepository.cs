using Amazon.SimpleSystemsManagement;
using Dapper;
using Dragons.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Dragons.Api.DataAccess
{
    public class DragonRepository : IDragonRepository
    {
        private readonly IDbConnection _dragonRepositoryConnection;
        private readonly IAmazonSimpleSystemsManagement _amazonSimpleSystemsManagement;

        public DragonRepository(
            IConfiguration configuration,
            IAmazonSimpleSystemsManagement amazonSimpleSystemsManagement)
        {
            _amazonSimpleSystemsManagement= amazonSimpleSystemsManagement;

            var connectionString = _amazonSimpleSystemsManagement.GetParameterAsync(
                new Amazon.SimpleSystemsManagement.Model.GetParameterRequest 
                { 
                    Name = "dragons-db-connection-string" 
                }).Result.Parameter.Value;

            _dragonRepositoryConnection = new NpgsqlConnection(connectionString);
        }

        public async Task<IEnumerable<Dragon>> ListAllDragonsAsync()
        {
            var dragonsList = await _dragonRepositoryConnection.QueryAsync<Dragon>("SELECT * FROM dragons");

            return dragonsList;
        }

        public async Task<Dragon> GetDragonByIdAsync(Guid dragonId)
        {
            // Object to map the parameters to the query
            object parameters = new { DragonId = dragonId };

            var dragon = await _dragonRepositoryConnection.QuerySingleAsync<Dragon>("SELECT * FROM dragons WHERE dragon_id = @DragonId", parameters);

            return dragon;
        }

        public async Task<bool> InsertDragonAsync(Dragon dragon)
        {
            // String builder used for clarity
            StringBuilder query= new StringBuilder();

            // Will return 1 if successful
            var createdDragonResult = await _dragonRepositoryConnection.ExecuteAsync(query
                .Append("INSERT INTO dragons (dragon_id, name, type, size) ")
                .Append("VALUES (@DragonId, @Name, @Type, @Size)").ToString(),
                dragon);

            return createdDragonResult == 1 ? true : false;
        }


        public async Task<bool> UpdateDragonAsync(Dragon dragon)
        {
            // String builder used for clarity
            StringBuilder query = new StringBuilder();

            // Update dragons SET name=@Name, type=@Type, size=@Size WHERE dragon_id=@DragonId;

            // Will return 1 if successful
            var updatedDragonResult = await _dragonRepositoryConnection.ExecuteAsync(query
                .Append("Update dragons ")
                .Append("SET name=@Name, type=@Type, size=@Size ")
                .Append("WHERE dragon_id=@DragonId").ToString(),
                dragon);

            return updatedDragonResult == 1 ? true : false;
        }

        public async Task<bool> DeleteDragonAsync(Guid dragonId)
        {
            // String builder used for clarity
            StringBuilder query = new StringBuilder();

            // Object to map the parameters to the query
            object[] parameters = { new { DragonId = dragonId } };

            // Will return 1 if successful
            var deleteDragonResult = await _dragonRepositoryConnection.ExecuteAsync(query
                .Append("DELETE FROM dragons ")
                .Append("WHERE dragon_id=@DragonId").ToString(),
                parameters);

            return deleteDragonResult.GetType() == typeof(int) ? true : false;
        }
    }
}
