using SmartSaver.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSaver.Domain.Repositories.Interfaces
{
    public interface IGenericRepository<TModel> where TModel : IdentityModelBase
    {
        Task<Guid> Create(TModel model);

        Task<TModel> GetById(Guid id);

        Task<IReadOnlyList<TModel>> GetAll();

        Task Update(TModel model);

        Task Delete(TModel toDelete);
    }
}