using HireMe_Backend.Controllers;
using HireMe_Backend.Models.DTOS;
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
                var userToPost = new RegisterUserDto() {Email="mockUser@email.com", Name="MockUser", Password="123" };

                //when
                await usersController.Post(userToPost);
                var readUser = dbContext.users.First();

                //then
                Assert.Equal(readUser.Name, userToPost.Name);
                Assert.Equal(readUser.Email, userToPost.Email);
                Assert.Equal(readUser.Password, usersController.HashUserPassword(userToPost.Password));
            }

        }
    }
}
