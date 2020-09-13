namespace ControleBancario.Services.Service
{
    using System;
    using AutoMapper;
    using System.Linq;
    using ControleBancario.Model;
    using System.Threading.Tasks;
    using ControleBancario.Model.DTO;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using ControleBancario.Services.IService;

    public class SettingsService : ISettingsService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public SettingsService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Settings> CreateSettings(SettingsDTO dto)
        {
            var settings = _mapper.Map<SettingsDTO, Settings>(dto, new Settings(dto.Name));
            await _context.TbSettings.AddAsync(settings);
            await _context.SaveChangesAsync();

            return settings;
        }

        public List<Settings> GetSettings(string filter, bool? isAdmin, bool? isManager, bool? isCreateUser)
        {
            var settings = _context.TbSettings.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                settings = settings.Where(w => w.Name.Contains(filter));
            }

            if (isAdmin != null)
            {
                settings = settings.Where(w => w.IsAdmin == isAdmin);
            }

            if (isManager != null)
            {
                settings = settings.Where(w => w.IsManager == isManager);
            }

            if (isCreateUser != null)
            {
                settings = settings.Where(w => w.IsCreateUser == isCreateUser);
            }

            return settings.ToList();
        }

        public Settings GetSettingsForID(int id)
        {
            var setting = _context.TbSettings.Where(w => w.ID == id).AsNoTracking().FirstOrDefault();

            if (setting == null)
            {
                throw new Exception("Configuração não foi encontrada");
            }

            return setting;
        }

        public async Task LogicDeleted(int id)
        {
            var setting = this.GetSettingsForID(id);
            setting.SetLogicDeleted();
            _context.TbSettings.Add(setting).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task PhysicalDeleted(int id)
        {
            var setting = this.GetSettingsForID(id);          
            _context.TbSettings.Add(setting).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task UnsetLogigDeleted(int id)
        {
            var setting = this.GetSettingsForID(id);
            setting.UnsetLogicDeleted();
            _context.TbSettings.Add(setting).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSettings(SettingsDTO dto)
        {
            var setting = this.GetSettingsForID(dto.ID);
            setting = _mapper.Map(dto, setting);

            _context.TbSettings.Add(setting).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}