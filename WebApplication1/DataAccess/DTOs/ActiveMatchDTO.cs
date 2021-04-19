using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DataAccess.DTOs
{
    public class ActiveMatchDTO
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public DateTime EndTime { get; set; }
    }
}