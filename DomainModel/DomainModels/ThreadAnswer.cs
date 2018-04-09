using System;

namespace DomainModel.DomainModels
{
    public class ThreadAnswer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ThreadId { get; set; }
        public DateTime SendDate { get; set; }
        public string Text { get; set; }
    }
}
