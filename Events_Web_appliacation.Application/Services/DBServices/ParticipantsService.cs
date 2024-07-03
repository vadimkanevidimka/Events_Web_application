using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Models;
using Events_Web_application.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events_Web_application.Application.Services.DBServices
{
    public class ParticipantsService
    {
        private IRepository<Participant> _participantRepository { get; set; }

        public ParticipantsService(ParticipantsRepository participantsRepository)
        {
            _participantRepository = participantsRepository;
        }
        public async Task<int> AddParticipant(Participant participant) =>
            await _participantRepository.Add(participant);
        public async Task<int> DeleteParticipant(Guid id) =>
            await _participantRepository.Delete(id);
        public async Task<int> UpdateParticipant(Participant participant) =>
            await _participantRepository.Update(participant);
        public async Task<IEnumerable<Participant>> GetAllParticipant() =>
            await _participantRepository.GetAll();
        public async Task<Participant> GetParticipantById(Guid id) =>
            await _participantRepository.Get(id);
    }
}
