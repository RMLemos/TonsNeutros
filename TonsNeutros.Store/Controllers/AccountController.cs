using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TonsNeutros.Admin.Models;
using TonsNeutros.Store.ViewModels;

namespace TonsNeutros.Store.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<Buyer> _userManager;
    private readonly SignInManager<Buyer> _signInManager;

    public AccountController(UserManager<Buyer> userManager, SignInManager<Buyer> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid)
            return View(loginVM);

        if (string.IsNullOrEmpty(loginVM.EmailAddress) || string.IsNullOrEmpty(loginVM.Password))
        {
            ModelState.AddModelError("", "Os campos são de preenchimento obrigatório");
            return View(loginVM);
        }

        var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(loginVM.ReturnUrl);
        }
        ModelState.AddModelError("", "Falha ao realizar o login");
        return View(loginVM);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel registerVM)
    {
        if (!ModelState.IsValid)
        {
            return View(registerVM);
        }

        // Verifica se o email está presente
        if (string.IsNullOrEmpty(registerVM.Email))
        {
            ModelState.AddModelError("Register", "Email is required.");
            return View(registerVM);
        }

        // Verifica se a senha está presente
        if (string.IsNullOrEmpty(registerVM.Password))
        {
            ModelState.AddModelError("Register", "Password is required.");
            return View(registerVM);
        }

        // Verifica se o usuário já existe
        var existingUser = await _userManager.FindByEmailAsync(registerVM.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError("Register", "Email already exists.");
            return View(registerVM);
        }

        // Cria o novo usuário
        var newUser = new Buyer
        {
            Email = registerVM.Email,
            UserName = registerVM.Email
        };

        var result = await _userManager.CreateAsync(newUser, registerVM.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, "Member");
            return RedirectToAction("Login", "Account");
        }
        else
        {
            ModelState.AddModelError("Registo", "Falha ao registar novo utilizador");
        }

        return View(registerVM);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
