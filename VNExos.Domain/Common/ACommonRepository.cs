using Microsoft.EntityFrameworkCore;
using VNExos.Common.Entity;
using VNExos.Common.Repository;
using VNExos.Domain.Presistence;

namespace VNExos.Domain.Common;

public class ACommonRepository<TEntity> : ICommonRepository<TEntity>
    where TEntity : CommonEntity
{
    protected VNExosContext context;
    protected DbSet<TEntity> dbSet;

    public ACommonRepository(VNExosContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        dbSet.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> Delete(Guid id)
    {
        var entity = await GetById(id);
        if (entity == null) return null;

        dbSet.Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> GetById(Guid id)
    {
        var conditions = GetByIdCondition();
        var res = await conditions
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync();
        return res;
    }

    protected DbSet<TEntity> GetByIdCondition()
    {
        return dbSet;
    }
    protected DbSet<TEntity> GetAllCondition()
    {
        return dbSet;
    }

    public async Task<TEntity?> Update(TEntity entity)
    {
        var existingEntity = await GetById(entity.Id);

        foreach (var property in typeof(TEntity).GetProperties())
        {
            var newValue = property.GetValue(entity);
            if (newValue != null && newValue.ToString() != Guid.Empty.ToString())
            {
                Console.WriteLine(newValue);
                property.SetValue(existingEntity, newValue);
            }
        }

        context.Entry(existingEntity!).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<ICollection<TEntity>> GetAll()
    {
        var condition = GetAllCondition();
        var res = await condition.ToListAsync();
        return res;
    }
}
