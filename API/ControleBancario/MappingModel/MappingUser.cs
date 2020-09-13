namespace ControleBancario.MappingModel
{
    using AutoMapper;
    using ControleBancario.Model;
    using ControleBancario.Model.DTO;

    public class MappingUser : Profile
    {
        //d = Destination, opt => options, s = Source
        public MappingUser()
        {
            CreateMap<User, UserDTO>()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.FName, opt => opt.MapFrom(s => s.FName))
                .ForMember(d => d.LName, opt => opt.MapFrom(s => s.LName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                .ForAllOtherMembers(s => s.Ignore());

            CreateMap<UserDTO, User>()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.FName, opt => opt.MapFrom(s => s.FName))
                .ForMember(d => d.LName, opt => opt.MapFrom(s => s.LName))
                .ForMember(d => d.Email, opt => opt.Ignore())
                .ForMember(d => d.Password, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore())
                .ForMember(d => d.DeletedAt, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.Settings, opt => opt.Ignore())
                .AfterMap((s, d) =>
                {
                    d.SetPassword(s.Password);
                    d.SetEmail(s.Email);
                });

            CreateMap<UserAuthenticateDTO, User>()
                .ForMember(d => d.ID, opt => opt.Ignore())
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Username))
                .ForMember(d => d.FName, opt => opt.Ignore())
                .ForMember(d => d.LName, opt => opt.Ignore())
                .ForMember(d => d.Email, opt => opt.Ignore())
                .ForMember(d => d.Password, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore())
                .ForMember(d => d.DeletedAt, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.Settings, opt => opt.Ignore())
                .AfterMap((s, d) =>
                {
                    d.SetPassword(s.Password);
                });
        }
    }
}