namespace ControleBancario.MappingModel
{
    using AutoMapper;
    using ControleBancario.Model;
    using ControleBancario.Model.DTO;

    public class MappingSettings : Profile
    {
        //d = Destination, opt => options, s = Source
        public MappingSettings()
        {
            CreateMap<Settings, SettingsDTO>()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.IsAdmin, opt => opt.MapFrom(s => s.IsAdmin))
                .ForMember(d => d.IsCreateUser, opt => opt.MapFrom(s => s.IsCreateUser))
                .ForMember(d => d.IsManager, opt => opt.MapFrom(s => s.IsManager));

            CreateMap<SettingsDTO, Settings>()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.IsAdmin, opt => opt.MapFrom(s => s.IsAdmin))
                .ForMember(d => d.IsCreateUser, opt => opt.MapFrom(s => s.IsCreateUser))
                .ForMember(d => d.IsManager, opt => opt.MapFrom(s => s.IsManager))
                .ForMember(d => d.Users, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.DeletedAt, opt => opt.Ignore())
                .ForMember(d => d.Users, opt => opt.Ignore());                
        }
    }
}