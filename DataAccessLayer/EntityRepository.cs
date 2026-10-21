using MemeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer
{
    internal class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        protected readonly AppDbContext _context;

        public EntityRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Readall()
        {
            throw new NotImplementedException();
        }

        public T ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
