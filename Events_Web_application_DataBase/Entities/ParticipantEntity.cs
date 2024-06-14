//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace Events_Web_application_DataBase.Entities
//{
//    internal class ParticipantEntity
//    {
//        public int Id { get; set; }

//        [Required]
//        [MaxLength(200)]
//        public string FirstName { get; set; }

//        [Required]
//        [MaxLength(200)]
//        public string LastName { get; set; }

//        [Required]
//        public DateTime DateOfBirth { get; set; }

//        [Required]
//        public DateTime RegistrationDate { get; set; }

//        [JsonIgnore]
//        public List<Event> UserEvents { get; set; }

//        [Required]
//        [MaxLength(200)]
//        public string Email { get; set; }
//    }
//}
