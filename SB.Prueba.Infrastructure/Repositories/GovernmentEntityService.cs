using SB.Prueba.Application.Interfaces;
using SB.Prueba.Domain.Entities;
using Serilog;

namespace SB.Prueba.Infrastructure.Repositories
{
    public class GovernmentEntityService : IGovernmentEntityService
    {
        private readonly string _filePath = "Data/governmentEntities.txt"; 
        private List<GovernmentEntity> _entities = new List<GovernmentEntity>();

        public GovernmentEntityService()
        {
            if (File.Exists(_filePath))
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(new char[] { ',' }, 3); 
                    if (parts.Length >= 3 && int.TryParse(parts[0], out int id))
                    {
                        _entities.Add(new GovernmentEntity
                        {
                            Id = id,
                            Nombre = parts[1].Trim(),
                            Descripcion = parts[2].Trim()
                        });
                    }
                }
            }
        }

        public async Task<List<GovernmentEntity>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public async Task<GovernmentEntity> GetByIdAsync(int id)
        {
            var entity = _entities.FirstOrDefault(e => e.Id == id);
            return await Task.FromResult(entity);
        }

        public async Task AddAsync(GovernmentEntity entity)
        {
            int newId = _entities.Count == 0 ? 1 : _entities.Max(e => e.Id) + 1;
            entity.Id = newId;
            _entities.Add(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(GovernmentEntity entity)
        {
            var existing = _entities.FirstOrDefault(e => e.Id == entity.Id);
            if (existing != null)
            {
                existing.Nombre = entity.Nombre;
                existing.Descripcion = entity.Descripcion;
                await SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = _entities.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                _entities.Remove(entity);
                await SaveChangesAsync();
            }
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                var lines = _entities.Select(e => $"{e.Id},{e.Nombre},{e.Descripcion}");
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath) ?? "Data");
                await File.WriteAllLinesAsync(_filePath, lines);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al guardar los cambios en el archivo.");
                throw;
            }
        }
    }
}
