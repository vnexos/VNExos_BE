using VNExos.Common.DataTransferObject;
using VNExos.Common.Entity;

namespace VNExos.Common.Repository;

public interface ICommonRepository<TEntity>
    where TEntity : CommonEntity
{
    Task<TEntity> Create(TEntity entity);
    Task<TEntity?> GetById(Guid id);
    Task<ICollection<TEntity>> GetAll();
    Task<TEntity?> Update(TEntity entity);
    Task<TEntity?> Delete(Guid id);
}
