using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEDProject.Models.Dtos;
using SEDProject.Models.Entities;
using SEDProject.Models.Extentions;
using SEDProject.Models.Repositories;

namespace SEDProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _repository;

        public UserController(IRepository<User> repository)
        {
            _repository = repository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IQueryable<User>>> Get()
        {
            var users = await _repository.GetAllAsync();
            if(users.Any())
            {
                return Ok(users);
            }

            return NotFound();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            var user = await _repository.GetAsync(id);

            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserDto userDto)
        {
            await _repository.CreateAsync(userDto.ToUser());

            return Created();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(Guid id, [FromBody] User user)
        {
            await _repository.UpdateAsync(user);
            
            return Accepted(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _repository.RemoveAsync(id);
            
            return NoContent();
        }
    }
}
