namespace LHDTV.Models.DbEntity
{
    public class UserDb
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Dni { get; set; }
        //public string Token { get; set; }
        public bool Deleted { get; set; }

    }
}