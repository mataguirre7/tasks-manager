using System.Text.RegularExpressions;
using API.Application.Authentication;
using API.Contracts.Accounts;
using API.Definitions.Services;
using API.Domain.Extended;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Accounts
{
    public class AccountAppService : TasksAppService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IMapper _objectMapper;
        private readonly JwtTokenService _jwtTokenService;
        public AccountAppService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            JwtTokenService jwtTokenService,
            IMapper objectMapper)
        {
            _objectMapper = objectMapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto usuarioDto)
        {
            var user = await _userManager.FindByEmailAsync(usuarioDto.Email!);

            if (user == null)
            {
                return BadRequest(new { Message = "Usuario y/o contraseña incorrecta" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, usuarioDto.Password!, lockoutOnFailure: false);

            var userName = user.Email!.Split('@')[0];

            if (result.Succeeded)
            {
                // Generar token
                var token = _jwtTokenService.GenerateToken(user.Id.ToString(), userName);

                // Configurar la cookie con el token
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None // Ajusta según tus necesidades
                };

                Response.Cookies.Append("token", token, cookieOptions);

                return Ok(new { Message = "Inicio de sesión exitoso.", Token = token, CurrentUser = user });
            }
            else
            {
                return BadRequest(new { Message = "Usuario y/o contraseña incorrecta" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto usuarioDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(usuarioDto.Email!);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "Este correo electrónico ya se encuentra registrado." });
            }

            var user = _objectMapper.Map<RegisterDto, ApplicationUser>(usuarioDto);

            // Hash y salting de la contraseña
            var hashedPassword = _passwordHasher.HashPassword(user, usuarioDto.Password!);
            user.PasswordHash = hashedPassword;

            var userNameFromEmail = user.Email!.Split('@')[0];

            var userName = Regex.Replace(userNameFromEmail, @"[^\w]", "");

            user.UserName = userName;
            user.NormalizedEmail = user.Email.ToUpper();
            user.NormalizedFullName = user.FullName.ToUpper();

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                // Generar token
                var token = _jwtTokenService.GenerateToken(user.Id.ToString(), user.UserName);

                // Configurar la cookie con el token
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append("token", token, cookieOptions);

                // Puedes iniciar sesión automáticamente si lo deseas
                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(new { Message = "Usuario registrado exitosamente.", Token = token, CurrentUser = user });
            }
            else
            {
                return BadRequest(new { Message = "Error al registrar el usuario.", Errors = result.Errors.Select(error => error.Description) });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Eliminar la cookie del token
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None // Ajusta según tus necesidades
            };

            Response.Cookies.Delete("token", cookieOptions);

            // Eliminar cualquier información del usuario almacenada en el cliente
            Response.Cookies.Delete("currentUser", cookieOptions);

            return Ok(new { Message = "Cierre de sesión exitoso." });
        }
    }
}