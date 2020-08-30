namespace ControleBancario.Services
{
    using ControleBancario.Services.Service;
    using ControleBancario.Services.IService;
    using Microsoft.Extensions.DependencyInjection;

    public class RegisterService
    {
        public RegisterService(ref IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}