using BiblioTech.API.Entities;
using BiblioTech.API.MappingViewModels;
using BiblioTech.API.Models.InputModels;
using BiblioTech.API.Models.ViewModels;
using BiblioTech.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly BiblioTechDbContext _BiblioTechDbContext;

        public UsersController(BiblioTechDbContext BiblioTechDbContext)
        {
            _BiblioTechDbContext = BiblioTechDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAll()
        {
            var users = await _BiblioTechDbContext.Users.ToListAsync();

            var usersInputModel = users.ConvertUserToViewModel();

            return Ok(usersInputModel);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<UserViewModel>> GetById(int id)
        {
            var user = await _BiblioTechDbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            var userViewModel = user.ConvertUserByIdViewModel();

            return Ok(user);
        }

        [HttpGet("GetByCpf/{cpf}")]

        public async Task<ActionResult<UserViewModel>> GetByCpf(string cpf)
        {
            var user = await _BiblioTechDbContext.Users.SingleOrDefaultAsync(u => u.Cpf == cpf);

            if (user is null)
            {
                return NotFound();
            }

            var userViewModel = user.ConvertUserByIdViewModel();

            return Ok(user);
        }

        [HttpPost]

        public async Task<IActionResult> Post(UserInputModel userInputModel)
        {

            var user = new User(userInputModel.Cpf, userInputModel.Name, userInputModel.Email, default);

            await _BiblioTechDbContext.AddAsync(user);

            await _BiblioTechDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> Update(int id, UserInputModel userInputModel)
        {

            var user = await _BiblioTechDbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return BadRequest();
            }

            user.UpdateUser(userInputModel.Name, userInputModel.Email);
            
            await _BiblioTechDbContext.SaveChangesAsync();

            return Ok(user);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var user = _BiblioTechDbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return BadRequest();
            }

            _BiblioTechDbContext.Remove(user);

            await _BiblioTechDbContext.SaveChangesAsync();

            return Ok(user);

        }

        [HttpPost("blockUser/{id}")]

        public async Task<IActionResult> BlockUser(int id, int days)
        {
            var user = _BiblioTechDbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return BadRequest();
            }

            user.SetBlockedDate(DateTime.Now.AddDays(days));

            _BiblioTechDbContext.Update(user);

            await _BiblioTechDbContext.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("removeBlockUser/{id}")]

        public async Task<IActionResult> BlockUser(int id)
        {
            var user = _BiblioTechDbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return BadRequest();
            }

            user.RemoveBlockedDate();

            _BiblioTechDbContext.Update(user);

            await _BiblioTechDbContext.SaveChangesAsync();

            return Ok(user);
        }
    }
}
