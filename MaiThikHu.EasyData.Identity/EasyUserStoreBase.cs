using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaiThikHu.EasyData.Abstractions;
using MaiThikHu.EasyData.Identity.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace MaiThikHu.EasyData.Identity
{
    public abstract class EasyUserStoreBase<TUser> :
        IUserStore<TUser>,
        IUserEmailStore<TUser>,
        IQueryableUserStore<TUser>
        where TUser : class, IEasyIdentityUser
    {
        protected readonly IEasyDatabase<string> _database;

        #region Disposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~EasyUserStoreBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public IQueryable<TUser> Users { get; }

        protected EasyUserStoreBase(IEasyDatabase<string> database)
        {
            _database = database;

            Users = _database.AsQueryable<TUser>();
        }

        public virtual async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _database.CreateAsync(user, cancellationToken: cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var findResult = await _database.FindOneAsync<TUser>(e => e.Id == userId, cancellationToken: cancellationToken);

            if (findResult.Result)
            {
                return findResult.EntityResult!;
            }

            return default(TUser)!;
        }

        public async Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var findResult = await _database.FindOneAsync<TUser>(e => e.UserName == normalizedUserName, cancellationToken: cancellationToken);

            if (findResult.Result)
            {
                return findResult.EntityResult!;
            }

            return default(TUser)!;
        }

        public virtual async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _database.UpdateAsync(user, cancellationToken: cancellationToken);

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _database.DeleteAsync(user, cancellationToken: cancellationToken);

            return IdentityResult.Success;
        }

        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;

            return Task.CompletedTask;
        }

        public Task SetEmailAsync(TUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public async Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var findResult = await _database.FindOneAsync<TUser>(e => e.NormalizedEmail == normalizedEmail, cancellationToken: cancellationToken);

            if (findResult.Result)
            {
                return findResult.EntityResult!;
            }

            return default(TUser)!;
        }

        public Task<string> GetNormalizedEmailAsync(TUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(TUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }
    }
}
