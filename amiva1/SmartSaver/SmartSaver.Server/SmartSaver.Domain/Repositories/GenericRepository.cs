using Microsoft.EntityFrameworkCore;
using SmartSaver.Domain.Models;
using SmartSaver.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSaver.Domain.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : IdentityModelBase
    {
        protected readonly SmartSaverContext Context;

        public GenericRepository(SmartSaverContext context)
        {
            Context = context;
        }

        protected DbSet<TModel> Set => Context.Set<TModel>();

        public async Task<Guid> Create(TModel model)
        {
            Set.Add(model);
            await Context.SaveChangesAsync();
            Context.Entry(model).State = EntityState.Detached;
            return model.Id;
        }

        public async Task<IReadOnlyList<TModel>> GetAll()
        {
            return await Set.ToListAsync();
        }

        public async Task<TModel> GetById(Guid id)
        {
            return await Set.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Update(TModel model)
        {
            Set.Update(model);
            await Context.SaveChangesAsync();
            Context.Entry(model).State = EntityState.Detached;
        }

        public async Task Delete(TModel toDelete)
        {
            Set.Remove(toDelete);
            await Context.SaveChangesAsync();
        }

    }
}