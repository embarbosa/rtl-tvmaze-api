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
using RTL.TVMaze.Data.Command;
using System.Linq.Expressions;

namespace RTL.TVMaze.Tests
{
    public class TVMazeCommandTest
    {
        [Fact]
        public void TVShowInsertTest()
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
            var mockSetCast = new Mock<Microsoft.EntityFrameworkCore.DbSet<Cast>>();

            var mockContext = new Mock<RTLContext>();
            mockContext.Setup(repo => repo.TVShows).Returns(mockSetTVShow.Object);
            mockContext.Setup(repo => repo.Casts).Returns(mockSetCast.Object);

            var mockSet = new Mock<System.Data.Entity.DbSet<TVShow>>();
            mockSet.As<IQueryable<TVShow>>().Setup(m => m.Provider).Returns(itemsTVShow.Provider);
            mockSet.As<IQueryable<TVShow>>().Setup(m => m.Expression).Returns(itemsTVShow.Expression);
            mockSet.As<IQueryable<TVShow>>().Setup(m => m.ElementType).Returns(itemsTVShow.ElementType);
            mockSet.As<IQueryable<TVShow>>().Setup(m => m.GetEnumerator()).Returns(itemsTVShow.GetEnumerator());

            // Arrange           
            var tvMazeCommand = new TVMazeCommand(mockContext.Object);

            // Act
            foreach (var item in itemsTVShow)
            {
                tvMazeCommand.AddTVShow(item);
            }

            foreach (var item in itemsCast)
            {
                tvMazeCommand.AddCast(item);
            }
            
            mockSetTVShow.Verify(m => m.Add(It.IsAny<TVShow>()), Times.Exactly(2));
            mockSetCast.Verify(m => m.Add(It.IsAny<Cast>()), Times.Exactly(4));            
        }
    }
}
