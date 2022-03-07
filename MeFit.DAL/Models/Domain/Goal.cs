using System;

namespace MeFitAPI.DAL.Models.Domain
{
    public class Goal
    {
        public int Id { get; set; }
        public DateTime EndDate { get; set; }
        public bool Achieved { get; set; }
    }
}
