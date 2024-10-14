using OnlineStore.Core.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Services.Contract.User
{
    public interface IUserService
    {
        Task<UserDTO> LoginAsync(LoginDTO login);
        Task<UserDTO> RegisterAsync(RegisterDTO register);
    }
}
