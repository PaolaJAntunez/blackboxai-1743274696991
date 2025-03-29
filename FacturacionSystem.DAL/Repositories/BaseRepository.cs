using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;

namespace FacturacionSystem.DAL.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected virtual string TableName { get; }
        protected virtual string PrimaryKey { get; } = "Id";

        public virtual List<T> GetAll()
        {
            var query = $"SELECT * FROM {TableName}";
            var dataTable = DatabaseHelper.ExecuteQuery(query);
            return DataTableToList(dataTable);
        }

        public virtual T GetById(int id)
        {
            var query = $"SELECT * FROM {TableName} WHERE {PrimaryKey} = @Id";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Id", id)
            };
            var dataTable = DatabaseHelper.ExecuteQuery(query, parameters);
            return DataTableToObject(dataTable);
        }

        public virtual int Insert(T entity)
        {
            var (query, parameters) = GetInsertQuery(entity);
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public virtual int Update(T entity)
        {
            var (query, parameters) = GetUpdateQuery(entity);
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        public virtual int Delete(int id)
        {
            var query = $"DELETE FROM {TableName} WHERE {PrimaryKey} = @Id";
            var parameters = new OleDbParameter[] {
                new OleDbParameter("@Id", id)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters);
        }

        protected abstract (string, OleDbParameter[]) GetInsertQuery(T entity);
        protected abstract (string, OleDbParameter[]) GetUpdateQuery(T entity);
        protected abstract T DataTableToObject(DataTable dataTable);
        protected abstract List<T> DataTableToList(DataTable dataTable);
    }
}