using Kay.Framework.Domain.Entities;
using Kay.Framework.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Domain.Repositories
{
    /// <summary>
    /// 仓储抽象接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IKeyEntity<TKey>
    {
        /// <summary>
        /// 根据组合主键查询实体数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// 获取所有实体，不建议使用
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> ListAll();

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity"></param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 修改实体,按列更新
        /// </summary>
        /// <param name="entity">更新实体</param>
        /// <param name="changedPropertyNames">需要更新的字段</param>
        void Update(TEntity entity, params string[] changedPropertyNames);

        /// <summary>
        /// 根据Id删除实体（软删除）
        /// </summary>
        /// <param name="id"></param>
        void Delete(TKey id);

        /// <summary>
        /// 删除实体（软删除）
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// 按照规约删除（软删除）
        /// </summary>
        /// <param name="spec"></param>
        void Delete(IBaseSpecification<TEntity> spec);

        /// <summary>
        /// 根据Id删除实体 物理删除
        /// </summary>
        /// <param name="id"></param>
        void DeleteForced(TKey id);

        /// <summary>
        /// 删除实体 物理删除
        /// </summary>
        /// <param name="entity"></param>
        void DeleteForced(TEntity entity);

        /// <summary>
        /// DeleteFromQuery 建议删除数据量小的场景下使用 物理删除
        /// </summary>
        /// <param name="spec"></param>
        void DeleteForced(IBaseSpecification<TEntity> spec);

        /// <summary>
        /// DeleteBatch 事务无关性，直接提交，强制删除
        /// </summary>
        void DeleteBatch(IBaseSpecification<TEntity> spec);

        #region 复杂查询

        /// <summary>
        /// 根据条件查询集合
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        IEnumerable<TEntity> List(IBaseSpecification<TEntity> spec);

        /// <summary>
        /// 根据条件查询单条实体
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        TEntity GetSingleBySpec(IBaseSpecification<TEntity> spec);

        /// <summary>
        /// 根据条件统计实体数量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        long Count(IBaseSpecification<TEntity> spec);

        #endregion
    }
}
