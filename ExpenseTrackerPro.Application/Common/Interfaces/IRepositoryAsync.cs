﻿using ExpenseTrackerPro.Domain.Contracts;

namespace ExpenseTrackerPro.Application.Common.Interfaces;

public interface IRepositoryAsync<T> where T : class, IEntity
{
    IQueryable<T> Entities { get; }
    Task<T> GetByIdAsync(int id);

    Task<List<T>> GetAllAsync();

    Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
