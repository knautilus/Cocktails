using System;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Data.Domain;
using Microsoft.AspNetCore.Identity;

namespace Cocktails.Data.Identity
{
    // TODO: Refactor this, remove unnexxessary methods (or may be remove this class)
    // TODO: Use Psi.Domain.Integration.AspNetCore.Identity
    public class IdentityRoleStore : IRoleStore<Role>
    {
        #region Dependencies

        private readonly IRoleStorage _storage;

        #endregion

        #region Constructor

        public IdentityRoleStore(IRoleStorage storage) =>
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));

        #endregion

        #region IRoleStore

        public Task<IdentityResult> CreateAsync(Role role, CancellationToken ct) =>
            throw new NotSupportedException();

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken ct) =>
            throw new NotSupportedException();

        public Task<IdentityResult> DeleteAsync(Role role, CancellationToken ct) =>
            throw new NotSupportedException();


        public Task<string> GetRoleIdAsync(Role role, CancellationToken ct) =>
            Task.FromResult(role.Id.ToString());

        public Task<string> GetRoleNameAsync(Role role, CancellationToken ct) =>
            Task.FromResult(role.Name);

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken ct) =>
            Task.FromResult(role.Name);


        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken ct) =>
            throw new NotSupportedException();

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken ct) =>
            throw new NotSupportedException();


        public async Task<Role> FindByIdAsync(string roleId, CancellationToken ct)
        {
            if (!byte.TryParse(roleId, out var id)) throw new ArgumentException(nameof(roleId));

            var role = await _storage.GetById(id, ct);

            return role;
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken) =>
            (await _storage.GetByName(normalizedRoleName)).SingleOrDefault();

        #endregion

        #region IDisposable

        public void Dispose() { }

        #endregion
    }
}