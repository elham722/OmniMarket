namespace OmniMarket.Application.Features.Pagination.Queries;

public class GetPagedDataQueryHandler<T> : IRequestHandler<GetPagedDataQuery<T>, PagedList<T>>
    where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GetPagedDataQueryHandler(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<PagedList<T>> Handle(GetPagedDataQuery<T> request, CancellationToken cancellationToken)
    {
        return await _repository.GetPagedAsync(request.PageNumber, request.PageSize, null, false, null, cancellationToken);
    }
}