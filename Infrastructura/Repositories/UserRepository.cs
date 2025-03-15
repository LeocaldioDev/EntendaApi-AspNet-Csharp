using PrimeiraApi.Domain.DTOs;
using PrimeiraApi.Domain.Model;
using PrimeiraApi.Infrastructura;

namespace PrimeiraApi.Infrastructura.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnection _userRepository = new DbConnection();
        public void Add(User user)
        {
            _userRepository.Users.Add(user);

            _userRepository.SaveChanges();
        }

        public List<UserDTO> Get(int pageNumber, int pageQuantity)
        {
            return _userRepository.Users
                .Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(b =>
                new UserDTO()
                {
                    Id = b.id,
                    NomeUser = b.nome,
                    Photo  = b.photo

                 })
                .ToList();
        }

        public User? Get(int id)
        {
            return _userRepository.Users.Find(id);
        }
    }
}
