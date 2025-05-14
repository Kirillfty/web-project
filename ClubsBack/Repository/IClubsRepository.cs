using ClubsBack.Entities;
namespace ClubsBack.Repository
{
    public interface IClubsRepository
    {
        public Club? GetById(int id);

        public List<Club> Get();

        public bool CreateClub(Club item,int userId);

        public bool Update(Club item);
        bool CheckUserOwnClub(ClubUser item);
        public bool EnterClub(int clubId, int userId);
        public bool ExitClub(int clubId, int userId);
        public bool Delete(int id);
        public List<Club> GetUserClub(int userId);
    }
}
