﻿using System;
using System.Threading.Tasks;

namespace RockLib.Encryption.Async
{
    /// <summary>
    /// An implementation of <see cref="IAsyncCrypto"/> that delegates all functionality
    /// to an instance of <see cref="ICrypto"/>. All tasks returned by its methods are
    /// guaranteed to be either completed or faulted.
    /// </summary>
    public sealed class SynchronousAsyncCrypto : IAsyncCrypto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronousAsyncCrypto"/> class.
        /// </summary>
        /// <param name="crypto">
        /// The instance of <see cref="ICrypto"/> that performs the actual work
        /// done by the methods of this instance of <see cref="SynchronousAsyncCrypto"/>.
        /// </param>
        public SynchronousAsyncCrypto(ICrypto crypto)
        {
            Crypto = crypto;
        }

        /// <summary>
        /// Gets the instance of <see cref="ICrypto"/> that performs the actual work
        /// done by the methods of this instance of <see cref="SynchronousAsyncCrypto"/>.
        /// </summary>
        public ICrypto Crypto { get; }

        /// <summary>
        /// Synchronously encrypts the specified plain text.
        /// <para>The <see cref="Task{TResult}"/> returned by this method is guaranteed
        /// to be either completed or faulted.</para>
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="keyIdentifier">
        /// An implementation-specific object used to identify the key for this
        /// encryption operation.
        /// </param>
        /// <returns>A completed task whose result represents the encrypted value as a string.</returns>
        public Task<string> EncryptAsync(string plainText, object keyIdentifier)
        {
            var completion = new TaskCompletionSource<string>();
            try
            {
                completion.SetResult(Crypto.Encrypt(plainText, keyIdentifier));
            }
            catch (Exception ex)
            {
                completion.SetException(ex);
            }
            return completion.Task;
        }

        /// <summary>
        /// Synchronously decrypts the specified cipher text.
        /// <para>The <see cref="Task{TResult}"/> returned by this method is guaranteed
        /// to be either completed or faulted.</para>
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="keyIdentifier">
        /// An implementation-specific object used to identify the key for this
        /// encryption operation.
        /// </param>
        /// <returns>A completed task whose result represents the decrypted value as a string.</returns>
        public Task<string> DecryptAsync(string cipherText, object keyIdentifier)
        {
            var completion = new TaskCompletionSource<string>();
            try
            {
                completion.SetResult(Crypto.Decrypt(cipherText, keyIdentifier));
            }
            catch (Exception ex)
            {
                completion.SetException(ex);
            }
            return completion.Task;
        }

        /// <summary>
        /// Synchronously encrypts the specified plain text.
        /// <para>The <see cref="Task{TResult}"/> returned by this method is guaranteed
        /// to be either completed or faulted.</para>
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="keyIdentifier">
        /// An implementation-specific object used to identify the key for this
        /// encryption operation.
        /// </param>
        /// <returns>A completed task whose result represents the encrypted value as a byte array.</returns>
        public Task<byte[]> EncryptAsync(byte[] plainText, object keyIdentifier)
        {
            var completion = new TaskCompletionSource<byte[]>();
            try
            {
                completion.SetResult(Crypto.Encrypt(plainText, keyIdentifier));
            }
            catch (Exception ex)
            {
                completion.SetException(ex);
            }
            return completion.Task;
        }

        /// <summary>
        /// Synchronously decrypts the specified cipher text.
        /// <para>The <see cref="Task{TResult}"/> returned by this method is guaranteed
        /// to be either completed or faulted.</para>
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="keyIdentifier">
        /// An implementation-specific object used to identify the key for this
        /// encryption operation.
        /// </param>
        /// <returns>A completed task whose result represents the decrypted value as a byte array.</returns>
        public Task<byte[]> DecryptAsync(byte[] cipherText, object keyIdentifier)
        {
            var completion = new TaskCompletionSource<byte[]>();
            try
            {
                completion.SetResult(Crypto.Decrypt(cipherText, keyIdentifier));
            }
            catch (Exception ex)
            {
                completion.SetException(ex);
            }
            return completion.Task;
        }

        /// <summary>
        /// Synchronously gets an instance of <see cref="IAsyncEncryptor"/> for the provided
        /// encrypt key.
        /// <para>The <see cref="Task{TResult}"/> returned by this method is guaranteed
        /// to be either completed or faulted.</para>
        /// </summary>
        /// <param name="keyIdentifier">
        /// An implementation-specific object used to identify the key for this
        /// encryption operation.
        /// </param>
        /// <returns>
        /// A completed task whose result represents an object that can be used for encryption operations.
        /// </returns>
        public Task<IAsyncEncryptor> GetEncryptorAsync(object keyIdentifier)
        {
            var completion = new TaskCompletionSource<IAsyncEncryptor>();
            try
            {
                completion.SetResult(new SynchronousAsyncEncryptor(Crypto.GetEncryptor(keyIdentifier)));
            }
            catch (Exception ex)
            {
                completion.SetException(ex);
            }
            return completion.Task;
        }

        /// <summary>
        /// Synchronously gets an instance of <see cref="IAsyncDecryptor"/> for the provided
        /// encrypt key.
        /// <para>The <see cref="Task{TResult}"/> returned by this method is guaranteed
        /// to be either completed or faulted.</para>
        /// </summary>
        /// <param name="keyIdentifier">
        /// An implementation-specific object used to identify the key for this
        /// encryption operation.
        /// </param>
        /// <returns>
        /// A completed task whose result represents an object that can be used for decryption operations.
        /// </returns>
        public Task<IAsyncDecryptor> GetDecryptorAsync(object keyIdentifier)
        {
            var completion = new TaskCompletionSource<IAsyncDecryptor>();
            try
            {
                completion.SetResult(new SynchronousAsyncDecryptor(Crypto.GetDecryptor(keyIdentifier)));
            }
            catch (Exception ex)
            {
                completion.SetException(ex);
            }
            return completion.Task;
        }

        /// <summary>
        /// Returns a value indicating whether this instance of <see cref="ICrypto"/>
        /// is able to handle the provided key identifier for an encrypt operation.
        /// </summary>
        /// <param name="keyIdentifier">The key identifier to check.</param>
        /// <returns>
        /// True, if this instance can handle the key identifier for an encrypt operation.
        /// Otherwise, false.
        /// </returns>
        public bool CanEncrypt(object keyIdentifier)
        {
            return Crypto.CanEncrypt(keyIdentifier);
        }

        /// <summary>
        /// Returns a value indicating whether this instance of <see cref="ICrypto"/>
        /// is able to handle the provided key identifier for an decrypt operation.
        /// </summary>
        /// <param name="keyIdentifier">The key identifier to check.</param>
        /// <returns>
        /// True, if this instance can handle the key identifier for an encrypt operation.
        /// Otherwise, false.
        /// </returns>
        public bool CanDecrypt(object keyIdentifier)
        {
            return Crypto.CanDecrypt(keyIdentifier);
        }
    }
}