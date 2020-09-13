namespace ControleBancario.Services.IService
{
    using ControleBancario.Model;
    using System.Threading.Tasks;
    using ControleBancario.Model.DTO;
    using System.Collections.Generic;

    public interface ISettingsService
    {
        Settings GetSettingsForID(int id);
        List<Settings> GetSettings(string filter, bool? isAdmin, bool? isManager, bool? isCreateUser);
        Task<Settings> CreateSettings(SettingsDTO dto);
        Task UpdateSettings(SettingsDTO dto);
        Task LogicDeleted(int id);
        Task UnsetLogigDeleted(int id);
        Task PhysicalDeleted(int id);
    }
}