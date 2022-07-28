using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MaiThikHu.EasyData.Abstractions;
using MaiThikHu.EasyData.Identity.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace MaiThikHu.EasyData.Identity
{
    public abstract class EasyIdentityRoleStoreBase<TKey, TRole, TUserRole, TRoleClaim> :
        IQueryableRoleStore<TRole>,
        IRoleClaimStore<TRole>
        where TKey : IEquatable<TKey>
        where TRole : class, IEasyIdentityRole<TKey>
        where TUserRole : IEasyIdentityUserRole<TKey>, new()
        where TRoleClaim : IEasyIdentityRoleClaim<TKey>, new()
    {
        protected readonly IEasyDatabase<TKey> _database;
        private bool disposedValue;

        public IQueryable<TRole> Roles { get; }

        public EasyIdentityRoleStoreBase(IEasyDatabase<TKey> database)
        {
            _database = database;

            Roles = _database.AsQueryable<TRole>();
        }

        public async Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken)
        {
            await _database.CreateAsync(role, cancellationToken: cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken)
        {
            await _database.UpdateAsync(role, cancellationToken: cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken)
        {
            await _database.DeleteAsync(role, cancellationToken: cancellationToken);

            return IdentityResult.Success;
        }

        public virtual TKey? ConvertIdFromString(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return default(TKey);
            }

            return (TKey?)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

        public async Task<TRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var findResult = await _database.FindOneAsync<TRole>(e => EqualityComparer<TKey>.Default.Equals(e.Id, ConvertIdFromString(roleId)), cancellationToken: cancellationToken);

            if (findResult.Result)
            {
                return findResult.EntityResult!;
            }

            return default(TRole)!;
        }

        public async Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var findResult = await _database.FindOneAsync<TRole>(e => e.NormalizedName == normalizedRoleName, cancellationToken: cancellationToken);

            if (findResult.Result)
            {
                return findResult.EntityResult!;
            }

            return default(TRole)!;
        }

        public Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName!);
        }

        public Task<string> GetRoleIdAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString()!);
        }

        public Task<string> GetRoleNameAsync(TRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name!);
        }

        public Task SetNormalizedRoleNameAsync(TRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(TRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

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
        // ~EasyIdentityRoleStoreBase()
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
    }
}
