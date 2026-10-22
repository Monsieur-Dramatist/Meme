using Dapper;
using MemeApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DataAccessLayer
{
    internal class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly IDbConnection _connection;

        /// <summary>
        /// Принимает подключение к базе данных, через которое будут выполняться SQL-запросы
        /// </summary>
        /// <param name="connection">Подключение к бд</param>
        public DapperRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Добавляет новую запись в таблицу Memes
        /// </summary>
        /// <param name="entity">мем, который нужно сохранить</param>
        public void Add(T entity)
        {
            string sql = "INSERT INTO Memes (Name, Category, IsActual) VALUES (@Name, @Category, @IsActual)";
            _connection.Execute(sql, entity);
        }

        /// <summary>
        /// Удаляет запись из таблицы Memes по её идентификатору
        /// </summary>
        /// <param name="id">идентификатор удаляемоо мема</param>
        public void Delete(int id)
        {
            string sql = "DELETE FROM Memes WHERE Id = @Id";
            _connection.Execute(sql, new { Id = id });
        }

        /// <summary>
        /// Возвращает список всех записей из таблицы Memes.
        /// </summary>
        /// <returns>список всех мемов</returns>
        public IEnumerable<T> Readall()
        {
            string sql = "SELECT * FROM Memes";
            return _connection.Query<T>(sql);
        }

        /// <summary>
        /// Находит мем в таблице Memes по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор мема</param>
        /// <returns>найденный мем или null</returns>
        public T ReadById(int id)
        {
            string sql = "SELECT * FROM Memes WHERE Id = @Id";
            return _connection.QueryFirstOrDefault<T>(sql, new { Id = id });
        }

        /// <summary>
        /// Обновляет данные существующей записи в таблице Memes
        /// </summary>
        /// <param name="entity">обновлённый мем</param>
        public void Update(T entity)
        {
            string sql = "UPDATE Memes SET Name = @Name, Category = @Category, IsActual = @IsActual WHERE Id = @Id";
            _connection.Execute(sql, entity);
        }
    }
}
