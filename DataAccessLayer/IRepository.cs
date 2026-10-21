using MemeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : class,IDomainObject
    {
        void Add(T entity);

        IEnumerable<T> Readall();

        T ReadById(int id);

        void Update(T entity);

        void Delete(int id);
    }
}
