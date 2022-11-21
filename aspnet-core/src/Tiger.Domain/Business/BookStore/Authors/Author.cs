/**
 * 类    名：Author   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 0:57:19       
 * 说    明: 
 * 
 */

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using TigerAdmin.Books;
using Tiger.Books.Demo;
using Tiger.Books;

namespace Tiger.Business.Demo
{
    /// <summary>
    /// 作者
    /// </summary>
    /// <remarks>
    /// 由 `FullAuditedAggregateRoot<Guid>` 继承使得实体支持[软删除]
    /// 
    /// </remarks>
    public class Author : FullAuditedAggregateRoot<Guid>
    {

        /// <summary>
        /// 名称
        /// </summary>
        /// <remarks>
        /// `Name` 属性的 `private set` 限制从类的外部设置这个属性
        /// </remarks>
        public string Name { get; private set; }
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        private Author()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        /// <summary>
        /// 当新建一个作者时, 通过构造器.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="birthDate"></param>
        /// <param name="shortBio"></param>
        internal Author(
            Guid id,
            [NotNull] string name,
            DateTime birthDate,
            [CanBeNull] string shortBio = null)
            : base(id)
        {
            SetName(name);
            BirthDate = birthDate;
            ShortBio = shortBio;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        ///  使用 `ChangeName` 方法更新名字.
        /// </remarks>
        internal Author ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            //`Check` 类是一个ABP框架工具类, 用于检查方法参数 (如果参数非法会抛出 `ArgumentException`).
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: AuthorConsts.MaxNameLength
            );
        }
    }
}
