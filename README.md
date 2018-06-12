# RTL-TVMaze-Api Test

Project created using .net core 2.0 and EF 6.
API:

  - http://localhost:port/api/tvshows/1

# Solution

Create 3 dockers containers with following compose:
  - 1 - Image with SQL Server 2017;
  - 2 - Image with .net core and WebApi created
  - 3 - Image with .net core and schedule a crow job for run the Console Application created to update the Database. 