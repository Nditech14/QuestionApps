using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuestionApp.Dtos;
using QuestionApp.Entity;
using QuestionApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;

        public QuestionController(IMapper mapper, IQuestionService questionService)
        {
            _mapper = mapper;
            _questionService = questionService;
        }

        [HttpPost("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDtos questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = _mapper.Map<Question>(questionDto);
            await _questionService.AddQuestion(question);
            var createdQuestionDto = _mapper.Map<QuestionDtos>(question);
            return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, createdQuestionDto);
        }

        [HttpPut("EditQuestion/{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] QuestionDtos questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var existingQuestion = await _questionService.GetQuestionById(id);
            if (existingQuestion == null)
            {
                return NotFound($"Question with ID {id} not found.");
            }

           
            _mapper.Map(questionDto, existingQuestion);

           
            await _questionService.UpdateQuestion(existingQuestion);

            return NoContent();
        }


        [HttpGet("GetAllQuestions")]
        public async Task<IActionResult> GetQuestions()
        {
            var questions = await _questionService.GetAllQuestions();
            var questionDtos = _mapper.Map<IEnumerable<QuestionDtos>>(questions);
            return Ok(questionDtos);
        }

        [HttpPost("SubmitResponses")]
        public async Task<IActionResult> SubmitResponses([FromBody] List<ResponseDtos> responses)
        {
            if (responses == null || !responses.Any())
            {
                return BadRequest("Responses cannot be null or empty.");
            }

            try
            {
          
                var questionIds = responses.Select(r => r.QuestionId).Distinct().ToList();

              
               
                var existingQuestionIds = await _questionService.GetResponsesByIds(questionIds);
                
                if(existingQuestionIds == null)
                { 
                    return BadRequest($"The following question IDs do not exist: {string.Join(", ")}");
                }

                var responseEntities = _mapper.Map<List<Response>>(responses);
                await _questionService.AddResponses(responseEntities);
                var savedResponses = await _questionService.GetResponsesByIds(responseEntities.Select(r => r.Id).ToList());
                var responseDtos = _mapper.Map<List<ResponseDtos>>(savedResponses);

                return Ok(responseDtos);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error.");
            }


        }


    [HttpGet("GetQuestionById/{id}")]
        public async Task<IActionResult> GetQuestionById(Guid id)
        {
            var question = await _questionService.GetQuestionById(id);
            if (question == null)
            {
                return NotFound($"Question with ID {id} not found.");
            }

            var questionDto = _mapper.Map<QuestionDtos>(question);
            return Ok(questionDto);
        }
    }
}
