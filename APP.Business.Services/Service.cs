using APP.DbAccess.Repositories;
using AutoMapper;

namespace APP.Business.Services
{
    public class Service : IService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public Service(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        // todo
    }
}
