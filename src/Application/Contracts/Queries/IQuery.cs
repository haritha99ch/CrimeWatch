namespace Application.Contracts.Queries;

internal interface IQuery<TResponse> : IRequest<Result<TResponse>>;
