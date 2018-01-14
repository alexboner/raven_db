using System;
using Raven.Client;

namespace ravendb
{
    public class RavenUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static Func<IDocumentStore> _objectContextDelegate;
        private static readonly Object _lockObject = new Object();

        public static void SetObjectContext(Func<IDocumentStore> objectContextDelegate)
        {
            _objectContextDelegate = objectContextDelegate;
        }

        #region IUnitOfWorkFactory Members

        public IUnitOfWork Create()
        {
            IDocumentStore context;

            lock (_lockObject)
            {
                context = _objectContextDelegate();
            }

            return new RavenUnitOfWork(context);
        }

        #endregion
    }
}
