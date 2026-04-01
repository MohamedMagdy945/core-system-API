using MediatR;
using UniversitySystem.Application.ApplicationUser.Models;
using UniversitySystem.Application.Common.Bases;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Identity.User.Commands.CreateUser
{
    public record CreateUserCommand(string UserName, string Email, string Password, string ConfirmPassword) : IRequest<Response<CreateUserResponse>>;

    public class CreatedUserCommand : IRequestHandler<CreateUserCommand, Response<CreateUserResponse>>
    {
        private readonly IIdentityService _identityService;
        public CreatedUserCommand(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<Response<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var result = await _identityService.CreateUserAsync(request.UserName, request.Email, request.Password);

            if (!result.Result.Succeeded)
            {
                var errors = result.Result.Errors
                  .GroupBy(e => e.Code)
                  .ToDictionary(
                      g => g.Key,
                      g => g.Select(e => e.Description).ToList()
                  );
                return ResponseHandler.Failure<CreateUserResponse>("Failed to Create User", errors);
            }

            return ResponseHandler.Success(new CreateUserResponse { UserId = result.UserId }, "User Created Successfully");
        }
    }
}
