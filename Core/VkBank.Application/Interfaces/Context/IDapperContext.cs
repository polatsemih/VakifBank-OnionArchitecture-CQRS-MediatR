﻿using Microsoft.Data.SqlClient;

namespace VkBank.Application.Interfaces.Context
{
    public interface IDapperContext
    {
        public void Execute(Action<SqlConnection> @event);
        public Task ExecuteAsync(Func<SqlConnection, Task> action);
        public Task ExecuteAsync(Func<SqlConnection, Task> action, CancellationToken cancellationToken);

        public T Query<T>(Func<SqlConnection, T> query);
        public Task<T> QueryAsync<T>(Func<SqlConnection, Task<T>> query);
        public Task<T> QueryAsync<T>(Func<SqlConnection, Task<T>> query, CancellationToken cancellationToken);
    }
}
