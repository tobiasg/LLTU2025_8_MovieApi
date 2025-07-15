using Microsoft.EntityFrameworkCore;
using Movies.Core;
using Movies.Core.Entities;
using System.Linq.Expressions;

namespace Movies.Data.Repositories;

public abstract class BaseRepository<T>(ApplicationContext context) : IRepositoryBase<T> where T : EntityBase
{
    protected DbSet<T> DbSet { get; set; } = context.Set<T>();

    public IQueryable<T> FindAll(bool trackChanges = false)
    {
        return trackChanges ? DbSet : DbSet.AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        return trackChanges ? DbSet.Where(expression) : DbSet.Where(expression).AsNoTracking();
    }

    public void Create(T entity) => DbSet.Add(entity);

    public void Update(T entity) => DbSet.Update(entity);

    public void Delete(T entity) => DbSet.Remove(entity);
}