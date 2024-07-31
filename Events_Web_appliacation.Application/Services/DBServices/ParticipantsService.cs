using Events_Web_appliacation.Domain.Abstractions;
using Events_Web_application.Domain.Entities;
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
        public async Task<int> AddParticipant(Participant participant, CancellationTokenSource cancellationToken) =>
            await _participantRepository.Add(participant, cancellationToken);
        public async Task<int> DeleteParticipant(Guid id, CancellationTokenSource cancellationToken) =>
            await _participantRepository.Delete(id, cancellationToken);
        public async Task<int> UpdateParticipant(Participant participant, CancellationTokenSource cancellationToken) =>
            await _participantRepository.Update(participant, cancellationToken);
        public async Task<IEnumerable<Participant>> GetAllParticipant(CancellationTokenSource cancellationToken) =>
            await _participantRepository.GetAll(cancellationToken);
        public async Task<Participant> GetParticipantById(Guid id, CancellationTokenSource cancellationToken) =>
            await _participantRepository.Get(id, cancellationToken.Token);
    }
}
