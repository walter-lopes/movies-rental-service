using Aloha.CQRS.Queries;
using MoviesRentalService.Application.Catalog.Queries;
using MoviesRentalService.Application.Catalog.Responses;
using MoviesRentalService.Domain.Catalog.Repositories;
using System.Threading.Tasks;

namespace MoviesRentalService.Application.Catalog.QueryHandlers
{
    public class GetMovieByIdQueryHandler : IQueryHandler<GetMovieByIdQuery, GetMovieByIdResponse>
    {
        private readonly IMovieRepository _repository;

        public GetMovieByIdQueryHandler(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetMovieByIdResponse> HandleAsync(GetMovieByIdQuery query)
        {
            var movie = await _repository.GetByIdAsync(query.Id);

            return new GetMovieByIdResponse(movie);
        }
    }
}
