using System;

namespace DomainModel
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public int WalletId { get; set; }
        public int StatusId { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return Nickname;
        }
    }
}
