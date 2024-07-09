using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEDProject.Models.Dtos;
using SEDProject.Models.Entities;
using SEDProject.Models.Extentions;
using SEDProject.Models.Repositories;

namespace SEDProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IRepository<Department> _repository;

        public DepartmentsController(IRepository<Department> repository)
        {
            _repository = repository;
        }

        // GET: api/<DepartmentController>
        [HttpGet]
        public async Task<ActionResult<IQueryable<Department>>> Get()
        {
            var users = await _repository.GetAllAsync();
            if (users.Any())
            {
                return Ok(users);
            }

            return NotFound();
        }

        // GET api/<DepartmentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(Guid id)
        {
            var user = await _repository.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<DepartmentController>/
        [HttpPost]
        public async Task<ActionResult<Department>> Post([FromBody] DepartmentDto departmentDto)
        {
            await _repository.CreateAsync(departmentDto.ToDepartment());

            return Created();
        }

        // PUT api/
        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> Put(Guid id, [FromBody] Department department)
        {
            await _repository.UpdateAsync(department);

            return Accepted(department);
        }

        // DELETE api/<DepartmentController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _repository.RemoveAsync(id);
            
            return NoContent();
        }
    }
}
