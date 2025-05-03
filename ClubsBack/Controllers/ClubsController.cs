using ClubsBack.Entities;
using ClubsBack.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubsBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClubsController : ControllerBase
    {
        private IClubs _repository;
        public ClubsController(IClubs usersRepository)
        {
            _repository = usersRepository;
        }

        [HttpGet]
        public List<Club> Get()
        {
            return _repository.Get();
        }

        [HttpGet("{id:int}")]
        public ActionResult GetById([FromRoute] int id)
        {
            Club? result = _repository.GetById(id);

            if (result == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }


        public record CreateClubRequest(string Title, string Description);

        [HttpPost]
        [Route("create")]
        [Authorize]
        public ActionResult Post([FromBody] CreateClubRequest request)
        {
            var user = HttpContext.User.Identity.Name;

            if (_repository.CreateClub(new Club { Title = request.Title, Description = request.Description},int.Parse(user)) == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public ActionResult Delete([FromRoute] int id)
        {
            var user = HttpContext.User.Identity.Name;
            ClubUser clubUsers = new ClubUser { UserId = int.Parse(user), ClubId = id, IsAdmin = true };
            if (_repository.CheckUserOwnClub(clubUsers))
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
            else{
                return BadRequest("не прошел проверку на пользователя");
            }


        }
        public record ClubId(int clubId, int userId);
        [HttpPost]
        public ActionResult Signclub([FromBody] ClubUser clubId)
        {
            if (_repository.SignClub(clubId) == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("exit-club/{id:int}")]

        public ActionResult ExitClubs([FromRoute] int id)
        {
            if (_repository.ExitClub(id) == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
