using ClubsBack.Entities;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ClubsBack.Repository
{
    public class ClubsRepository : IClubs
    {
        private readonly DBconnect _options;
        public ClubsRepository(DBconnect options)
        {
            _options = options;
        }
        public bool CreateClub(Club item, int userId)
        {
            using (SqliteConnection conn = new SqliteConnection(_options.Connect))
            {

                int result = conn.Execute("INSERT INTO Clubs (title, description) VALUES (@title, @description)", item);
                if (result != 0)
                {
                    int clubId = conn.QuerySingle<int>("SELECT last_insert_rowid();");
                    ClubUser clubs = new ClubUser {ClubId = clubId,UserId = userId,IsAdmin = true };
                        
                    
                    conn.Execute("INSERT INTO ClubsUsers (userId, clubId, isAdmin) VALUES (@userId, @clubId,@isAdmin)", clubs);
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool Delete(int id)
        {
            using (SqliteConnection conn = new SqliteConnection(_options.Connect))
            {
                int result = conn.Execute("DELETE FROM Clubs WHERE id = @id",new { id = id });
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool CheckUserOwnClub(ClubUser item){
            using (SqliteConnection conn = new SqliteConnection(_options.Connect)) { 
                ClubUser? result = conn.QueryFirstOrDefault<ClubUser>($"SELECT * FROM ClubsUsers WHERE clubId = @clubId AND userId = @userId AND isAdmin = @isAdmin",item);
                if(result != null){
                    return true;
                }
                else{
                    return false;
                }
                
            }
        }
        public bool ExitClub(int id)
        {
            using (SqliteConnection conn = new SqliteConnection(_options.Connect)) { 
                int result =  conn.Execute(@"DELETE FROM ClubUsers WHERE Id = @Id", new {Id = id});
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Club> Get()
        {
            using (SqliteConnection conn = new SqliteConnection(_options.Connect)) { 
                 return conn.Query<Club>("SELECT * FROM Clubs").ToList();
            }
        }

        public Club? GetById(int id)
        {
            using (SqliteConnection conn = new SqliteConnection(_options.Connect))
            {
                return conn.QueryFirstOrDefault<Club>("SELECT * FROM Clubs WHERE id = @id", new { id = id });
            }
        }

        public bool SignClub(int clubId, int userId)
        {
            using (SqliteConnection conn = new SqliteConnection(_options.Connect))
            {
                int result = conn.Execute("INSERT INTO ClubsUsers (clubId, userId) VALUES (@clubId, @userId)", new { clubId, userId });

                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }

        }

        public bool Update(Club item)
        {
            using (SqliteConnection conn = new SqliteConnection(_options.Connect))
            {
                int result = conn.Execute("UPDATE Clubs SET title = @title, description = @description WHERE id = @id", item);

                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
