using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RTL.TVMaze.ConsoleApp;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using RTL.TVMaze.Data.Command;
using RTL.TVMaze.Data.Models;

namespace RTL.TVMaze.Tests
{   
    public class JobTest
    {        
        [Fact]
        public void Test()
        {
            //setup
            List<TVMazeTVShow> listTVShow = new List<TVMazeTVShow>();            
            TVMazeTVShow tvShow = new TVMazeTVShow() { ID = 1, Name = string.Format("Name{0},", 1) };
            listTVShow.Add(tvShow);            

            List<TVMazeCast> listCast = new List<TVMazeCast>();
            for (int i = 0; i < 10; i++)
            {                
                TVMazeCast cast = new TVMazeCast() {
                    Person = new TVMazePerson() { ID = i, Name = string.Format("Name{0},", i + 1) },
                    Character = new TVMazeCharacter() { ID = i, Name = string.Format("Name{0},", i + 1) }
                };

                listCast.Add(cast);
            }
            
            var mockTVMazeAPI = new Mock<ITVMazeAPI>();
            mockTVMazeAPI.Setup(c => c.GetTVShowsAsync())
                .Returns(Task.FromResult(listTVShow));
            mockTVMazeAPI.Setup(c => c.GetCastAsync(1))
               .Returns(Task.FromResult(listCast));            

            var mockTVMazeCommand = new Mock<ITVMazeCommand>();
            mockTVMazeCommand.Setup(c => c.AddTVShow(It.IsAny<TVShow>()));
            mockTVMazeCommand.Setup(c => c.AddCast(It.IsAny<Cast>()));

            //arrange            
            var job = new Job(string.Empty, mockTVMazeAPI.Object, mockTVMazeCommand.Object);

            //act
            job.Run();
        }
    }
}
