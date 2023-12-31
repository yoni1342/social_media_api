using AutoMapper;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Galacticos.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        public GenericRepository(ApiDbContext context)
        {
            _context = context;
        }
        public GenericRepository(IMapper mapper){
            _mapper = mapper;
        }

        public Guid Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return (Guid)entity.GetType().GetProperty("Id").GetValue(entity, null);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public Task<List<T>> GetAll()
        {
            return _context.Set<T>().ToListAsync();
        }

        public Task<T> GetById(Guid id)
        {
            var res = _context.Set<T>().FindAsync(id);
            return _mapper.Map<Task<T>>(res);
        }

        public Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}