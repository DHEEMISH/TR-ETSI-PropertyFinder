using API;
using AutoMapper;
using CompanyEmployees.JwtFeatures;
using eLearning.Models;
using TRETSIPropertyFinderApi.DTO;
using eLearning_System.Interfaces;
using eLearning_System.Models;
using EventOrganizerAPI.CodeUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using TR_ETSI_PropertyFinderApi.Models;
using TRETSIPropertyFinderApi.DTO;
using TRETSIPropertyFinderApi.Models;
using CompanyEmployees.Entities.DataTransferObjects;
/// <summary>
/// Created by Shahid Riaz Bhatti
/// www.argumentexception.com
/// </summary>
namespace eLearning.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        /// <summary>
        /// private instance of User,SignIn Manager and ITokenService
        /// </summary>
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        private readonly JwtHandler _jwtHandler;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="tokenService"></param>
        /// /// <param name="mapper"></param>
        /// <param name="jwtHandler"></param>
        /// 
        public UserController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, IMapper mapper, JwtHandler jwtHandler )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
         
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">User to Create</param>
        /// <returns></returns>

        [HttpPost("Registration")]
        public async Task<IActionResult> Create([FromBody] UserForRegistrationDto userForRegistration)
        {
           

            if (ModelState.IsValid)
            {

                //Create User
              

                var user = _mapper.Map<ApplicationUser>(userForRegistration);
                user.Password = ConfigUtility.EncodePasswordToBase64(userForRegistration.Password);   
               
                IdentityResult identityResult = await _userManager.CreateAsync(user, userForRegistration.Password);
                var result = identityResult;
                //If user is created
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);

                    return BadRequest(new RegistrationResponseDto { Errors = errors });
                }
                else
                {
                    return Json(user);
                }
               

           

            }
            return BadRequest(ModelState);
        }
        /// <summary>
        /// Retrive user against the Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                return Json(user);
            else
                return BadRequest(new Exception("Email was not found"));
        }
        /// <summary>
        /// Used to Login and return the JWT Token
        /// </summary>
        /// <param name="signIn"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //    return Unauthorized(new AuthResponseDto { ErrorMessage = "Email is not confirmed" });
        //    user.Password = ConfigUtility.EncodePasswordToBase64(userForAuthentication.Password);
            if (!await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            {
               
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            }          

            var token = await _jwtHandler.GenerateToken(user);        
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }


        [HttpPost("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto externalAuth)
        {
            var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);
            if (payload == null)
                return BadRequest("Invalid External Authentication.");

            var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new ApplicationUser { Email = payload.Email, UserName = payload.Email };
                    await _userManager.CreateAsync(user);

                    //prepare and send an email for the email confirmation

               //     await _userManager.AddToRoleAsync(user, "Viewer");
                    await _userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }

            if (user == null)
                return BadRequest("Invalid External Authentication.");

            //check for the Locked out account

            var token = await _jwtHandler.GenerateToken(user);

            return Ok(new AuthResponseDto { Token = token, IsAuthSuccessful = true });
        }


    }
}
