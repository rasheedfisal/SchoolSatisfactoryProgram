using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace School.DAL
{
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }


        public string GetConnectionString()
        {
            return _config.GetConnectionString("myConn");
        }

        public async Task<List<T>> LoadData<T, U>(string storedProcedures, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                return (await conn.QueryAsync<T>(storedProcedures, parameters, commandType: CommandType.StoredProcedure)).AsList();
            }
        }
        public async Task<List<T>> LoadDataQuery<T>(string QueryText)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                return (await conn.QueryAsync<T>(QueryText, commandType: CommandType.Text)).AsList();
            }
        }

        public async Task<T> SaveData<T, U>(string storedProcedures, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                return await conn.QuerySingleOrDefaultAsync<T>(storedProcedures, parameters, commandType: CommandType.StoredProcedure);

            }
        }
        
        public async Task SaveData<T>(string storedProcedures, T parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                await conn.ExecuteAsync(storedProcedures, parameters, commandType: CommandType.StoredProcedure);

            }
        }
        public async Task<T> SaveDataQuery<T, U>(string QueryText, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                return await conn.QuerySingleOrDefaultAsync<T>(QueryText, parameters, commandType: CommandType.Text);

            }
        }
        public async Task<bool> SaveDataQuery<T>(string QueryText, T parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                var result = (await conn.ExecuteAsync(QueryText, parameters, commandType: CommandType.Text));
                return result > 0 ? true : false;
            }
        }
        public async Task<T> LoadModel<T, U>(string storedProcedures, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                return await conn.QuerySingleOrDefaultAsync<T>(storedProcedures, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<T> LoadModelQuery<T>(string QueryText)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                return await conn.QuerySingleOrDefaultAsync<T>(QueryText, commandType: CommandType.Text);
            }
        }

        public async Task<T> GetValue<T>(string storedProcedures, DynamicParameters parameters)
        {
            using (IDbConnection conn = new SqlConnection(GetConnectionString()))
            {
                return ((T)Convert.ChangeType(await conn.ExecuteScalarAsync(storedProcedures, parameters, commandType: CommandType.StoredProcedure), typeof(T)));
            }
        }

        // Transactions Methods
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public async Task<T> SaveDataInTransaction<T, U>(string storedProcedures, U parameters)
        {
           return await _connection.QuerySingleAsync<T>(storedProcedures, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
        }
        public async Task<T> SaveDataInTransactionQuery<T, U>(string QueryText, U parameters)
        {
            return await _connection.QuerySingleAsync<T>(QueryText, parameters, commandType: CommandType.Text, transaction: _transaction);
        }
        public async Task<bool> SaveDataInTransactionWithoutReturn<T, U>(string storedProcedures, U parameters)
        {
            return (await _connection.ExecuteAsync(storedProcedures, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction)) > 0 ? true : false;
        }
        public async Task<bool> SaveDataInTransactionWithoutReturnQuery<T, U>(string QueryText, U parameters)
        {
            return (await _connection.ExecuteAsync(QueryText, parameters, commandType: CommandType.Text, transaction: _transaction)) > 0 ? true : false;
        }

        public async Task<List<T>> LoadDataInTransaction<T, U>(string storedProcedures, U parameters)
        {
            return (await _connection.QueryAsync<T>(storedProcedures, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction)).AsList();

        }

        public async Task<T> GetValueInTransaction<T, U>(string storedProcedures, U parameters)
        {
                return ((T)Convert.ChangeType(await _connection.ExecuteScalarAsync(storedProcedures, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction), typeof(T)));
        }
        public void StartTransaction()
        {
            _connection = new SqlConnection(GetConnectionString());
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            isClosed = false;
        }

        private bool isClosed = false;
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();

            isClosed = true;
            _transaction = null;
            _connection = null;
        }

        public void RollBackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();

            isClosed = true;
            _transaction = null;
            _connection = null;
        }

        public void Dispose()
        {
            if (isClosed == false)
            {
                try
                {
                    //CommitTransaction();
                }
                catch
                {
                    // TODO - log this issue
                }
            }

            //_transaction = null;
            //_connection = null;
        }
    }
}
