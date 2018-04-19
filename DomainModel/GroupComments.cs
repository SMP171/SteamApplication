using System;

namespace DomainModel
{
    public class GroupComments
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime? SendDate { get; set; }
        public User User { get; set; }
    }
}