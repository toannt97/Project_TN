using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        #region Decalre variable
        private readonly OnlineShopContext _context;
        private readonly IConfiguration _iconfiguration;
        #endregion
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

        /// <summary>
        /// Verify user to allow signing in or not.
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns>
        /// </returns>
        //POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserSignIn userRequest)
        {

            var user = await _context.User.Where(u => u.Status == Constant.IS_ACTIVE && u.EmailAddress.Equals(userRequest.EmailAddress)
                                            && u.Password.Equals(userRequest.Password))
                                          .Include(c => c.ShoppingCart)
                                          .Select(u => new UserDTO
                                          {
                                              Id = u.Id,
                                              EmailAddress = u.EmailAddress,
                                              Role = u.Role,
                                              UserName = u.UserName,
                                              ItemsInCart = u.ShoppingCart.Count()
                                          }).FirstOrDefaultAsync();

            // Login information is not correct
            if (user == null)
            {
                return StatusCode(Constant.NOT_FOUND, Constant.NOT_FOUND_MESSAGE);
            }

            return user;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // PUT: api/Users/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        /// <summary>
        /// Create new user and send email to verify user's email
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="DbUpdateConcurrencyException">Thrown when write down data to table occurs error</exception>
        /// <exception cref="Exception">Thrown when occuring any exception except DbUpdateConcurrencyException</exception>
        /// <returns></returns>
        // POST: api/Users/AddUser
        [HttpPost("{user}")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try
            {
                if (UserExists(user.EmailAddress))
                {
                    return StatusCode(Constant.DUPLICATE_DATA, Constant.DUPLICATE_DATA_MESSAGE);
                }

                var fromEmailID = _iconfiguration.GetSection(Constant.MAIL_SECTION).GetSection("Id").Value;
                var fromEmailPassword = _iconfiguration.GetSection(Constant.MAIL_SECTION).GetSection("Password").Value;

                var baseUrl = string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host);

                // Set status for inactive user
                user.Status = -1;
                user.CreateDate = DateTime.Now;

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                // Get user id to send mail to verify user's email.
                var newUser = _context.User.Where(us => us.EmailAddress.Equals(user.EmailAddress)).SingleOrDefault();
                Common.Email.Send(fromEmailID, fromEmailPassword, user.EmailAddress, Constant.CONTENT(user.UserName, baseUrl, newUser.Id, user.EmailAddress)
                                 , Constant.PORT, Constant.GMAIL_HOST, Constant.SUBJECT_REGISTER_ACCOUNT);

                return StatusCode(Constant.OK, Constant.OK_MESSAGE);

            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(Constant.SQL_EXECUTION_ERROR, Constant.SQL_EXECUTION_MESSAGE);
            }
            catch (Exception)
            {
                return StatusCode(Constant.INTERNAL_ERROR, Constant.INTERNAL_MESSAGE);
            }
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

        private bool UserExists(string email)
        {
            return _context.User.Any(e => e.EmailAddress.Equals(email));
        }
        // GET: api/Users/

        [HttpGet]
        [Route("CreateUser")]
        public async Task<ActionResult> CreateUser(int id, string email)
        {
            try
            {
                var user = _context.User.Where(us => us.Id == id && us.EmailAddress.Equals(email)).SingleOrDefault();
                if (user.Status == -1)
                    user.Status = 0;
                else
                {
                    var contentHTMLDeny = System.IO.File.ReadAllText("./Template/Deny.html");
                    return base.Content(contentHTMLDeny, "text/html");
                }
                user.CreateDate = DateTime.Now;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                var contentHTMLSuccess = System.IO.File.ReadAllText("./Template/Success.html");
                return base.Content(contentHTMLSuccess, "text/html");
            }
            catch (SqlException)
            {
                return StatusCode(Constant.SQL_EXECUTION_ERROR, Constant.SQL_EXECUTION_MESSAGE);
            }
            catch (Exception)
            {
                return StatusCode(Constant.INTERNAL_ERROR, Constant.INTERNAL_MESSAGE);
            }
        }
    }
}
