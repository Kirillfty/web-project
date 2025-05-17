using ClubsBack.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
//AsNoTracking() add
namespace ClubsBack.Repository
{
    public class ClubsRepository : IClubsRepository
    {
        private readonly ApplicationContext _context;
        private readonly DBconnect _options;
        public ClubsRepository(DBconnect connect,ApplicationContext context) {
            _options = connect;
            _context = context;
        }
        
        public bool CreateClub(Club newClub, int userId)
        {
            _context.Clubs.Add(newClub);
            try {
                _context.SaveChanges();
            }
            catch (Exception ex) {
                return false;
            }

            _context.ClubsUsers.Add(new ClubUser { ClubId = newClub.Id,UserId = userId,IsAdmin = true});
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            //после создания клуба получай его Id, а создавай ClubUser с isAdmin = true
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

        public bool EnterClub(int clubId, int userId)
        {
            var club = _context.Clubs.AsNoTracking().FirstOrDefault(c => c.Id == clubId);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (club == null || user == null) 
                return false;

            if (_context.ClubsUsers.Any(c => c.ClubId == club.Id && c.UserId == user.Id )) 
                return false;

            var newClubUser = new ClubUser {ClubId = club.Id,UserId = user.Id,IsAdmin = false};
            _context.ClubsUsers.Add(newClubUser);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool ExitClub(int clubId, int userId)
        {
            ClubUser? c = _context.ClubsUsers.FirstOrDefault(c => c.ClubId == clubId && c.UserId == userId);

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
        
        public List<Club> GetUserClub(int userId)
        {
           return _context.ClubsUsers
               .Where(u => u.UserId == userId && u.IsAdmin == true)
               .Select(club => club.Club)
               .ToList();
        }
    }
}
