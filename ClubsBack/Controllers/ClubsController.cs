using ClubsBack.Entities;
using ClubsBack.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubsBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubsController : ControllerBase
    {
        private IClubsRepository _repository;
        public ClubsController(IClubsRepository usersRepository)
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
        
        [HttpPost("enter-club/{clubId:int}")]
        public ActionResult EnterClub([FromRoute] int clubId)
        {
            var user = HttpContext.User.Identity.Name;

            if (int.TryParse(user, out int userId) == false)
                return BadRequest();


            if (_repository.EnterClub(clubId, userId) == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("exit-club/{clubId:int}")]
        public ActionResult ExitClubs([FromRoute] int clubId)
        {
            var user = HttpContext.User.Identity.Name;

            if (int.TryParse(user, out int userId) == false)
                return BadRequest();

            if (_repository.ExitClub(clubId, userId) == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
