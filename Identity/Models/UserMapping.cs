using Application.Models.Auth;

namespace Identity.Models;

public static class UserMapping
{
    public static UserDto ToUserDto(this ApplicationUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email
        };
    }

    public static List<UserDto> ToUserDtoList(this IEnumerable<ApplicationUser> users)
    {
        return users.Select(user => user.ToUserDto()).ToList();
    }
}
