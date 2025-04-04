﻿using Library.Models;

namespace Library;

public interface IRepository<T> where T : class 
{
    void Insert(T entity);
    T GetById(int entityId);
    IEnumerable<T> GetAll();
    void Update(T entity);
    void Delete(int entityId);
    void Save();
}