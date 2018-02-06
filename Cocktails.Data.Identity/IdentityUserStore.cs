using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocktails.Data.Domain;
using Microsoft.AspNetCore.Identity;

namespace Cocktails.Data.Identity
{
    public class IdentityUserStore : IUserPasswordStore<User>, IUserEmailStore<User>
    {
        #region Fields

        private bool _disposed;
        private readonly IUserStorage<long> _storage;

        #endregion

        #region Constructor

        public IdentityUserStore(IUserStorage<long> storage) =>
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));

        #endregion

        #region IUserStore

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken ct)
        {
            var res = await _storage.InsertAsync(user, ct);
            return res != null ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken ct)
        {
            var res = await _storage.UpdateAsync(user, ct);
            return res != null ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken ct)
        {
            await _storage.DeleteAsync(user, ct);
            return IdentityResult.Success;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken ct)
        {
            if (!long.TryParse(userId, out var id))
                throw new ArgumentException();

            var user = await _storage.GetById(id, ct);

            return user;
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken ct) =>
            await _storage.GetByName(normalizedUserName, ct);

        public Task<string> GetUserIdAsync(User user, CancellationToken ct) =>
            Task.FromResult(user.Id.ToString());

        public Task<string> GetUserNameAsync(User user, CancellationToken ct) =>
            Task.FromResult(user.UserName);

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken ct) =>
            (await GetUserNameAsync(user, ct)).ToUpper();

        public Task SetUserNameAsync(User user, string userName, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.UserName = userName;
            // TODO update user in db?
            return TaskCache.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken ct)
        {
            return TaskCache.CompletedTask;
        }

        #endregion

        #region IDIsposable

        public void Dispose()
        {
            _disposed = true;
        }

        #endregion

        #region IUserPasswordStore

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PasswordHash = passwordHash;
            return TaskCache.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PasswordHash != null);
        }

        #endregion

        #region IUserEmailStore

        /// <summary>
        /// Sets the <paramref name="email"/> address for a <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose email should be set.</param>
        /// <param name="email">The email to set.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Email = email;
            // TODO update user in db?
            return TaskCache.CompletedTask;
        }

        /// <summary>
        /// Gets the email address for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose email should be returned.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object containing the results of the asynchronous operation, the email address for the specified <paramref name="user"/>.</returns>
        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.EmailConfirmed = confirmed;
            // TODO update user in db?
            return TaskCache.CompletedTask;
        }

        /// <summary>
        /// Returns the normalized email for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose email address to retrieve.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation, the normalized email address if any associated with the specified user.
        /// </returns>
        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
            => (await GetEmailAsync(user, cancellationToken)).ToUpper();

        /// <summary>
        /// Sets the normalized email for the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user whose email address to set.</param>
        /// <param name="normalizedEmail">The normalized email to set for the specified <paramref name="user"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            return TaskCache.CompletedTask;
        }

        /// <summary>
        /// Gets the user, if any, associated with the specified, normalized email address.
        /// </summary>
        /// <param name="normalizedEmail">The normalized email address to return the user for.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation, the user if any associated with the specified normalized email address.
        /// </returns>
        public virtual Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return _storage.GetSingleAsync(x => x.Where(u => u.Email == normalizedEmail), cancellationToken);
        }

        #endregion

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }

    internal static class TaskCache
    {
        /// <summary>
        /// A <see cref="Task"/> that's already completed successfully.
        /// </summary>
        /// <remarks>
        /// We're caching this in a static readonly field to make it more inlinable and avoid the volatile lookup done
        /// by <c>Task.CompletedTask</c>.
        /// </remarks>
        public static readonly Task CompletedTask = Task.CompletedTask;
    }
}
