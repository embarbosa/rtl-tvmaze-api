using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RTL.TVMaze.ConsoleApp;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace RTL.TVMaze.Tests
{   
    public class TVMazeAPITest
    {        
        [Fact]
        public void TVShowTest()
        {
            //setup    
            string content = "[{\"id\":1,\"url\":\"http://www.tvmaze.com/shows/1/under-the-dome\",\"name\":\"Under the Dome\",\"type\":\"Scripted\",\"language\":\"English\",\"genres\":[\"Drama\",\"Science-Fiction\",\"Thriller\"],\"status\":\"Ended\",\"runtime\":60,\"premiered\":\"2013-06-24\",\"officialSite\":\"http://www.cbs.com/shows/under-the-dome/\",\"schedule\":{\"time\":\"22:00\",\"days\":[\"Thursday\"]},\"rating\":{\"average\":6.5},\"weight\":94,\"network\":{\"id\":2,\"name\":\"CBS\",\"country\":{\"name\":\"United States\",\"code\":\"US\",\"timezone\":\"America/New_York\"}},\"webChannel\":null,\"externals\":{\"tvrage\":25988,\"thetvdb\":264492,\"imdb\":\"tt1553656\"},\"image\":{\"medium\":\"http://static.tvmaze.com/uploads/images/medium_portrait/0/1.jpg\",\"original\":\"http://static.tvmaze.com/uploads/images/original_untouched/0/1.jpg\"},\"summary\":\"<p><b>Under the Dome</b> is the story of a small town that is suddenly and inexplicably sealed off from the rest of the world by an enormous transparent dome. The town's inhabitants must deal with surviving the post-apocalyptic conditions while searching for answers about the dome, where it came from and if and when it will go away.</p>\",\"updated\":1514029125,\"_links\":{\"self\":{\"href\":\"http://api.tvmaze.com/shows/1\"},\"previousepisode\":{\"href\":\"http://api.tvmaze.com/episodes/185054\"}}},{\"id\":2,\"url\":\"http://www.tvmaze.com/shows/2/person-of-interest\",\"name\":\"Person of Interest\",\"type\":\"Scripted\",\"language\":\"English\",\"genres\":[\"Drama\",\"Action\",\"Crime\"],\"status\":\"Ended\",\"runtime\":60,\"premiered\":\"2011-09-22\",\"officialSite\":\"http://www.cbs.com/shows/person_of_interest/\",\"schedule\":{\"time\":\"22:00\",\"days\":[\"Tuesday\"]},\"rating\":{\"average\":9.1},\"weight\":92,\"network\":{\"id\":2,\"name\":\"CBS\",\"country\":{\"name\":\"United States\",\"code\":\"US\",\"timezone\":\"America/New_York\"}},\"webChannel\":null,\"externals\":{\"tvrage\":28376,\"thetvdb\":248742,\"imdb\":\"tt1839578\"},\"image\":{\"medium\":\"http://static.tvmaze.com/uploads/images/medium_portrait/55/137682.jpg\",\"original\":\"http://static.tvmaze.com/uploads/images/original_untouched/55/137682.jpg\"},\"summary\":\"<p>You are being watched. The government has a secret system, a machine that spies on you every hour of every day. I know because I built it. I designed the Machine to detect acts of terror but it sees everything. Violent crimes involving ordinary people. People like you. Crimes the government considered \\\"irrelevant\\\". They wouldn't act so I decided I would. But I needed a partner. Someone with the skills to intervene. Hunted by the authorities, we work in secret. You'll never find us. But victim or perpetrator, if your number is up, we'll find you.</p>\",\"updated\":1526666883,\"_links\":{\"self\":{\"href\":\"http://api.tvmaze.com/shows/2\"},\"previousepisode\":{\"href\":\"http://api.tvmaze.com/episodes/659372\"}}}]";
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            message.Content = new StringContent(content, Encoding.UTF8, "application/json");

            var mockFakeHttp = new Mock<IFakeHttpClient>();
            mockFakeHttp.Setup(c => c.GetAsync("/shows")).Returns(Task.FromResult(message));

            //arrange
            var tvMazeAPI = new TVMazeAPI(string.Empty, mockFakeHttp.Object);

            //act 
            var taskResult = tvMazeAPI.GetTVShowsAsync();

            //assert
            Assert.NotNull(taskResult);
            Assert.Equal(2, taskResult.Result.Count);
            Assert.Equal("Under the Dome", taskResult.Result[0].Name);
            Assert.Equal("Person of Interest", taskResult.Result[1].Name);
        }

        [Fact]
        public void CastTest()
        {
            //setup    
            string content = "[{\"person\":{\"id\":9,\"url\":\"http://www.tvmaze.com/people/9/dean-norris\",\"name\":\"Dean Norris\",\"country\":{\"name\":\"United States\",\"code\":\"US\",\"timezone\":\"America/New_York\"},\"birthday\":\"1963-04-08\",\"deathday\":null,\"gender\":\"Male\",\"image\":{\"medium\":\"http://static.tvmaze.com/uploads/images/medium_portrait/0/2497.jpg\",\"original\":\"http://static.tvmaze.com/uploads/images/original_untouched/0/2497.jpg\"},\"_links\":{\"self\":{\"href\":\"http://api.tvmaze.com/people/9\"}}},\"character\":{\"id\":9,\"url\":\"http://www.tvmaze.com/characters/9/under-the-dome-james-big-jim-rennie\",\"name\":\"James \\\"Big Jim\\\" Rennie\",\"image\":{\"medium\":\"http://static.tvmaze.com/uploads/images/medium_portrait/0/2.jpg\",\"original\":\"http://static.tvmaze.com/uploads/images/original_untouched/0/2.jpg\"},\"_links\":{\"self\":{\"href\":\"http://api.tvmaze.com/characters/9\"}}}},{\"person\":{\"id\":1,\"url\":\"http://www.tvmaze.com/people/1/mike-vogel\",\"name\":\"Mike Vogel\",\"country\":{\"name\":\"United States\",\"code\":\"US\",\"timezone\":\"America/New_York\"},\"birthday\":\"1979-07-17\",\"deathday\":null,\"gender\":\"Male\",\"image\":{\"medium\":\"http://static.tvmaze.com/uploads/images/medium_portrait/0/1815.jpg\",\"original\":\"http://static.tvmaze.com/uploads/images/original_untouched/0/1815.jpg\"},\"_links\":{\"self\":{\"href\":\"http://api.tvmaze.com/people/1\"}}},\"character\":{\"id\":1,\"url\":\"http://www.tvmaze.com/characters/1/under-the-dome-dale-barbie-barbara\",\"name\":\"Dale \\\"Barbie\\\" Barbara\",\"image\":{\"medium\":\"http://static.tvmaze.com/uploads/images/medium_portrait/0/3.jpg\",\"original\":\"http://static.tvmaze.com/uploads/images/original_untouched/0/3.jpg\"},\"_links\":{\"self\":{\"href\":\"http://api.tvmaze.com/characters/1\"}}}}]";
            HttpResponseMessage message = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            message.Content = new StringContent(content, Encoding.UTF8, "application/json");

            string urlCast = string.Format("/shows/{0}/cast", 1);

            var mockFakeHttp = new Mock<IFakeHttpClient>();
            mockFakeHttp.Setup(c => c.GetAsync(urlCast)).Returns(Task.FromResult(message));

            //arrange
            var tvMazeAPI = new TVMazeAPI(string.Empty, mockFakeHttp.Object);

            //act 
            var taskResult = tvMazeAPI.GetCastAsync(1);

            //assert
            Assert.NotNull(taskResult);
            Assert.Equal(2, taskResult.Result.Count);
            Assert.Equal("Dean Norris", taskResult.Result[0].Person.Name);
            Assert.Equal("Mike Vogel", taskResult.Result[1].Person.Name);
        }
    }
}
