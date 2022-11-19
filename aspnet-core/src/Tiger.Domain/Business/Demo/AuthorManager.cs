/**
 * 类    名：AuthorManager   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:00:16       
 * 说    明: 
 * 
 */

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;

namespace Tiger.Business.Demo
{
    /// <summary>
    /// AuthorManager forces to create an author and change name of an author in a controlled way
    /// </summary>
    public class AuthorManager : DomainService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> CreateAsync(
            [NotNull] string name,
            DateTime birthDate,
            [CanBeNull] string shortBio = null)

        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingAuthor = await _authorRepository.FindByNameAsync(name);
            if (existingAuthor != null)
            {
                throw new AuthorAlreadyExistsException(name);
            }

            return new Author(
                GuidGenerator.Create(),
                name,
                birthDate,
                shortBio
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Author author,
            [NotNull] string newName)
        {
            Check.NotNull(author, nameof(author));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingAuthor = await _authorRepository.FindByNameAsync(newName);
            if (existingAuthor != null && existingAuthor.Id != author.Id)
            {
                throw new AuthorAlreadyExistsException(newName);
            }

            author.ChangeName(newName);
        }
    }
}
