using System;
using System.Collections.Generic;
using EventStore.Domain.Model;
using Newtonsoft.Json.Linq;

namespace EventStore.UnitTests.Data
{
    public class Aggregate
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private List<string> users = new List<string>();
        public IEnumerable<string> Users { 
            get {
                return users;
            }
        }
        public string CreatedBy { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public string ClosedBy { get; private set; }
        public DateTimeOffset ClosedDate { get; private set; }

        public virtual Aggregate Apply(Event @event)
        {
            //NOTE: This is not an efficient serialization pattern.
            //DO NOT use this implementation for production code, this is used for testing purposes.

            switch(@event.Type)
            {
                case "Aggregate.Created":
                    ApplyCreated(@event);
                    break;
                case "Aggregate.User.Added":
                    ApplyUserAdded(@event);
                    break;
                case "Aggregate.Closed":
                    ApplyClosed(@event);
                    break;
                default:
                    break;  //FOR TESTING PURPOSES Ignore unknown events.
            }

            return this;
        }

        private void ApplyCreated(Event @event)
        {
            var data = @event.Data;

            Id = Convert.ToInt32(data["id"].ToString());
            Name = data["name"].ToString();
            Description = data["description"].ToString();

            CreatedBy = @event.User;
            CreatedDate = @event.Timestamp;
        }

        private void ApplyUserAdded(Event @event)
        {
            var data = @event.Data;

            users.Add(data["user"].ToString());
        }

        private void ApplyClosed(Event @event)
        {
            ClosedBy = @event.User;
            ClosedDate = @event.Timestamp;
        }
    }
}