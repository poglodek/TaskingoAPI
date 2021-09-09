using FluentValidation;

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
