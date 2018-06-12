using System;
using Xunit;
using RTL.TVMaze.WebApi.Controllers;
using RTL.TVMaze.WebApi.Repositories;
using System.Linq;
using Moq;
using System.Collections.Generic;
using RTL.TVMaze.Data.Models;

namespace RTL.TVMaze.Tests
{
    public class APITest
    {
        [Fact]
        public void GetTVShowNoPaginationTest()
        {
            //setup 
            List<TVShow> items = new List<TVShow> {
                new TVShow() { ID = 1, Name = "TVShow1", Cast = null },
                new TVShow() { ID = 2, Name = "TVShow2", Cast = null },
                new TVShow() { ID = 3, Name = "TVShow3", Cast = null }
            };

            var mockRepo = new Mock<ITVShowsRepository>();
            mockRepo.Setup(repo => repo.Get()).Returns(items); 
            
            // Arrange           
            var controller = new TVShowsController(mockRepo.Object);

            // Act
            var result = controller.Get(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetTVShowWithPaginationWhenTenOrMoreItemsTest()
        {
            //setup 
            List<TVShow> items = new List<TVShow>();
            for (int i = 0; i < 20; i++)
            {
                items.Add(new TVShow() { ID = i, Name = "TVShow" + i, Cast = null });
            }

            var mockRepo = new Mock<ITVShowsRepository>();
            mockRepo.Setup(repo => repo.Get()).Returns(items); 
            
            // Arrange           
            var controller = new TVShowsController(mockRepo.Object);

            // Act
            var result = controller.Get(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public void GetTVShowWithPaginationWhenTenOrMoreItemsPage2Test()
        {
            //setup 
            List<TVShow> items = new List<TVShow>();
            for (int i = 0; i < 19; i++)
            {
                items.Add(new TVShow() { ID = i, Name = "TVShow" + i, Cast = null });
            }

            var mockRepo = new Mock<ITVShowsRepository>();
            mockRepo.Setup(repo => repo.Get()).Returns(items);

            // Arrange           
            var controller = new TVShowsController(mockRepo.Object);

            // Act
            var result = controller.Get(2);

            // Assert            
            Assert.Equal(9, result.Count());
            Assert.Equal(10, result.ToList()[0].ID);
        }
    }
}
