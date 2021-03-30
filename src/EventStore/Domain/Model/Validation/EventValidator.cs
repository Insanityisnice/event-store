using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace EventStore.Domain.Model.Validation
{
    internal static class EventExtensions
    {
        public static void ValidateAndThrow(this IEnumerable<Event> events)
        {
            throw new NotImplementedException();
        }

        public static ValidationResult Validate(this IEnumerable<Event> events)
        {
            throw new NotImplementedException();
        }

        public static void ValidateAndThrow(this Event @event)
        {
            throw new NotImplementedException();
        }

        public static ValidationResult Validate(this Event @event)
        {
            throw new NotImplementedException();
        }
    }

    internal class EventsValidator : AbstractValidator<IEnumerable<Event>>
    {
        public EventsValidator()
        {
            RuleFor(tags => tags).NotNull();
            RuleFor(tags => tags).NotEmpty();
        }
    }

    internal class EventValidator : AbstractValidator<Event>
    {
        public EventValidator()
        {            
            RuleFor(@event => @event).NotNull();
        }
    }
}