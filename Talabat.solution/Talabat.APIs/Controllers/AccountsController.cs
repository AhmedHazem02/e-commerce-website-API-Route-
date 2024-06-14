using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using System.Net.Sockets;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Service;

namespace Talabat.APIs.Controllers
{
  
    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager
                               ,SignInManager<AppUser>signInManager
                               ,ITokenService tokenService
                                ,IMapper mapper)
        {
          _userManager = userManager;
          _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        // Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>>Register(RegisterDto Model)
        {
            if (CheckEmailExists(Model.Email).Result.Value) return BadRequest(new ApiResponse(400,"This Email Is Used"));

            var User = new AppUser()
            {
                DisplayName = Model.DisplayName,
                Email = Model.Email,
                UserName = Model.Email.Split('@')[0],
            };
            var Check=await _userManager.CreateAsync(User,Model.Password);
            if (!Check.Succeeded) return BadRequest(new ApiResponse(400));
            var Return = new UserDto()
            {
                Email = Model.Email,
                DisplayName = Model.DisplayName,
                Token =await _tokenService.CreateTokenAsync(User),
            };

            return Return; 
        }
        // Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto Model)
        {
            var User=await _userManager.FindByEmailAsync(Model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));
            var Res=await _signInManager.CheckPasswordSignInAsync(User,Model.Password,false);
            if (!Res.Succeeded) Unauthorized(new ApiResponse(401));
            var Return = new UserDto()
            {
                Email = User.Email,
                DisplayName = User.DisplayName,
                Token = await _tokenService.CreateTokenAsync(User),
            };

            return Return;

        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var Account = await _userManager.FindByEmailAsync(Email);
            var Return = new UserDto()
            {
                DisplayName = Account.DisplayName,
                Email = Email,
                Token = await _tokenService.CreateTokenAsync(Account)
            };
            return Ok(Return);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var user =await _userManager.GetCurrentUserAddress(User);
            var MappingAddress = _mapper.Map<Address, AddressDto>(user.address);
            return Ok(MappingAddress);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>>UpdataCurrentUserAddress(AddressDto Updatedaddress)
        {
            var user=await _userManager.GetCurrentUserAddress(User);
            var AddressMapped=_mapper.Map<AddressDto, Address>(Updatedaddress);
            AddressMapped.Id = user.address.Id;
            user.address = AddressMapped;
            var Res=await _userManager.UpdateAsync(user);
            if(!Res.Succeeded)return BadRequest(new ApiResponse(400));
            return Ok(Updatedaddress);
        }
        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
           return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
