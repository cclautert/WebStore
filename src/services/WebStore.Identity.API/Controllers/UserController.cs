using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Identity.API.ViewModels;

namespace WebStore.Identity.API.Controllers
{
    [Route("api/user")]
    public class UserController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UserController(IMapper mapper,
                                  IUserRepository userRepository,
                                  IUserService userService,
                                  INotifier notificator) : base(notificator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetAllAsync());
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserToken>> Register(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var usuario = await _userRepository.FindByEmailAsync(userViewModel.Email);
            if (usuario?.Email != null)
            {
                NotifyError("email exists!");
                return CustomResponse();
            }

            await _userService.CreateAsync(_mapper.Map<User>(userViewModel));

            var authToken =  _userService.GenerateToken(userViewModel.Email, userViewModel.Password);

            return new UserToken
            {
                Token = authToken
            };
        }

        [HttpPost("LogIn")]
        public async Task<ActionResult<UserToken>> LogIn(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _userService.AuthenticationAsync(userViewModel.Email, userViewModel.Password);

            if (result)
            {
                var authToken =  _userService.GenerateToken(userViewModel.Email, userViewModel.Password);

                return new UserToken
                {
                    Token = authToken
                };
            }

            NotifyError("Incorrect username or password");
            return CustomResponse();
        }
    }
}