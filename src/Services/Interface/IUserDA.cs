using Models;
namespace Interface
{
    public interface IUserDA
    {
        public Task<User?> getUserFromLogin(string login);
        public Task<int> changePassword(int id_user, string newPass);
        public Task<int> addUser(User user);
    }
}
