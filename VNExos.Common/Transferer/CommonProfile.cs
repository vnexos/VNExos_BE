using AutoMapper;
using VNExos.Common.DataTransferObject;
using VNExos.Common.Entity;

namespace VNExos.Common.Transferer;

public class CommonProfile<TEntity, TDto> : Profile
    where TEntity : CommonEntity
    where TDto : CommonDto
{
    public CommonProfile()
    {
        CreateMap<TEntity, TDto>();
    }
}
public class CommonProfile<TEntity, TDto, TTransferer1> : Profile
    where TEntity : CommonEntity
    where TDto : CommonDto
    where TTransferer1 : CommonTransferer<TDto>
{
    public CommonProfile()
    {
        CreateMap<TTransferer1, TEntity>();
        CreateMap<TEntity, TTransferer1>();
        CreateMap<TEntity, TDto>();
    }
}
public class CommonProfile<TEntity, TDto, TTransferer1, TTransferer2> : Profile
    where TEntity : CommonEntity
    where TDto : CommonDto
    where TTransferer1 : CommonTransferer<TDto>
    where TTransferer2 : CommonTransferer<TDto>
{
    public CommonProfile()
    {
        CreateMap<TTransferer1, TEntity>();
        CreateMap<TEntity, TTransferer1>();
        CreateMap<TTransferer2, TEntity>();
        CreateMap<TEntity, TTransferer2>();
        CreateMap<TEntity, TDto>();
    }
}

public class CommonProfile<TEntity, TDto, TTransferer1, TTransferer2, TTransferer3> : Profile
    where TEntity : CommonEntity
    where TDto : CommonDto
    where TTransferer1 : CommonTransferer<TDto>
    where TTransferer2 : CommonTransferer<TDto>
    where TTransferer3 : CommonTransferer<TDto>
{
    public CommonProfile()
    {
        CreateMap<TTransferer1, TEntity>();
        CreateMap<TEntity, TTransferer1>();
        CreateMap<TTransferer2, TEntity>();
        CreateMap<TEntity, TTransferer2>();
        CreateMap<TTransferer3, TEntity>();
        CreateMap<TEntity, TTransferer3>();
        CreateMap<TEntity, TDto>();
    }
}
public class CommonProfile<TEntity, TDto, TTransferer1, TTransferer2, TTransferer3, TTransferer4> : Profile
    where TEntity : CommonEntity
    where TDto : CommonDto
    where TTransferer1 : CommonTransferer<TDto>
    where TTransferer2 : CommonTransferer<TDto>
    where TTransferer3 : CommonTransferer<TDto>
    where TTransferer4 : CommonTransferer<TDto>
{
    public CommonProfile()
    {
        CreateMap<TTransferer1, TEntity>();
        CreateMap<TEntity, TTransferer1>();
        CreateMap<TTransferer2, TEntity>();
        CreateMap<TEntity, TTransferer2>();
        CreateMap<TTransferer3, TEntity>();
        CreateMap<TEntity, TTransferer3>();
        CreateMap<TTransferer4, TEntity>();
        CreateMap<TEntity, TTransferer4>();
        CreateMap<TEntity, TDto>();
    }
}
