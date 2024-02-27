namespace Persistence.Common.Results;
public readonly struct Query<TResult>
{
    private bool IsList => _results is not null;
    private readonly IQueryable<TResult>? _result = null;
    private readonly IQueryable<List<TResult>>? _results = null;

    private Query(IQueryable<TResult> result)
    {
        _result = result;
    }

    private Query(IQueryable<List<TResult>>? results)
    {
        _results = results;
    }

    internal static Query<TResult> FromSingleResult(IQueryable<TResult> result) => new(result);
    internal static Query<TResult> FromListResult(IQueryable<List<TResult>> results) => new(results);

    internal void Handle(
            Action<IQueryable<TResult>> onQuery,
            Action<IQueryable<List<TResult>>> onListQuery
        )
    {
        if (!IsList)
        {
            if (_result is null) throw new("Result has no value");
            onQuery(_result);
            return;
        }
        if (_results is null) throw new("Result has no value");
        onListQuery(_results);
    }

    internal void Handle(Action<IQueryable<TResult>> onQuery)
    {
        if (IsList) throw new("Is a List");
        if (_result is null) throw new("Result has no value");
        onQuery(_result);
    }
}
