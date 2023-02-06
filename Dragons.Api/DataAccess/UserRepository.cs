using Amazon.SimpleSystemsManagement;
using Dapper;
using Dragons.Api.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dragons.Api.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _userRepositoryConnection;

        private readonly IAmazonSimpleSystemsManagement _amazonSimpleSystemsManagement;

        public UserRepository(IConfiguration configuration, IAmazonSimpleSystemsManagement simpleSystemsManagement)
        {
            _amazonSimpleSystemsManagement = simpleSystemsManagement;

            var connectionString = _amazonSimpleSystemsManagement.GetParameterAsync(
                new Amazon.SimpleSystemsManagement.Model.GetParameterRequest
                {
                    Name = "dragons-db-connection-string"
                }).Result.Parameter.Value;

            _userRepositoryConnection = new NpgsqlConnection(connectionString);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            // Object to map the parameters to the query
            object parameters = new { Username = username };

            var user = await _userRepositoryConnection.QuerySingleAsync<User>("SELECT * FROM users WHERE username = @Username", parameters);

            return user;
        }

        public async Task<IEnumerable<User>> ListAllUsersAsync()
        {
            var usersList = await _userRepositoryConnection.QueryAsync<User>("SELECT * FROM users");

            return usersList;
        }
    }
}
