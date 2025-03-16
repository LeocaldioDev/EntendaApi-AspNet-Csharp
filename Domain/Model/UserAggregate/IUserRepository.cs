using PrimeiraApi.Domain.DTOs;

namespace PrimeiraApi.Domain.Model.UserAggregate
{
    public interface IUserRepository
    {
        void Add(User user);

        List<UserDTO> Get(int pageNumber, int pageQuantity);

        User? Get(int id);

    }
}
