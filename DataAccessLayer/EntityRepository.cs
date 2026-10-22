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


        /// <summary>
        /// Принимает контекст базы данных, через который будут выполняться все операции с таблицами
        /// </summary>
        /// <param name="context">контекст базы данных</param>
        public EntityRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет новую запись в бд
        /// </summary>
        /// <param name="entity">мем, который нужно добавить</param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет мем из бд
        /// </summary>
        /// <param name="id">идентефикатор уаляемомого мема</param>
        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Возвращает список всех записей из бд
        /// </summary>
        /// <returns>Список мемов</returns>
        public IEnumerable<T> Readall()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// Находит мем по идентификатору
        /// </summary>
        /// <param name="id">дентификатор мема</param>
        /// <returns>найденный мем</returns>
        public T ReadById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Обновляет данные о записи в бд
        /// </summary>
        /// <param name="entity">обновлённый мем</param>
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
