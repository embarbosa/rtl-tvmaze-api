using System;
using Xunit;
using RTL.TVMaze.WebApi.Repositories;
using System.Linq;
using Moq;
using System.Collections.Generic;
using RTL.TVMaze.Data.Models;
using RTL.TVMaze.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using RTL.TVMaze.Data.Query;

namespace RTL.TVMaze.Tests
{
    public class TVShowRepositoryTest
    {
        [Fact]
        public void RepositoryReturnCastInOrderDescendingTest()
        {
            List<Cast> itemsCast = new List<Cast> {
                new Cast() { ID = 1, Name = "Name1", Birthday = DateTime.Now.AddYears(-10), TVShowId = 1 },
                new Cast() { ID = 2, Name = "Name2", Birthday = DateTime.Now.AddYears(-20), TVShowId = 2 },
                new Cast() { ID = 3, Name = "Name3", Birthday = DateTime.Now.AddYears(-30), TVShowId = 1 },
                new Cast() { ID = 4, Name = "Name4", Birthday = DateTime.Now.AddYears(-40), TVShowId = 2 },
            };


            List<TVShow> itemsTVShow = new List<TVShow> {
                new TVShow() { ID = 2, Name = "TVShow2", Cast = itemsCast.Where(c => c.TVShowId == 2).ToList() },
                new TVShow() { ID = 1, Name = "TVShow1", Cast = itemsCast.Where(c => c.TVShowId == 1).ToList() },
                
            };            

            var mockRepo = new Mock<ITVMazeQuery>();
            mockRepo.Setup(repo => repo.GetTVShows()).Returns(itemsTVShow);

            // Arrange           
            var repositoryTVShow = new TVShowRepository(mockRepo.Object);            

            // Act
            var resultTVShow = repositoryTVShow.Get().ToList();            

            // Assert;
            Assert.Equal(2, resultTVShow.Count);
            Assert.Equal("Name1", resultTVShow[0].Cast.ToList()[0].Name);
            Assert.Equal("Name2", resultTVShow[1].Cast.ToList()[0].Name);
        }
    }
}
