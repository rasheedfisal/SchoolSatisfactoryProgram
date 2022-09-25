using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.DAL
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        string GetConnectionString();
        Task<List<T>> LoadData<T, U>(string storedProcedures, U parameters);
        Task<List<T>> LoadDataQuery<T>(string QueryText);
        Task<T> LoadModel<T, U>(string storedProcedures, U parameters);
        Task<T> LoadModelQuery<T>(string QueryText);
        Task<T> GetValue<T>(string storedProcedures, DynamicParameters parameters);
        void RollBackTransaction();
        Task<T> SaveData<T, U>(string storedProcedures, U parameters);
        Task SaveData<T>(string storedProcedures, T parameters);
        Task<T> SaveDataQuery<T, U>(string QueryText, U parameters);
        Task<bool> SaveDataQuery<T>(string QueryText, T parameters);
        Task<T> SaveDataInTransaction<T, U>(string storedProcedures, U parameters);
        Task<T> SaveDataInTransactionQuery<T, U>(string QueryText, U parameters);
        Task<bool> SaveDataInTransactionWithoutReturn<T, U>(string storedProcedures, U parameters);
        Task<bool> SaveDataInTransactionWithoutReturnQuery<T, U>(string QueryText, U parameters);
        Task<T> GetValueInTransaction<T, U>(string storedProcedures, U parameters);
        Task<List<T>> LoadDataInTransaction<T, U>(string storedProcedures, U parameters);
        void StartTransaction();
    }
}