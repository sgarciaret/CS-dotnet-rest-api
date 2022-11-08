using ApiRest.Model;

namespace ApiRest.Repository
{
    public interface IUsersSQLServer
    {
        Task<UserAPI> GiveUser(LoginAPI login);

    }
}
