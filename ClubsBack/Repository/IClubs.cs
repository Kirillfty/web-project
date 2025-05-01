using ClubsBack.Entities;
namespace ClubsBack.Repository
{
    public interface IClubs
    {
        public Club? GetById(int id);

        public List<Club> Get();

        public bool CreateClub(Club item,int userId);

        public bool Update(Club item);

        public bool SignClub(ClubUser item);
        bool CheckUserOwnClub(ClubUser item);

        public bool ExitClub(int id);
        public bool Delete(int id);
    }
}
