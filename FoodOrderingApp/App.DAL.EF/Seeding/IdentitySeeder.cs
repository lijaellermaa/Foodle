using App.Domain.Identity;
using ErrorOr;
using Helpers.Base;
using Microsoft.AspNetCore.Identity;

namespace App.DAL.EF.Seeding;

public class IdentitySeeder(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
{
    public const string AdminRole = "Admin";

    public static AppUser AdminUser = GetAppUser("Admin", "User", "+37200000001", "Kalamaja 7, Tallinn");
    public static AppUser RegularUser = GetAppUser("Regular", "User", "+37200000002", "Kalamaja 8, Tallinn");

    private async Task<ErrorOr<Created>> CreateRoleAsync(string roleName)
    {
        if (await roleManager.RoleExistsAsync(AdminRole))
        {
            return Result.Created;
        }

        var res = await roleManager.CreateAsync(new AppRole { Name = roleName });
        if (!res.Succeeded)
            return Error.Failure("Failed to create a role", metadata: res.Errors.ToDictionary(x => x.Code, x => x.Description as object));
        return Result.Created;
    }

    private static string GetEmail(string firstName, string lastName) => $"{firstName.ToLower()}.{lastName.ToLower()}@example.ee";
    private static string GetPassword(string firstName, string lastName) => $"{firstName}.{lastName}.{firstName.Length}";
    private static AppUser GetAppUser(string firstName, string lastName, string phoneNumber, string address)
    {
        var email = GetEmail(firstName, lastName);
        return new AppUser
        {
            Id = Guid.NewGuid(),
            Email = email,
            UserName = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Address = address,
            EmailConfirmed = true
        };
    }

    private async Task<ErrorOr<AppUser>> CreateUserAsync(AppUser user)
    {
        var email = user.Email ?? GetEmail(user.FirstName, user.LastName);
        var existingUser = await userManager.FindByEmailAsync(email);
        if (existingUser != null) return existingUser;

        if (user.Id == Guid.Empty) user.Id = Guid.NewGuid();

        var password = GetPassword(user.FirstName, user.LastName);
        var userCreated = await userManager.CreateAsync(user, password);
        if (!userCreated.Succeeded)
            return Error.Failure("Failed to create a user", metadata: userCreated.Errors.ToDictionary(x => x.Code, x => x.Description as object));

        var newUser = await userManager.FindByEmailAsync(email);
        return newUser != null ? newUser : Error.Failure("Failed to fetch a new user");
    }

    private async Task<ErrorOr<AppUser>> CreateUserWithRoleAsync(AppUser user, string roleName)
    {
        var roleCreated = await CreateRoleAsync(roleName);
        if (roleCreated.IsError) return roleCreated.Errors;

        var createdUser = await CreateUserAsync(user);
        if (createdUser.IsError) return createdUser.Errors;

        var userRoles = await userManager.GetRolesAsync(createdUser.Value);
        if (userRoles.Contains(roleName)) return createdUser;

        var result = await userManager.AddToRoleAsync(createdUser.Value, roleName);
        return result.Succeeded ? user : Error.Failure("Failed to add role to a user");
    }

    public async Task SeedIdentityAsync()
    {
        var regularUser = await CreateUserAsync(RegularUser);
        regularUser.Switch(
        value => RegularUser = value,
        errors => {
            errors.ForEach(x => Console.WriteLine(x.ToString()));
        }
        );

        var adminUser = await CreateUserWithRoleAsync(AdminUser, AdminRole);
        adminUser.Switch(
        value => AdminUser = value,
        errors => {
            foreach (var error in errors)
            {
                Console.WriteLine(error.ToString());
                if (error.Metadata == null) continue;
                foreach (var metadata in error.Metadata)
                {
                    Console.WriteLine("{0}: {1}", metadata.Key, metadata.Value);
                }
            }
            errors.ForEach(x => Console.WriteLine(x.ToString()));
        }
        );
    }

    public void SeedIdentity() => AsyncHelper.RunSync(SeedIdentityAsync);
}