﻿using Matrix.Core.MongoCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Matrix.Core.FrameworkCore
{
    /// <summary>
    /// This is a generic contract that defines most generic behavior 
    /// </summary>
    public interface IRepository
    {
        string Insert<T>(T entity) where T : IMXEntity;

        /// <summary>
        /// Batch Insert; suitable for a batch of 100 or less docs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns>List of IDs of the generated documents</returns>
        IList<string> Insert<T>(IList<T> entities) where T : IMXEntity;

        /// <summary>
        /// Bulk insert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns>List of IDs of the generated documents</returns>
        IList<string> BulkInsert<T>(IList<T> entities) where T : IMXEntity;

        /// <summary>
        /// Get one document by Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns>T</returns>
        T GetOne<T>(string id) where T : IMXEntity;

        /// <summary>
        /// Get many documents
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="predicate">predicate</param>        
        /// <param name="take">"-1" here would actually default ot 256 records</param>
        /// <param name="skip">skip count</param>
        /// <returns>IList<T></returns>
        IList<T> GetMany<T>(Expression<Func<T, bool>> predicate = null, int take = -1, int skip = 0) where T : IMXEntity;

        /// <summary>
        /// Updating a document. Please note that the version number is mandatory to be passed.
        /// Check the implementation in MXMongoRepository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update<T>(T entity) where T : IMXEntity;
                
        /// <summary>
        /// Delete by Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">Document Id</param>
        /// <returns></returns>
        bool Delete<T>(string id) where T : IMXEntity;

        /// <summary>
        /// Delete by Ids for a smaller batch size; 100 or so.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Delete<T>(IList<string> ids) where T : IMXEntity;
                
        //other important ones
        string GetNameById<T>(string Id) where T : IMXEntity;        

        /// <summary>
        /// Returns the count of records in a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">Optional value is null.</param>
        /// <returns></returns>
        long GetCount<T>(Expression<Func<T, bool>> predicate = null) where T : IMXEntity;

        /// <summary>
        /// Returns a single pair of DenormalizedReference type.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDenormalizedReference"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        TDenormalizedReference GetOptionById<TEntity, TDenormalizedReference>(string Id) 
            where TEntity : IMXEntity
            where TDenormalizedReference : IDenormalizedReference, new();

        /// <summary>
        /// Returns a list of DenormalizedReference types
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDenormalizedReference"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="take">"-1" here would actually default ot 256 records</param>
        /// <returns></returns>
        IList<TDenormalizedReference> GetOptionSet<TEntity, TDenormalizedReference>(Expression<Func<TEntity, bool>> predicate = null, int take = -1, int skip = 0)
            where TEntity : IMXEntity
            where TDenormalizedReference : IDenormalizedReference, new();

        /// <summary>
        /// drops the detabase
        /// </summary>
        /// <returns></returns>
        bool DropDatabase();

        /// <summary>
        /// Drops the collection. This same concept is there in other databases such as arango, raven etc. 
        /// Whereas couchbase and couchdb doesn't have explicit collections, but the same concept of clearing all documents of a particular types applies good.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        bool DropCollection(string collectionName);
    }
}
