using AutoMapper;
using MediatR;
using VNExos.Common.DataTransferObject;
using VNExos.Common.Entity;
using VNExos.Common.Repository;

namespace VNExos.Common.Transferer;

public abstract class ACommonTransfererHandler<TEntity, TDto, TTransferer, TRepos>(IMapper mapper, TRepos repository) : IRequestHandler<TTransferer, TDto>
    where TEntity : CommonEntity
    where TDto : CommonDTO
    where TTransferer : CommonTransferer<TDto>
    where TRepos : ICommonRepository<TEntity>
{
    public async Task<TDto> Create(TTransferer request)
    {
        var entity = mapper.Map<TEntity>(request);
        var result = await repository.Create(entity);
        return mapper.Map<TDto>(result);
    }

    public async Task<TDto> Update(TTransferer request)
    {
        var entity = mapper.Map<TEntity>(request);
        var result = await repository.Update(entity);
        return mapper.Map<TDto>(result);
    }

    public async Task<TDto> GetById(Guid id)
    {
        var result = await repository.GetById(id);
        return mapper.Map<TDto>(result);
    }

    public async Task<bool> Delete(Guid id)
    {
        var result = await repository.Delete(id);
        return result;
    }

    public abstract Task<TDto> Handle(TTransferer request, CancellationToken cancellationToken);
}
