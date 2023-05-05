using Elasticsearch.Net;
using Nest;

namespace Songs.Api.Elastic;

public interface ISearchService
{
    Task<ISearchResponse<ElasticSong>> SearchAsync(SearchParameters parameters,
        CancellationToken cancellationToken);
}

public class SearchService : ISearchService
{
    private readonly IElasticClient _elasticClient;

    public SearchService(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<ISearchResponse<ElasticSong>> SearchAsync(SearchParameters parameters,
        CancellationToken cancellationToken)
    {
        var result = await _elasticClient.SearchAsync<ElasticSong>(x => x
            .Query(q => q
                .Bool(b => b
                    .Should(s => s
                        .MultiMatch(m => m
                            .Fields(f => f
                                .Field(ff => ff.Title, boost: 2)
                                .Field(ff => ff.AlbumTitle)
                                .Field(ff => ff.ArtistName, boost: 3)
                            )
                            .Query(parameters.SearchText)
                            .Fuzziness(Fuzziness.Auto)
                        )
                    )
                    .MinimumShouldMatch(1)
                    .Filter(f => f
                        .Term(t => t.Genre, parameters.Genre)
                    )
                )
            )
            .Sort(s => s.Descending(SortSpecialField.Score))
            .Skip(parameters.Skip)
            .Take(parameters.Take
            ), cancellationToken);

        return result;
    }
}