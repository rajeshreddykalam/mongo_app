﻿using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Core.SearchCore
{
    public interface ISearchRepository
    {
        #region "index"
        /// <summary>
        /// Index the searchDoc synchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>        
        /// <returns></returns>
        bool Index<T>(T document) where T : MXSearchDocument;

        /// <summary>
        /// Index the searchDoc asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>        
        /// <returns></returns>
        void IndexAsync<T>(T document) where T : MXSearchDocument;

        /// <summary>
        /// Batch Indexing the searchDocs synchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documents"></param>        
        /// <returns></returns>
        bool Index<T>(IList<T> documents) where T : MXSearchDocument;

        /// <summary>
        /// Batch Indexing the searchDocs asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="documents"></param>        
        /// <returns></returns>
        void IndexAsync<T>(IList<T> documents) where T : MXSearchDocument;

        #endregion

        #region "Get"
        /// <summary>
        /// Load a searchDoc by Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>        
        /// <param name="documentType"></param>
        /// <returns></returns>
        T GetOne<T>(string id, string documentType = "") where T : MXSearchDocument;

        /// <summary>
        /// This is a MultiGet based on the multiple IDs passed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>        
        /// <param name="documentType"></param>
        /// <returns></returns>
        IList<T> GetMany<T>(IEnumerable<string> ids, string documentType = "") where T : MXSearchDocument;

        /// <summary>
        /// This is lucene term query. Searching for a term on all fields.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="term"></param>
        /// <param name="skip">-1 here means take all</param>
        /// <param name="take"></param>
        /// <returns></returns>
        IList<T> Search<T>(string term, int skip = 0, int take = -1) where T : MXSearchDocument;

        #endregion

        #region "Update"

        /// <summary>
        /// Update a searchDoc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        bool Update<T>(T document) where T : MXSearchDocument;

        #endregion

        #region "Delete"

        bool Delete<T>(string id) where T : MXSearchDocument;

        void DeleteAsync<T>(string id) where T : MXSearchDocument;

        bool Delete<T>(IList<string> ids) where T : MXSearchDocument, new();

        void DeleteAsync<T>(IList<string> ids) where T : MXSearchDocument, new();

        #endregion

        #region "Index Level ops"

        bool CreateIndex(string index, IndexSettings settings);

        bool DeleteIndex(string index = "");

        #endregion
    }
}
