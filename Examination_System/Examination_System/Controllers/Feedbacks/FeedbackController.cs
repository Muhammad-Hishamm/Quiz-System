using AutoMapper;
using Examination_System.DTOs.Feedbacks;
using Examination_System.Models;
using Examination_System.Services;
using Examination_System.ViewModels;
using Examination_System.ViewModels.Feedback;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examination_System.Controllers.Feedbacks
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbackController(IMapper mapper)
        {
            _mapper = mapper;
            _feedbackService = new FeedbackService(_mapper);
        }

        // GET: api/Feedback
        [HttpGet]
        public ResponseViewModel<IEnumerable<GetFeedbackViewModel>> Get()
        {
            var dtos = _feedbackService.GetAll();
            var vm = _mapper.Map<IEnumerable<GetFeedbackViewModel>>(dtos);
            return new ResponseViewModel<IEnumerable<GetFeedbackViewModel>> { Data = vm, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        // GET: api/Feedback/1
        [HttpGet("{id}")]
        public ResponseViewModel<GetFeedbackViewModel> GetById(int id)
        {
            var dto = _feedback_service_wrapper(id);
            if (dto == null) return new ResponseViewModel<GetFeedbackViewModel> { Data = null, IsSuccess = false, ErrorCode = ErrorCode.CourseNotFound, Message = "Feedback not found." };

            var res = _mapper.Map<GetFeedbackViewModel>(dto);
            return new ResponseViewModel<GetFeedbackViewModel>() { Data = res, IsSuccess = true, ErrorCode = ErrorCode.NoError, Message = string.Empty };
        }

        [HttpPost]
        public async Task<ResponseViewModel<bool>> Create(CreateFeedbackViewModel feedback)
        {
            var dto = _mapper.Map<CreateFeedbackDTO>(feedback);
            var ok = await _feedbackService.Create(dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.BadRequest, Message = ok ? string.Empty : "Failed to create feedback." };
        }

        [HttpPut("{id}")]
        public async Task<ResponseViewModel<bool>> Update(int id, UpdateFeedbackViewModel updatedFeedback)
        {
            var dto = _mapper.Map<UpdateFeedbackDto>(updatedFeedback);
            var ok = await _feedbackService.Update(id, dto).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? string.Empty : "Feedback not found." };
        }

        [HttpDelete("{id}")]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var ok = await _feedbackService.Delete(id).ConfigureAwait(false);
            return new ResponseViewModel<bool> { Data = ok, IsSuccess = ok, ErrorCode = ok ? ErrorCode.NoError : ErrorCode.CourseNotFound, Message = ok ? string.Empty : "Feedback not found." };
        }

        // small private wrapper to avoid accidental naming collisions in mapping
        private GetAllFeedbacksDTOs _feedback_service_wrapper(int id) => _feedbackService.GetById(id);
    }
}