namespace Application.Contracts.Queries;
internal interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
