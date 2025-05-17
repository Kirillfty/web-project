using ClubsBack.Entities;
using ClubsBack.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ClubsBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IRepository<User> _repository;
        public UsersController(IRepository<User> usersRepository)
        {
            _repository = usersRepository;
        }

        [HttpGet]
        public List<User> Get()
        {
            return _repository.Get();
        }
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            if (_repository.Insert(user) != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult GetById([FromRoute] int id)
        {
            User? result = _repository.GetById(id);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpGet]
        [Route("name")]
        public ActionResult GetByName([FromQuery] string name)
        {
            User? result = _repository.GetByName(name);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromBody] int id)
        {
            if (_repository.Delete(id) == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public ActionResult Update([FromBody] User user)
        {
            if (_repository.Update(user) == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("get-users-in-club/{clubId}")]
        public ActionResult<List<User>> GetUsersInClubs([FromRoute] int clubId)
        {
            return _repository.GetUsersInClub(clubId);
        }
    }
}
