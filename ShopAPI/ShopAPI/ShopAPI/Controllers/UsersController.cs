

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopAPI.Constants;
using ShopAPI.Models;
using ShopAPI.Models.Requests;
using ShopAPI.Models.Responses;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly OnlineShopContext _context;
        private readonly IConfiguration _iconfiguration;
        public UsersController(OnlineShopContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }
        //us.EmailAddress.Equals(userRequest.EmailAddress.Trim()) && us.Password.Equals(userRequest.Password.Trim()) &&
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserSignIn userRequest)
        {
            var result = (from a in _context.User
                          where (a.Status == 0 && a.EmailAddress.Equals(userRequest.EmailAddress) && a.Password.Equals(userRequest.Password))
                          select ( 
                            new UserDTO {
                              Id = a.Id, 
                              EmailAddress = a.EmailAddress, 
                              Role = a.Role,
                              UserName = a.UserName
                          })
                         ).SingleOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users/AddUser
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{user}")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                var existingUser = _context.User.Where(u => u.EmailAddress.Equals(user.EmailAddress)).FirstOrDefault();
                if (existingUser != null)
                {
                    return StatusCode(Constant.DUPLICATE_DATA, Constant.DUPLICATE_DATA_MESSAGE);
                }
                var fromEmailPassword = _iconfiguration.GetSection("MailAccount").GetSection("Password").Value;
                var fromEmailID = _iconfiguration.GetSection("MailAccount").GetSection("Id").Value;

                Common.Email.Send(fromEmailID, fromEmailPassword, user.EmailAddress, "", Constant.PORT, Constant.GMAIL_HOST, Constant.SUBJECT);

                return CreatedAtAction("GetUser", new { id = user.Id }, user);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }

            //_context.User.Add(user);
            //await _context.SaveChangesAsync();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

    }
}
