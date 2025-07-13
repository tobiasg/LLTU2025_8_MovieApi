using Bogus;
using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Data;

public class SeedData
{
    private static Faker faker = new Faker("sv");

    public static async Task InitAsync(ApplicationContext context)
    {
        var genres = GenerateGenres();
        await context.AddRangeAsync(genres);

        var movies = GenerateMovies(100, genres);
        await context.AddRangeAsync(movies);

        var actors = GenerateActors(movies);
        await context.AddRangeAsync(actors);

        var movieDetails = GenerateMovieDetails(movies);
        await context.AddRangeAsync(movieDetails);

        var reviews = GenerateReviews(movies);
        await context.AddRangeAsync(reviews);

        await context.SaveChangesAsync();
    }

    private static IEnumerable<Genre> GenerateGenres()
    {
        List<string> genres = [
            "Action",
            "Adventure",
            "Animation",
            "Biography",
            "Comedy",
            "Crime",
            "Documentary",
            "Drama",
            "Family",
            "Fantasy",
            "History",
            "Horror",
            "Music",
            "Musical",
            "Mystery",
            "Romance",
            "Sci-Fi",
            "Sport",
            "Thriller",
            "War",
            "Western"
        ];

        return genres.Select(genre => new Genre
        {
            Name = genre
        }).ToList();
    }

    private static IEnumerable<Movie> GenerateMovies(int count, IEnumerable<Genre> genres)
    {
        var movies = new List<Movie>();

        for (int i = 0; i < count; i++)
        {
            var movie = new Movie
            {
                Title = faker.Lorem.Sentence(3, 2),
                Year = faker.Date.Past(50).Year,
                Duration = faker.Random.Int(0, 250),
                Genres = []
            };

            foreach (var genre in faker.PickRandom(genres, faker.Random.Int(1, 5)).ToList())
            {
                movie.Genres.Add(genre);
            }

            movies.Add(movie);
        }

        return movies;
    }

    private static IEnumerable<Actor> GenerateActors(IEnumerable<Movie> movies)
    {
        var actors = new List<Actor>();

        for (int i = 0; i < 200; i++)
        {
            var actor = new Actor
            {
                Name = faker.Name.FullName(),
                BirthDate = faker.Date.Past(80),
                Movies = []
            };


            foreach (var movie in faker.PickRandom(movies, faker.Random.Int(1, 10)).ToList())
            {
                actor.Movies.Add(movie);
            }

            actors.Add(actor);
        }

        return actors;
    }

    private static IEnumerable<MovieDetails> GenerateMovieDetails(IEnumerable<Movie> movies)
    {
        var movieDetails = new List<MovieDetails>();

        foreach (var movie in movies)
        {
            movieDetails.Add(new MovieDetails
            {
                Movie = movie,
                Synopsis = faker.Lorem.Paragraph(),
                Language = faker.Random.String2(2),
                Budget = faker.Finance.Amount(100000, 100000000),
            });
        }

        return movieDetails;
    }

    private static IEnumerable<Review> GenerateReviews(IEnumerable<Movie> movies)
    {
        var reviews = new List<Review>();

        foreach (var movie in movies)
        {
            for (int i = 0; i < faker.Random.Int(0, 20); i++)
            {
                reviews.Add(new Review
                {
                    Rating = Math.Round(faker.Random.Double(1, 5), 1),
                    Name = faker.Name.FullName(),
                    Comment = faker.Lorem.Sentence(10, 5),
                    Movie = movie,
                });
            }
        }

        return reviews;
    }
}
