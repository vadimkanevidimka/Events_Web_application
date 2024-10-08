﻿using System.Text.Json.Serialization;

namespace Events_Web_application.Domain.Entities
{
    public class Participant : BaseEntity
    {
        public Participant() => UserEvents = new List<Event>();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; }

        [JsonIgnore]
        public List<Event> UserEvents { get; set; }
    }

}
