//using Events_Web_application_DataBase.Services.DBServices.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Events_Web_application_DataBase.Entities
//{
//    public class EventEntity
//    {
//        public int Id { get; }

//        [Required]
//        [MaxLength(200)]
//        public string Title { get; } = string.Empty;

//        [MaxLength(1000)]
//        public string Description { get; } = string.Empty;

//        [Required]
//        public DateTime EventDateTime { get; }

//        [Required]
//        [MaxLength(300)]
//        public string Location { get; } = string.Empty;

//        [Required]
//        [MaxLength(100)]
//        public string Category { get; } = string.Empty;
//        [Required]
//        public int MaxParticipants { get; }

//        public List<Participant> Participants { get; } = new List<Participant>();

//        public byte[]? Image { get; }
//    }
//}
