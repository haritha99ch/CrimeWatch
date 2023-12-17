namespace Application.Contracts.Commands;
internal interface ICommand<TResponse> : IRequest<Result<TResponse>>;

internal interface ICommand : IRequest<Result>;
