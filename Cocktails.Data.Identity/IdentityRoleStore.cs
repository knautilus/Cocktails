using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Cocktails.Data.Identity
{
    public class IdentityRoleStore : IRoleStore<Role>
    {
        #region Dependencies

        private readonly IRoleStorage<long> _storage;

        #endregion

        #region Constructor

        public IdentityRoleStore(IRoleStorage<long> storage) =>
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));

        #endregion

        #region IRoleStore

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken ct)
        {
            var res = await _storage.InsertAsync(role, ct);
            return res != null ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken ct)
        {
            var res = await _storage.UpdateAsync(role, ct);
            return res != null ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken ct)
        {
            await _storage.DeleteAsync(role, ct);
            return IdentityResult.Success;
        }


        public Task<string> GetRoleIdAsync(Role role, CancellationToken ct) =>
            Task.FromResult(role.Id.ToString());

        public Task<string> GetRoleNameAsync(Role role, CancellationToken ct) =>
            Task.FromResult(role.Name);

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken ct) =>
            Task.FromResult(role.Name.ToUpper());


        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken ct) =>
            throw new NotSupportedException();

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken ct) =>
            throw new NotSupportedException();


        public async Task<Role> FindByIdAsync(string roleId, CancellationToken ct)
        {
            if (!long.TryParse(roleId, out var id)) throw new ArgumentException(nameof(roleId));

            var role = await _storage.GetById(id, ct);

            return role;
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken ct) =>
            await _storage.GetByName(normalizedRoleName, ct);

        #endregion

        #region IDisposable

        public void Dispose() { }

        #endregion
    }
}
