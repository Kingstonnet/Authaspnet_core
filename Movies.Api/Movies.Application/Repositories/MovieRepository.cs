﻿using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movies = new List<Movie>();

        public Task<bool> CreateAsync(Movie movie)
        {
            _movies.Add(movie);
            return Task.FromResult(true);
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            var removecount=_movies.RemoveAll(x => x.Id == id);
            var movieremoved = removecount > 0;
           return Task.FromResult(movieremoved);
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            //return Task.FromResult<IEnumerable<Movie>>(_movies);
            return Task.FromResult(_movies.AsEnumerable());

        }

        public Task<Movie?> GetByIdAsync(Guid id)
        {
            var movie=_movies.SingleOrDefault(x => x.Id == id);
            return Task.FromResult(movie);
        }

        public Task<bool> UpdateAsync(Movie movie)
        {
            var movieindex=_movies.FindIndex((x=>x.Id== movie.Id));
            if(movieindex==-1)
            {
                return Task.FromResult(false);
            }
            _movies[movieindex] = movie;
            return Task.FromResult(true);
        }

        public Task<Movie?> GetBySlugAsync(string slug)
        {
            var movie = _movies.SingleOrDefault(x => x.Slug == slug);
            return Task.FromResult(movie);
        }
    }
}
