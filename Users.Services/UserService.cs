using DataEntities;
using DataStorage;
using FamilyDto;
using System;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.EntityFrameworkCore;

namespace Users.Services
{
    public class UserService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IServiceMapper _serviceMapper;

        public UserService(DatabaseContext dbContext, IServiceMapper serviceMapper)
        {
            _dbContext = dbContext;
            _serviceMapper = serviceMapper;
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Login == userDto.Login);
            if (user != null)
            {
                throw new ArgumentException($"Duplicated user with login {userDto.Login}");
            }

            var userEntityAdd = _serviceMapper.Map<UserDto, User>(userDto);

            await _dbContext.Users.AddAsync(userEntityAdd);
            await _dbContext.SaveChangesAsync();

            userDto = _serviceMapper.Map<User, UserDto>(userEntityAdd);
            return userDto;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NullReferenceException($"This user {user.Login} is not found");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<User> PatchUserAsync(UserDto userDto)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userDto.Id);

            if (user == null)
            {
                throw new NullReferenceException($"This user {user.Login} is not found");
            }

            var userMerge = _serviceMapper.Merge(userDto, user);
            await _dbContext.SaveChangesAsync();
            return userMerge;
        }
        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new NullReferenceException($"This user {user.Login} is not found");
            }

            return _serviceMapper.Map<User, UserDto>(user);
        }

        public IQueryable<UserDto> GetAllUser()
        {
            return _dbContext.Users.Select(x => _serviceMapper.Map<User, UserDto>(x));
        }
    }
}
