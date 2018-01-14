﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;

namespace ravendb
{
    public class RavenRepository<T> : IRepository<T> where T : class
    {
        private Raven.Client.Document.DocumentSession _context;

        protected IDocumentSession Context
        {
            get
            {
                if (_context == null)
                {
                    _context = GetCurrentUnitOfWork<RavenUnitOfWork>().Context;
                }

                return _context;
            }
        }

        public TUnitOfWork GetCurrentUnitOfWork<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
        {
            return (TUnitOfWork)UnitOfWork.Current;
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Query<T>();
        }

        public IEnumerable<T> Find(Func<T, bool> where)
        {
            return this.Context.Query<T>().Where<T>(where);
        }

        public T Single(Func<T, bool> where)
        {
            return this.Context.Query<T>().SingleOrDefault<T>(where);
        }

        public T First(Func<T, bool> where)
        {
            return this.Context.Query<T>().First<T>(where);
        }

        public virtual void Delete(T entity)
        {
            this.Context.Delete(entity);
        }

        public virtual void Add(T entity)
        {
            this.Context.Store(entity);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}