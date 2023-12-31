using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.Persistence.Contracts
{
    public interface INewsFeedRepository
    {
        Task<List<Post>> GetNewsFeedForUser(Guid userId, int pageNumber, int pageSize);
    }
}