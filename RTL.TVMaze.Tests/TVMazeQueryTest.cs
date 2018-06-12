using Microsoft.EntityFrameworkCore;
using Moq;
using RTL.TVMaze.Data.Mapping;
using RTL.TVMaze.Data.Models;
using RTL.TVMaze.Data.Query;
using RTL.TVMaze.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Data.Entity;

namespace RTL.TVMaze.Tests
{
    public class TVMazeQueryTest
    {
        [Fact]
        public void DisorderedQueryTest()
        {
            var itemsCast = new List<Cast> {
                new Cast() { ID = 1, Name = "Name1", Birthday = DateTime.Now.AddYears(-10), TVShowId = 1 },
                new Cast() { ID = 2, Name = "Name2", Birthday = DateTime.Now.AddYears(-20), TVShowId = 2 },
                new Cast() { ID = 3, Name = "Name3", Birthday = DateTime.Now.AddYears(-30), TVShowId = 1 },
                new Cast() { ID = 4, Name = "Name4", Birthday = DateTime.Now.AddYears(-40), TVShowId = 2 },
            }.AsQueryable(); 

            var itemsTVShow = new List<TVShow> {
                new TVShow() { ID = 2, Name = "TVShow2" },
                new TVShow() { ID = 1, Name = "TVShow1" },
                
            }.AsQueryable();

            var mockSetTVShow = new Mock<Microsoft.EntityFrameworkCore.DbSet<TVShow>>();
            mockSetTVShow.As<IQueryable<TVShow>>().Setup(m => m.Provider).Returns(itemsTVShow.Provider);
            mockSetTVShow.As<IQueryable<TVShow>>().Setup(m => m.Expression).Returns(itemsTVShow.Expression);
            mockSetTVShow.As<IQueryable<TVShow>>().Setup(m => m.ElementType).Returns(itemsTVShow.ElementType);
            mockSetTVShow.As<IQueryable<TVShow>>().Setup(m => m.GetEnumerator()).Returns(itemsTVShow.GetEnumerator());

            var mockSetCast = new Mock<Microsoft.EntityFrameworkCore.DbSet<Cast>>();
            mockSetCast.As<IQueryable<Cast>>().Setup(m => m.Provider).Returns(itemsCast.Provider);
            mockSetCast.As<IQueryable<Cast>>().Setup(m => m.Expression).Returns(itemsCast.Expression);
            mockSetCast.As<IQueryable<Cast>>().Setup(m => m.ElementType).Returns(itemsCast.ElementType);
            mockSetCast.As<IQueryable<Cast>>().Setup(m => m.GetEnumerator()).Returns(itemsCast.GetEnumerator());

            var mockContext = new Mock<RTLContext>();
            mockContext.Setup(repo => repo.TVShows).Returns(mockSetTVShow.Object);
            mockContext.Setup(repo => repo.Casts).Returns(mockSetCast.Object);

            // Arrange           
            var tvMazeQuery = new TVMazeQuery(mockContext.Object);

            // Act
            var result = tvMazeQuery.GetTVShows().ToList();   

            // Assert;
            Assert.Equal(2, result.Count);
            Assert.Equal("TVShow2", result[0].Name);
            Assert.Equal("TVShow1", result[1].Name);
            Assert.Equal("Name2", result[0].Cast.ToList()[0].Name);
            Assert.Equal("Name1", result[1].Cast.ToList()[0].Name);
        }
    }
}
