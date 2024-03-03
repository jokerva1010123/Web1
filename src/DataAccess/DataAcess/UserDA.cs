using Interface;
using Models;
using ApplicationDbContext;
namespace DataAcess
{
    public class UserDA : IUserDA
    {
        private readonly AppDbContext appContext;
        public UserDA(AppDbContext appContext)
        {
            this.appContext = appContext;
        }
        public async Task<User?> getUserFromLogin(string login)
        {
            foreach (User temp in this.appContext.Users)
                if (temp.Login == login)
                    return temp;
            return null;
        }
        public async Task<int> changePassword(int id_user, string newPass)
        {
            User? user = appContext.Users.Where(u => u.ID == id_user).FirstOrDefault();
            if (user == null)
                return 0;
            user.Password = newPass;
            appContext.SaveChanges();
            return 1;
        }
        public async Task<int> addUser(User user)
        {
            List<User>? lst = appContext.Users.Count() > 0 ? appContext.Users.ToList() : null;
            int maxid = 0;
            foreach (User temp in lst)
                if (temp.ID > maxid)
                    maxid = temp.ID;
            user.ID = maxid + 1;
            appContext.Users.Add(user);
            await appContext.SaveChangesAsync();
            return user.ID;
        }
    }
}
