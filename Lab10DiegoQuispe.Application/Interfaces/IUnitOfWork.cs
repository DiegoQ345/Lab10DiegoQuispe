using System;
using System.Threading.Tasks;

namespace Lab10DiegoQuispe.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : class;
    Task<int> SaveChangesAsync();
}
