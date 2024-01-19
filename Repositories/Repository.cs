using Granthology.Data;
using Granthology.Models;
using System.Collections;

namespace Granthology.Repositories
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
    }
    public abstract class Repository<TModel> where TModel : RootModel
    {
        internal readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> Delete(TModel entity)
        {
            try
            {
                entity.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> Restore(TModel entity)
        {
            try
            {
                entity.DeletedAt = null;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public abstract IEnumerable FindAll(int page, int max, Func<TModel, bool>? filter, Func<TModel, dynamic>? order, bool orderAscending);

        public abstract Task<TModel?> FindById(int id);

        public virtual async Task<TModel> Insert(TModel entity)
        {
            entity.CreatedAt = DateTime.Now;
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> Update(TModel oldEntity, TModel newEntity)
        {
            if(oldEntity.Id == newEntity.Id)
            {
                oldEntity.LastUpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        


    }
}
