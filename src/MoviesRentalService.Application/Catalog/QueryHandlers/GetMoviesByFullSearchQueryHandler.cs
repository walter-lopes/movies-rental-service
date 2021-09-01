using Aloha.CQRS.Queries;
using MoviesRentalService.Application.Catalog.Queries;
using MoviesRentalService.Application.Catalog.Responses;
using MoviesRentalService.Domain.Catalog.Repositories;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Catalog.QueryHandlers
{
    public class GetMoviesByFullSearchQueryHandler : IQueryHandler<GetMoviesByFullSearchQuery, GetMoviesByFullSearchResponse>
    {
        private readonly IMovieRepository _repository;

        public GetMoviesByFullSearchQueryHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetMoviesByFullSearchResponse> HandleAsync(GetMoviesByFullSearchQuery query)
        {
            var movies = await _repository.FullSearchAsync(query.FullSearch);

            return new GetMoviesByFullSearchResponse(movies);
        }
    }
}
