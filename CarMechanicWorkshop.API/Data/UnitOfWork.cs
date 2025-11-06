using CarMechanicWorkshop.API.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarMechanicWorkshop.API.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly CarMechanicWorkshopContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(CarMechanicWorkshopContext context)
    {
        _context = context;
    }

    public IRepository<T> Repository<T>() where T : class
    {
        return new Repositories.Repository<T>(_context);
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
            await _transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
