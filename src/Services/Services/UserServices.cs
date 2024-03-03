using Exceptions;
using Interface;
using Models;

namespace Services
{
    public class UserServices
    {
        protected IUserDA userDA;
        public UserServices(IUserDA userDA)
        {
            this.userDA = userDA;
        }
        public async Task<User?> login(string login, string password)
        {

            User? user = await getUserFromLogin(login);
            if (user == null)
                throw new LoginNotFoundException();
            if (user.Password == password)
                return user;
            throw new IncorrectPasswordExcept();
        }
        public async Task<User?> getUserFromLogin(string login)
        {
            User? user = await userDA.getUserFromLogin(login);
            if (user == null)
                throw new LoginNotFoundException();
            return user;
        }
        public async Task<int> changePassword(int id_user, string password, string oldPass, string newPass, string repeatePass)
        {
            if(string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(password))
                throw new InputInvalidException();
            if (password != oldPass)
                throw new OldPasswordWrongException();
            if (newPass != repeatePass)
                throw new RepeateNewPassWordWrongException();
            if (await userDA.changePassword(id_user, newPass) == 0)
                throw new DataBaseError();
            return 1;
        }
        public async Task<int> checkLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new InputInvalidException();
            User? user = await userDA.getUserFromLogin(login);
            if (user != null)
                throw new LoginExistsException();
            return 1;
        }
        public async Task<int> addUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new InputInvalidException();
            return await userDA.addUser(new User { Login = login, Password = password, Level = Levels.STUDENT});
        }
    }
}
