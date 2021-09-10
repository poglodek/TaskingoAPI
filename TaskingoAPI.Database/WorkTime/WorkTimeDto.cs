using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskingoAPI.Dto.WorkTime
{
    public class WorkTimeDto
    {
        public DateTime WorkTimeStart { get; set; }
        public DateTime WorkTimeEnd { get; set; }
        public int BreakTimeInMinutes { get; set; }
    }
}
