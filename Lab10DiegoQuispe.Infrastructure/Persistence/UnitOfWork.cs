using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;

namespace Lab10DiegoQuispe.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);
        if (_repositories.TryGetValue(type, out var repo))
        {
            return (IRepository<T>)repo;
        }

        var repositoryInstance = new Repositories.Repository<T>(_context);
        _repositories[type] = repositoryInstance;
        return repositoryInstance;
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
