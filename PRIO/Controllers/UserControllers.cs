using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Data;
using PRIO.DTOS;
using PRIO.Models;
using PRIO.Services;
using PRIO.ViewModels;

namespace PRIO.Controllers
{
    [ApiController]
    public class UserControllers : ControllerBase
    {
        #region Get
        [HttpGet("users")]
        public async Task<IActionResult> Get([FromServices] DataContext context)
        {
            var users = await context.Users.ToListAsync();
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                if (!user.IsActive)
                    continue;

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };
                userDTOs.Add(userDTO);
            }

            return Ok(userDTOs);

        }
        #endregion

        #region Create
        [HttpPost("users")]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModel body, [FromServices] DataContext context)
        {
            var userInDatabase = await context.Users.FirstOrDefaultAsync((x) => x.Email == body.Email);

            if (userInDatabase != null)
                return StatusCode(409, new { message = "User with this e-mail already exists." });

            try
            {
                var user = new User
                {
                    Name = body.Name,
                    Username = body.Username,
                    Email = body.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(body.Password),
                    CreatedAt = DateTime.Now,

                };

                await context.AddAsync(user);
                await context.SaveChangesAsync();

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                };


                return Created($"users/{user.Id}", userDTO);
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        #endregion

        #region Get By Id
        [HttpGet("users/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, [FromServices] DataContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == id);
            if (user == null || !user.IsActive)
                return NotFound();

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };


            return Ok(userDTO);
        }
        #endregion

        #region Update PATCH
        [HttpPatch("users/{id:Guid}")]
        public async Task<IActionResult> UpdatePartialAsync([FromRoute] Guid id, [FromBody] UpdateUserViewModel body, [FromServices] DataContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == id);

            if (user == null)
                return NotFound();

            try
            {
                if (body.Name != null) user.Name = body.Name;

                if (body.Password != null) user.Password = BCrypt.Net.BCrypt.HashPassword(body.Password);

                if (body.Email != null) user.Email = body.Email;

                if (body.Username != null) user.Username = body.Username;


                context.Users.Update(user);
                user.UpdatedAt = DateTime.Now;
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        #endregion


        #region Soft Delete
        [HttpDelete("users/{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, [FromServices] DataContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync((x) => x.Id == id);

            if (user == null || !user.IsActive)
                return NotFound();

            user.IsActive = false;
            user.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();

            return NoContent();
        }
        #endregion


        //#region Restore Login
        //[HttpPatch("restore/{userId:Guid}")]
        //public async Task<IActionResult> RestoreUserAsync([FromRoute] Guid userId, [FromServices] DataContext context)
        //{
        //    var user = await context.Users.FirstAsync((x) => x.Id == userId);

        //    if (user == null)
        //        return NotFound();

        //    try
        //    {
        //        user.IsActive = true;
        //        user.DeletedAt = null;

        //        context.Users.Update(user);
        //        await context.SaveChangesAsync();

        //        return Ok(new { message = "User successfully restored" });
        //    }
        //    catch
        //    {
        //        return StatusCode(500, new { message = "Internal Server Error" });
        //    }
        //}
        //#endregion

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(
        [FromBody] LoginViewModel body,
        [FromServices] DataContext context,
        [FromServices] TokenService tokenService)
        {
            var user = await context
                .Users
                .FirstOrDefaultAsync(x => x.Email == body.Email);

            if (user == null)
                return StatusCode(401, new { message = "E-mail or password invalid" });

            var passwordVerificationResult = BCrypt.Net.BCrypt.Verify(body.Password, user.Password);

            if (passwordVerificationResult)
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new { token });
            }

            return StatusCode(401, new { message = "E-mail or password invalid" });
        }

        #endregion
    }
}