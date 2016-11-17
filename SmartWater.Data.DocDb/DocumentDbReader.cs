using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using SmartWater.Domain.Core.Contracts.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SmartWater.Data.DocDb
{
    public abstract class DocumentDbReader 
    {
        private readonly Uri _collectionUri;
        private IConfigurationReader _settings;
        protected readonly string CollectionName;


        public DocumentDbReader(IConfigurationReader settings, string collectionName)
        {
            _settings      = settings;
            _collectionUri = GetCollectionUri(collectionName);
            CollectionName = collectionName;
        }

        protected async Task<IEnumerable<TEntity>> QueryFor<TEntity>(Expression<Func<TEntity, bool>> predicate, FeedOptions feedOptions)
        {
            var query = GetClient()
                .CreateDocumentQuery<TEntity>(_collectionUri, feedOptions)
                .Where(predicate)
                .AsDocumentQuery();

            var results = new List<TEntity>();
            var cancellationToken = new CancellationToken();

            while(query.HasMoreResults)
            {
                var nextResult = await query.ExecuteNextAsync<TEntity>(cancellationToken);
                results.AddRange(nextResult);
            }
            return results;
        }

        protected async Task<IEnumerable<TEntity>> SqlQueryFor<TEntity>(string sql, FeedOptions feedOptions)
        {
          var query = GetClient()
              .CreateDocumentQuery<TEntity>(_collectionUri, sql, feedOptions)
              .AsDocumentQuery();

          var results = new List<TEntity>();
          var cancellationToken = new CancellationToken();

          while (query.HasMoreResults)
          {
            var nextResult = await query.ExecuteNextAsync<TEntity>(cancellationToken);
            results.AddRange(nextResult);
          }
          return results;
        }

    protected Uri GetCollectionUri(string collectionName)
        {
            return UriFactory.CreateDocumentCollectionUri(
                _settings["DocumentDb.DatabaseName"],
                collectionName);
        }


        protected DocumentClient GetClient()
        {
            var serviceEndpointUri = new Uri(_settings["DocumentDb.DatabaseUri"]);
            return GetClient(serviceEndpointUri, _settings["DocumentDb.AuthorizationKey"]);
        }


        private DocumentClient GetClient(Uri serviceEndpoint, string authKey)
        {
            return new DocumentClient(serviceEndpoint, authKey, new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct
            });
        }
    }
}
