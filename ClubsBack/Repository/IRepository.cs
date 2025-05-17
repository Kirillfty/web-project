using ClubsBack.Entities;

namespace ClubsBack.Repository
{
    public interface IRepository<Item>
    {
        public int? Insert(Item item);

        //удаление
        public bool Delete(int id);

        //редактирование
        public bool Update(Item item);

        //получение одного элемента
        public Item? GetById(int id);
        public User? GetByName(string name);

        //получение всех
        public List<Item> Get();
        bool SetRefreshToken(string refreshToken, int id);
        public List<User> GetUsersInClub(int clubId);
    }
}
