using MemeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// интерфейс, который определяет общий набор операций для любого типа сущности Т
    /// </summary>
    /// <typeparam name="T"> тип сущности</typeparam>
    public interface IRepository<T> where T: class, IDomainObject
    {
        void Add(Meme entity);

        IEnumerable<Meme> Readall();

        Meme ReadById(int id);

        void Update(Meme entity);

        void Delete(int id);
    }
}
