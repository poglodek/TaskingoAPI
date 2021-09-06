using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskingoAPI.Dto.User;

namespace TaskingoAPI.Dto.WorkTask
{
    public class WorkTaskCreatedDtoValidation : AbstractValidator<WorkTaskCreatedDto>
    {
        public WorkTaskCreatedDtoValidation()
        {
            RuleFor(x => x.Priority).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10);
        }
    }
}
