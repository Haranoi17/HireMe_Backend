using HireMe_Backend.Controllers;
using HireMe_Backend.Models.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HireMe_Backend.Tests
{
    public class UsersControllerUnitTests
    {
        [Fact]
        public async Task RegisterUserTest()
        {
            //given
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MockUsersDataBase").Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var usersController = new UsersController(dbContext);
                var userToPost = new RegisterUserDto() { Email = "mockUser@email.com", Name = "MockUser", Password = "12345" };

                //when
                await usersController.Post(userToPost);
                var readUser = dbContext.users.First();

                //then
                Assert.Equal(readUser.Name, userToPost.Name);
                Assert.Equal(readUser.Email, userToPost.Email);
                Assert.Equal(readUser.Password, usersController.HashUserPassword(userToPost.Password));
            }
        }

        [Fact]
        public async Task RegisterUserBadPasswordTest()
        {
            //given
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MockUsersDataBase").Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var usersController = new UsersController(dbContext);
                var userToPost = new RegisterUserDto() { Email = "mockUser@email.com", Name = "MockUser", Password = "1234" };
                var expectedResponse = new Dictionary<string, bool>{
                    {"email", true }, { "name", true}, { "password", false} };

                //when
                var result = await usersController.Post(userToPost) as BadRequestObjectResult;
                var resultResponse = result.Value as Dictionary<string, bool>;
                var differences = expectedResponse.Except(resultResponse);

                //then
                Assert.False(differences.Any());
            }
        }

        [Fact]
        public async Task RegisterUserWithAlreadyExistingEmailTest()
        {
            //given
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MockUsersDataBase").Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var usersController = new UsersController(dbContext);
                var userToPost = new RegisterUserDto() { Email = "mockUser@email.com", Name = "MockUser", Password = "12345" };

                var expectedResponse = new Dictionary<string, bool>{
                    {"email", false }, { "name", true}, { "password", true} };

                //when
                dbContext.users.Add(new Models.User() { Id = Guid.NewGuid(), Email = userToPost.Email, Name = "userName", Password = "123456" });
                await dbContext.SaveChangesAsync();

                var result = await usersController.Post(userToPost) as BadRequestObjectResult;
                var resultResponse = result.Value as Dictionary<string, bool>;
                var differences = expectedResponse.Except(resultResponse);

                //then
                Assert.False(differences.Any());
            }
        }

        [Fact]
        public async Task RegisterUserWithAlreadyExistingNameTest()
        {
            //given
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "MockUsersDataBase").Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var usersController = new UsersController(dbContext);
                var userToPost = new RegisterUserDto() { Email = "mockUser@email.com", Name = "MockUser", Password = "12345" };

                var expectedResponse = new Dictionary<string, bool>{
                    {"email", true}, { "name", false}, { "password", true} };

                //when
                dbContext.users.Add(new Models.User() { Id = Guid.NewGuid(), Email = "different@email.com", Name = userToPost.Name, Password = "123456" });
                await dbContext.SaveChangesAsync();

                var result = await usersController.Post(userToPost) as BadRequestObjectResult;
                var resultResponse = result.Value as Dictionary<string, bool>;
                var differences = expectedResponse.Except(resultResponse);

                //then
                Assert.False(differences.Any());
            }
        }
    }
}
