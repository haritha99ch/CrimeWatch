namespace Persistence.Common.Results;
public readonly struct Query<TResult>
{
    private bool IsList => _results is not null;
    private readonly IQueryable<TResult>? _result = null;
    private readonly IQueryable<List<TResult>>? _results = null;

    public Query(IQueryable<TResult> result)
    {
        _result = result;
    }

    public Query(IQueryable<List<TResult>>? results)
    {
        _results = results;
    }

    internal void Handle(
            Action<IQueryable<TResult>> onQuery,
            Action<IQueryable<List<TResult>>> onListQuery
        )
    {
        if (IsList) onListQuery(_results ?? throw new InvalidOperationException("Results list is null."));
        else onQuery(_result ?? throw new InvalidOperationException("Result is null."));
    }

    internal void Handle(Action<IQueryable<TResult>> onQuery)
    {
        if (IsList) throw new InvalidOperationException("Cannot handle a list result.");
        onQuery(_result ?? throw new InvalidOperationException("Result is null."));
    }
}
