using Infrastructure.DAL.Repositories;
using Npgsql;

namespace Infrastructure.DAL
{
    public class UnitOfWork:IUnitOfWork
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction _transaction;
        private IHdrBoxRepo _hdrBoxRepo;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IHdrBoxRepo HdrBoxRepo
        {
            get { return _hdrBoxRepo ?? (_hdrBoxRepo = new HdrBoxRepo(_transaction)); }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _hdrBoxRepo = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}