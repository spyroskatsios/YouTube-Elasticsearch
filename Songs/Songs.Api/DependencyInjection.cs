using Elasticsearch.Net;
using Nest;

namespace Songs.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration.GetValue<string>("Elastic:Url");
        var index = configuration.GetValue<string>("Elastic:Index");
        var username = configuration.GetValue<string>("Elastic:Username");
        var password = configuration.GetValue<string>("Elastic:Password");

        var settings = new ConnectionSettings(new Uri(url!))
            .DefaultIndex(index)
            .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
            .BasicAuthentication(username, password);

        services.AddSingleton<IElasticClient>(new ElasticClient(settings));

        return services;
    }
    
    // docker network create elastic
    // docker pull docker.elastic.co/elasticsearch/elasticsearch:8.7.0
    // docker run --name elasticsearch --net elastic -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" -t docker.elastic.co/elasticsearch/elasticsearch:8.7.0
}