using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEDProject.Models.Dtos;
using SEDProject.Models.Entities;
using SEDProject.Models.Extentions;
using SEDProject.Models.Repositories;
using SEDProject.Models.utl;

namespace SEDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : Controller
    {
        private readonly IRepository<Participant> _participants;
        private readonly IRepository<User> _users;
        private readonly IRepository<Department> _departments;

        public ParticipantController(IRepository<Participant> repository, IRepository<User> users, IRepository<Department> departments)
        {
            _participants = repository;
            _users = users;
            _departments = departments;
        }

        // GET: api/<ParticipantContoller>
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IQueryable<Participant>>> Get()
        {
            var participants = await _participants.GetAllAsync();

            return participants.Any() ? Ok(participants) : NotFound();
        }

        // GET api/<ParticipantContoller>/?startDate?endDate
        [HttpGet]
        [Route("getWithDate")]
        public async Task<ActionResult<List<Participant>>> Get([FromQuery]DateTime startDate, [FromQuery] DateTime endDate)
        {
            var participants = await _participants
                .GetAllAsync(p => p.StartDate <= endDate && startDate <= (p.EndDate.Equals(null) ? DateTime.Now : p.EndDate));

            if (!participants.Any())
            {
                return NotFound();
            }
            return Ok(participants);
        }

        // GET api/<ParticipantContoller>/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Participant>> Get(Guid id)
        {
            var participants = await _participants
                .GetAsync(id);

            if (participants == null)
            {
                return NotFound();
            }
            return Ok(participants);
        }

        // POST api/<ParticipantContoller>
        [HttpPost]
        public async Task<ActionResult<Participant>> Post([FromBody] ParticipantDto participantDto)
        {
            var user = await _users.GetAsync(participantDto.userId);
            var department = await _departments.GetAsync(participantDto.departmentId);

            await _participants.CreateAsync(participantDto.ToParticipant(user, department));

            return Created();
        }

        // POST api/<ParticipantContoller>
        [HttpPost]
        [Route("transfer")]
        public async Task<ActionResult<Participant>> Post(Guid participantId, Guid depId)
        {
            var participant = await _participants.GetAsync(participantId);
            //if equal department
            if (participant.Department.Id == depId) return BadRequest();

            participant.EndDate = DateTime.Now;
            await _participants.UpdateAsync(participant);

            var department = await _departments.GetAsync(depId);
            var newParticipant = new Participant(participant.User, department);
            
            await _participants.CreateAsync(newParticipant);

            return Created();
        }

        // PUT api/<ParticipantContoller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Participant>> Put(Guid id, [FromBody] Participant participant)
        {
            await _participants.UpdateAsync(participant);
            
            return Accepted(participant);
        }

        // DELETE api/<ParticipantContoller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _participants.RemoveAsync(id);
            
            return NoContent();
        }
    }
}
