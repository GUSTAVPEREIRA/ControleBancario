namespace ControleBancario.Repositories
{
    using Microsoft.Extensions.DependencyInjection;

    public class RegisterRepositories
    {
        public RegisterRepositories(IServiceCollection repositories)
        {
            //repositories.AddScoped<IRepositorio, Repositorio>();
        }
    }
}