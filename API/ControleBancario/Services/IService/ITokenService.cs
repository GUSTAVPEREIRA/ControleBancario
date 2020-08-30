namespace ControleBancario.Services.IService
{
    using ControleBancario.Model;

    public interface ITokenService
    {       
        public string GenerateToken(User user);               
    }
}