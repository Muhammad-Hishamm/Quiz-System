using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.DTOs.Choices;
using Examination_System.Models;
using Examination_System.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class ChoiceService
    {
        private readonly GeneralRepository<Choice> _choiceRepository;
        private readonly GeneralRepository<QuestionChoice> _questionChoiceRepository;
        private readonly IMapper _mapper;

        public ChoiceService(IMapper mapper)
        {
            _mapper = mapper;
            _choiceRepository = new GeneralRepository<Choice>();
            _questionChoiceRepository = new GeneralRepository<QuestionChoice>();
        }

        public IEnumerable<GetAllChoicesDTOs>? GetAll()
        {
            var query = _choiceRepository.GetAll()
                        .ProjectTo<GetAllChoicesDTOs>(_mapper.ConfigurationProvider);
            return query;
        }

        public GetAllChoicesDTOs GetById(int id)
        {
            var dto = _choiceRepository.GetById(id)
                      .ProjectTo<GetAllChoicesDTOs>(_mapper.ConfigurationProvider)
                      .FirstOrDefault();
            return dto;
        }

        public async Task<bool> Create(CreateChoiceDTO choiceDto)
        {
            if (choiceDto == null) return false;

            var choice = _mapper.Map<Choice>(choiceDto);
            // Persist choice
            var created = await _choiceRepository.CreateAsync(choice).ConfigureAwait(false);

            // Create question-choice link if a question id is provided
            if (created && choiceDto.QuestionId > 0)
            {
                var qc = new QuestionChoice
                {
                    QuestionId = choiceDto.QuestionId,
                    ChoiceId = choice.Id,
                    CreatedAt = DateTime.UtcNow
                };
                await _questionChoiceRepository.CreateAsync(qc).ConfigureAwait(false);
            }

            return created;
        }

        public async Task<bool> Update(int id, UpdateChoiceDto updatedChoice)
        {
            if (updatedChoice == null) return false;
            if (this.GetById(id) == null) return false;

            var choice = _mapper.Map<Choice>(updatedChoice);
            choice.Id = id;
            var result = await _choiceRepository.UpdateAsync(choice).ConfigureAwait(false);

            // Update question-choice mapping: remove existing mappings and add new one if provided
            var existingQCs = _questionChoiceRepository.GetAll().Where(q => q.ChoiceId == id).ToList();
            foreach (var qc in existingQCs)
            {
                await _questionChoiceRepository.DeleteAsync(qc.Id).ConfigureAwait(false);
            }

            if (updatedChoice.QuestionId > 0)
            {
                var qc = new QuestionChoice
                {
                    QuestionId = updatedChoice.QuestionId,
                    ChoiceId = id,
                    CreatedAt = DateTime.UtcNow
                };
                await _questionChoiceRepository.CreateAsync(qc).ConfigureAwait(false);
            }

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = this.GetById(id);
            if (existing == null) return false;

            await _choiceRepository.DeleteAsync(id).ConfigureAwait(false);

            // Soft-delete any question-choice mappings
            var existingQCs = _questionChoiceRepository.GetAll().Where(q => q.ChoiceId == id).ToList();
            foreach (var qc in existingQCs)
            {
                await _questionChoiceRepository.DeleteAsync(qc.Id).ConfigureAwait(false);
            }

            return true;
        }
    }
}