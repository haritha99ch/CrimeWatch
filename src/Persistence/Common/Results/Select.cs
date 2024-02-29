using System.Linq.Expressions;

namespace Persistence.Common.Results;
public class Select<TEntity, TResult>
{
    private readonly Expression<Func<TEntity, TResult>>? _select;
    private readonly Expression<Func<TEntity, List<TResult>>>? _selectFromList;

    private Select(Expression<Func<TEntity, TResult>> select)
    {
        _select = select;
        _selectFromList = null;
    }

    private Select(Expression<Func<TEntity, List<TResult>>> selectFromList)
    {
        _selectFromList = selectFromList;
        _select = null;
    }

    public static implicit operator Select<TEntity, TResult>(Expression<Func<TEntity, TResult>> select) => new(select);
    public static implicit operator Select<TEntity, TResult>(Expression<Func<TEntity, List<TResult>>> selectFromList)
        => new(selectFromList);

    public Query<TResult> Handle(
            Func<Expression<Func<TEntity, TResult>>, Query<TResult>> onSelect,
            Func<Expression<Func<TEntity, List<TResult>>>, Query<TResult>> onListSelect
        ) => _select is not null ? onSelect(_select) : onListSelect(_selectFromList!);
}
