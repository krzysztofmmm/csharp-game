using csharp_game.Data;
using csharp_game.Models;
using csharp_game.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class AuthEndpoints
{
    public static void ConfigureAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("auth");

        authGroup.MapPost("/register" , async (RegisterDto registerPayload , UserManager<ApplicationUser> userManager , TokenService tokenService) =>
        {
            var user = new ApplicationUser { UserName = registerPayload.Username , Email = registerPayload.Email };
            var result = await userManager.CreateAsync(user , registerPayload.Password);

            if(result.Succeeded)
            {
                return Results.Created($"/auth/{user.Id}" , new { Token = tokenService.CreateToken(user) });
            }
            return Results.BadRequest(result.Errors);
        });

        authGroup.MapPost("/login" , async (LoginDto loginPayload , UserManager<ApplicationUser> userManager , TokenService tokenService) =>
        {
            var user = await userManager.FindByNameAsync(loginPayload.Username);
            if(user != null && await userManager.CheckPasswordAsync(user , loginPayload.Password))
            {
                return Results.Ok(new { Token = tokenService.CreateToken(user) });
            }
            return Results.BadRequest("Invalid username or password.");
        });

        authGroup.MapGet("/{userId}/reset-password" , async (string userId , UserManager<ApplicationUser> userManager , TokenService tokenService , GameContext context) =>
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return Results.NotFound();
            }

            var token = await tokenService.CreatePasswordResetToken(user);
            return Results.Ok(new { UserId = userId , Token = token });
        });

        authGroup.MapPost("/{userId}/reset-password" , async (string userId , string token , ResetPasswordDto resetPasswordPayload , UserManager<ApplicationUser> userManager , GameContext context) =>
        {
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return Results.NotFound();
            }

            if(resetPasswordPayload.NewPassword != resetPasswordPayload.ConfirmPassword)
            {
                return Results.BadRequest("Passwords do not match.");
            }

            // Check the token is in the tokens table and is valid and not expired
            var storedToken = await context.Tokens.SingleOrDefaultAsync(t => t.UserId == userId && t.Value == token);
            if(storedToken == null || storedToken.ExpiryDate < DateTime.UtcNow)
            {
                return Results.BadRequest("Invalid or expired token.");
            }

            // Check the new password is different from the previous password
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            if(passwordHasher.VerifyHashedPassword(user , user.PasswordHash , resetPasswordPayload.NewPassword) != PasswordVerificationResult.Failed)
            {
                return Results.BadRequest("New password must be different from the previous password.");
            }

            var result = await userManager.ResetPasswordAsync(user , token , resetPasswordPayload.NewPassword);
            if(result.Succeeded)
            {
                // Delete the token
                context.Tokens.Remove(storedToken);
                await context.SaveChangesAsync();

                return Results.Ok();
            }
            return Results.BadRequest(result.Errors);
        });
    }
}
