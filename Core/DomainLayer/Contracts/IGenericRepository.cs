﻿using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        #region with specification
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification);
        Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification); 

        Task<int> CountAsync(ISpecification<TEntity, TKey> specification);
        #endregion
    }
}
