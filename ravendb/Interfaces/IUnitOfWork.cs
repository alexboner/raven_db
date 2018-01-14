using System;

namespace ravendb
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}