using NetBanking.Core.Application.Dtos.Account;
using NetBanking.Core.Application.Dtos.User;
using NetBanking.Core.Application.ViewModels.User;

namespace NetBanking.Application.Interfaces.Services
{

    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task UpdateUser(SaveUserViewModel user);
        Task<UserDTO> GetUserByUserName(string UserName);
        Task<bool> IsaValidUser(string UserName);
        Task<List<UserDTO>> GetAllUsers();
    }
}