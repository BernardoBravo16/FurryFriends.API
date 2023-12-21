﻿using FurryFriends.Application.Shared.Contracts.Persistence;

namespace FurryFriends.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _database;

        public UnitOfWork(IDatabaseContext databaseContext)
        {
            _database = databaseContext;
        }

        public void Save()
        {
            _database.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _database.SaveChangesAsync();
        }
    }
}