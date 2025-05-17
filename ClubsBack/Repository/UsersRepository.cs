using ClubsBack.Entities;
using Microsoft.Data.Sqlite;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace ClubsBack.Repository
{
    public class UsersRepository : IRepository<User>
    {
        private readonly ApplicationContext _context;
        public UsersRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            User? u = _context.Users.FirstOrDefault(u => u.Id == id);

            if (u == null)
            {
                return false;
            }

            _context.Users.Remove(u);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public List<User> Get()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);

        }
        public User? GetByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.FirstName == name);

        }

        public int? Insert(User item)
        {
            _context.Users.Add(item);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return null;
            }
            return item.Id;
        }

        public bool Update(User item)
        {
            _context.Users.Update(item);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool SetRefreshToken(string refreshToken, int id)
        {
            User? r = _context.Users.FirstOrDefault(u => u.Id == id);

            if (r == null)
                return false;

            r.RefreshToken = refreshToken;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


    }
}
