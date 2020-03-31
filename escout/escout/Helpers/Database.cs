using Microsoft.Win32.SafeHandles;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static System.Environment;

namespace escout.Helpers
{
    public class BaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

    }

    abstract class BaseRepository<T> : IDisposable
        where T : BaseModel, new()
    {
        public abstract Task<T> Select(int id);
        public abstract Task<List<T>> Select();
        public abstract Task<List<T>> Query(string query);
        public abstract Task<int> Count();
        public abstract Task<int> Insert(T data);
        public abstract Task<int> Insert(IEnumerable<T> data);
        public abstract Task<int> Update(T data);
        public abstract Task<int> Update(IEnumerable<T> data);
        public abstract Task<int> Delete(T data);
        public abstract Task<int> Delete(int id);
        public abstract Task<int> Delete();
        public abstract void Dispose();
    }
    class Database<T> : BaseRepository<T>
        where T : BaseModel, new()
    {
        private static readonly object locker = new object();
        private SQLiteAsyncConnection connection;
        private string path = Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "database.db3");

        bool disposed = false;
        SafeHandle safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        public Database()
        {
            lock (locker)
            {
                connection = new SQLiteAsyncConnection(path);

                connection.CreateTableAsync<T>();
            }
        }

        public bool IsTableCreated(string tableName)
        {
            lock (locker)
            {
                var tableInfo = connection.GetConnection().GetTableInfo(tableName);

                if (tableInfo.Count > 0)
                    return true;
                return false;
            }
        }

        public override Task<List<T>> Select()
        {
            lock (locker)
                return connection.Table<T>().ToListAsync();
        }

        public override Task<T> Select(int id)
        {
            lock (locker)
                return connection.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public override Task<List<T>> Query(string query)
        {
            lock (locker)
                return connection.QueryAsync<T>(query);
        }

        public override Task<int> Count()
        {
            lock (locker)
                return connection.Table<T>().CountAsync();
        }

        public override Task<int> Insert(T data)
        {
            lock (locker)
            {
                if (data.Id != 0)
                    return connection.UpdateAsync(data);
                else
                    return connection.InsertAsync(data);
            }
        }

        public override Task<int> Insert(IEnumerable<T> data)
        {
            lock (locker)
                return connection.InsertAllAsync(data);
        }

        public override Task<int> Update(T data)
        {
            lock (locker)
                return connection.UpdateAsync(data);
        }

        public override Task<int> Update(IEnumerable<T> data)
        {
            lock (locker)
                return connection.UpdateAllAsync(data);
        }

        public override Task<int> Delete(T data)
        {
            lock (locker)
                return connection.DeleteAsync(data);
        }
        public override Task<int> Delete(int id)
        {
            lock (locker)
                return connection.DeleteAsync(id);
        }

        public override Task<int> Delete()
        {
            lock (locker)
                return connection.DeleteAllAsync<T>();
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (locker)
            {
                if (disposed)
                    return;

                if (disposing)
                    safeHandle.Dispose();

                connection = null;
                path = null;
                disposed = true;
            }
        }
    }
}