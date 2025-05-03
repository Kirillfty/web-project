using System;
using ClubsBack.Entities;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ClubsBack.Repository
{
    public class ClubsRepository : IClubs
    {
        private readonly ApplicationContext _context;
        private readonly DBconnect _options;
        public ClubsRepository(DBconnect connect,ApplicationContext context) {
            _options = connect;
            _context = context;
        }
        
        public bool CreateClub(Club item, int userId)
        {
            _context.Clubs.Add(item);
            try {
                _context.SaveChanges();
            }
            catch (Exception ex) {
                return false;
            }
            return true;
        }
        public bool Delete(int id)
        {
            Club? club = _context.Clubs.FirstOrDefault(u => u.Id == id);

            if (club == null)
            {
                return false;
            }

            _context.Clubs.Remove(club);
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
        public bool CheckUserOwnClub(ClubUser item){
            return _context.ClubsUsers.Any(u => u.UserId == item.UserId && u.ClubId == item.ClubId && u.IsAdmin);
        }
        public bool ExitClub(int id)
        {
            ClubUser? c = _context.ClubsUsers.FirstOrDefault(u => u.Id == id);

            if (c == null)
                return false;

            _context.ClubsUsers.Remove(c);

            try {
                _context.SaveChanges();
            }

            catch (Exception ex) {
                return false;
            }

            return true;
        }

        public List<Club> Get()
        {
            return _context.Clubs.ToList();
        }

        public Club? GetById(int id)
        {
            return _context.Clubs.FirstOrDefault(u => u.Id == id);
        }

        public bool SignClub(ClubUser item)
        {
            _context.ClubsUsers.Add(item);
            try {
                _context.SaveChanges();
            }
            catch (Exception ex) {
                return false;
            }

            return true;
        }

        public bool Update(Club item)
        {
            _context.Clubs.Update(item);

            try {
                _context.SaveChanges();
            }
            catch (Exception ex) {
                return false;
            }

            return true;
        }
    }
}
