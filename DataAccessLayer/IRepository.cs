using MemeApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRepository
    {
        void Add(Meme entity);

        IEnumerable<Meme> Readall();

        Meme ReadById(int id);

        void Update(Meme entity);

        void Delete(int id);
    }
}
