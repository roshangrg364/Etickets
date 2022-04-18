using CoreModule.Source.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public interface UserServiceInterface
    {
        Task Create(UserDto dto);
    }
}
