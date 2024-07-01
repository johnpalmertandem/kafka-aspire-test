using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace AspirePoc.ServiceDefaults;

public record TestEvent(Guid Id, string Name, int EventNumber);

public class TestValidator: AbstractValidator<TestEvent>
{

    public TestValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).Length(0, 10);
        RuleFor(x => x.EventNumber).NotNull();
    }
}