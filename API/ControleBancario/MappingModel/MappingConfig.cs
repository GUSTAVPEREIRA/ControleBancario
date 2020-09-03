namespace ControleBancario.MappingModel
{
    using AutoMapper;

    public class MappingConfig
    {
        public MapperConfiguration GetMapperConfiguration()
        {
            var mappingConfig = new MapperConfiguration(mapper =>
            {
                mapper.AddProfile(new MappingUser());
            });

#if DEBUG
            mappingConfig.AssertConfigurationIsValid();
#endif

            return mappingConfig;

        }
    }
}