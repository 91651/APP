using System.Collections.Generic;
using System.Linq;
using APP.DbAccess.Repositories;
using APP.Framework.Services.Models;
using AutoMapper;

namespace APP.Framework.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public List<UserModel> GetUsers()
        {
            var users = _userRepository.GetAll().ToList();
            return _mapper.Map<List<UserModel>>(users);
        }
    }
}
