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
        if (IsList)
        {
            if (_results is null) throw new("Result has no value");
            onListQuery(_results);
        }
        else
        {
            if (_result is null) throw new("Result has no value");
            onQuery(_result);
        }
    }

    internal void Handle(Action<IQueryable<TResult>> onQuery)
    {
        if (IsList) throw new("Is a List");
        if (_result is null) throw new("Result has no value");
        onQuery(_result);
    }
}
