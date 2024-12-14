using AutoMapper;
using MediatR;
using VNExos.Common.DataTransferObject;
using VNExos.Common.Entity;
using VNExos.Common.Repository;

namespace VNExos.Common.Transferer;

public abstract class ACommonListTransfererHandler<TEntity, TDto, TTransferer, TRepos>(IMapper mapper, TRepos repository)
    : IRequestHandler<TTransferer, ICollection<TDto>>
    where TEntity : CommonEntity
    where TDto : CommonDTO
    where TTransferer : CommonListTransferer<TDto>
    where TRepos : ICommonRepository<TEntity>
{
    public async Task<ICollection<TDto>> GetAll()
    {
        var result = await repository.GetAll();
        return mapper.Map<ICollection<TDto>>(result);
    }

    public abstract Task<ICollection<TDto>> Handle(TTransferer request, CancellationToken cancellationToken);
}
