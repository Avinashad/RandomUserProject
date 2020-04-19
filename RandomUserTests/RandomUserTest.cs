using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using RandomUserCore;
using RandomUserCore.Models;
using RandomUserCore.Models.coreModels;
using RandomUserCore.Models.CoreModels;
using RandomUserCore.Models.IntegrationModels;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RandomUserTests
{
    public class RandomUserTest : Integration
    {

        [Fact]
        public async Task GetUserList_ShouldReturnListResultOfUser_WhenUsersExists()
        {
            //Act
            var response = await TestClient.GetAsync("api/user/get-user-list");
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<ListResult<User>>()).Should().NotBeNull();
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var ImageDetail = new ImageDetail()
            {
                Original = "https://randomuser.me/api/portraits/men/26.jpg",
                Thumbnail = "https://randomuser.me/api/portraits/thumb/men/26.jpg"
            };
            var incommingUser = new User()
            {
                Title = "Mr",
                FirstName = "Louis",
                LastName = "Brown",
                Email = Guid.NewGuid().ToString() + "@example.com",
                PhoneNumber = "(708)-841-6447",
                DateOfBirth = DateTime.Parse("1994-07-29T09:33:48.087"),
                ImageDetail = ImageDetail

            };

            var apiResponse = TestClient.PostAsync(
                "api/user",
                new StringContent(
                    JsonConvert.SerializeObject(incommingUser)
                    , Encoding.UTF8
                    , "application/json"
                    )).Result;

            var createResponse = JsonConvert.DeserializeObject<User>(
                await apiResponse.Content.ReadAsStringAsync());

            //Act
            var response = await TestClient.GetAsync("api/user/" + createResponse.Id);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var returnedUser = JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
            returnedUser.Id.Should().Be(createResponse.Id);
            returnedUser.FirstName.Should().Be("Louis");
            returnedUser.LastName.Should().Be("Brown");
        }

        [Fact]
        public async Task GetUserById_ShouldReturnNothing_WhenUserDoesNotExists()
        {
            // Arrange
            var userId = Guid.NewGuid();

            //Act
            var response = await TestClient.GetAsync("api/user/" + userId);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnCreatedUser_WhenRequiredUserValueIsPassed()
        {
            // Arrange

            var ImageDetail = new ImageDetail()
            {
                Original = "https://randomuser.me/api/portraits/men/26.jpg",
                Thumbnail = "https://randomuser.me/api/portraits/thumb/men/26.jpg"
            };
            var incommingUser = new User()
            {
                Title = "Mr",
                FirstName = "Louis",
                LastName = "Brown",
                Email = Guid.NewGuid().ToString() + "@example.com",
                PhoneNumber = "(708)-841-6447",
                DateOfBirth = DateTime.Parse("1994-07-29T09:33:48.087"),
                ImageDetail = ImageDetail

            };

            //Act

            var response = TestClient.PostAsync(
            "api/user",
            new StringContent(
                JsonConvert.SerializeObject(incommingUser)
                , Encoding.UTF8
                , "application/json"
                )).Result;

            var createResponse = JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            createResponse.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnNull_WhenRequiredUserValueIsNotPassed()
        {
            // Arrange

            var incommingUser = new User()
            {
                Email = Guid.NewGuid().ToString() + "@example.com",
                PhoneNumber = "(708)-841-6447",
                DateOfBirth = DateTime.Parse("1994-07-29T09:33:48.087")
            };

            //Act

            var response = TestClient.PostAsync(
            "api/user",
            new StringContent(
                JsonConvert.SerializeObject(incommingUser)
                , Encoding.UTF8
                , "application/json"
                )).Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnUpdatedUser_WhenValidUserValueIsPassed()
        {
            // Arrange

            var newUser = new User()
            {
                Title = "Mr",
                FirstName = "Aowi",
                LastName = "Lockwood",
                Email = Guid.NewGuid().ToString() + "@example.com",
                PhoneNumber = "(708)-841-6447",
                DateOfBirth = DateTime.Parse("1994-07-29T09:33:48.087"),
                ImageDetail = new ImageDetail()
                {
                    Original = "https://randomuser.me/api/portraits/men/28.jpg",
                    Thumbnail = "https://randomuser.me/api/portraits/thumb/men/28.jpg"
                }

            };
            var apiResponse = TestClient.PostAsync(
         "api/user",
         new StringContent(
             JsonConvert.SerializeObject(newUser)
             , Encoding.UTF8
             , "application/json"
             )).Result;

            var createResponse = JsonConvert.DeserializeObject<User>(
                await apiResponse.Content.ReadAsStringAsync());

            var updatedUser = createResponse;
            updatedUser.FirstName = "Paren";
            updatedUser.LastName = "Volon";
            updatedUser.ImageDetail.Original = "https://randomuser.me/api/portraits/men/22.jpg";


            //Act

            var response = TestClient.PutAsync(
            "api/user/update/" + createResponse.Id,
            new StringContent(
                JsonConvert.SerializeObject(updatedUser)
                , Encoding.UTF8
                , "application/json"
                )).Result;

            var updatedResponse = JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedResponse.Id.Should().Be(createResponse.Id);
            updatedResponse.FirstName.Should().Be(createResponse.FirstName);
            updatedResponse.LastName.Should().Be(createResponse.LastName);
            updatedResponse.ImageDetail.Original.Should().Be(createResponse.ImageDetail.Original);

        }
        [Fact]
        public async Task UpdateUser_ShouldReturnBadRequest_WhenInvalidUserValueIsPassed()
        {
            // Arrange

            var newUser = new User()
            {
                Title = "Mr",
                FirstName = "Aowi",
                LastName = "Lockwood",
                Email = Guid.NewGuid().ToString() + "@example.com",
                PhoneNumber = "(708)-841-6447",
                DateOfBirth = DateTime.Parse("1994-07-29T09:33:48.087"),
                ImageDetail = new ImageDetail()
                {
                    Original = "https://randomuser.me/api/portraits/men/28.jpg",
                    Thumbnail = "https://randomuser.me/api/portraits/thumb/men/28.jpg"
                }

            };
            var apiResponse = TestClient.PostAsync(
         "api/user",
         new StringContent(
             JsonConvert.SerializeObject(newUser)
             , Encoding.UTF8
             , "application/json"
             )).Result;

            var createResponse = JsonConvert.DeserializeObject<User>(
                await apiResponse.Content.ReadAsStringAsync());

            var updatedUser = createResponse;
            updatedUser.FirstName = null;
            updatedUser.LastName = null;
            updatedUser.ImageDetail.Original = null;


            //Act

            var response = TestClient.PutAsync(
            "api/user/update/" + createResponse.Id,
            new StringContent(
                JsonConvert.SerializeObject(updatedUser)
                , Encoding.UTF8
                , "application/json"
                )).Result;

            var updatedResponse = JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);


        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNoContent_WhenUserExist()
        {
            // Arrange

            var newUser = new User()
            {
                Title = "Mr",
                FirstName = "Aowi",
                LastName = "Lockwood",
                Email = Guid.NewGuid().ToString() + "@example.com",
                PhoneNumber = "(708)-841-6447",
                DateOfBirth = DateTime.Parse("1994-07-29T09:33:48.087"),
                ImageDetail = new ImageDetail()
                {
                    Original = "https://randomuser.me/api/portraits/men/28.jpg",
                    Thumbnail = "https://randomuser.me/api/portraits/thumb/men/28.jpg"
                }

            };
            var apiResponse = TestClient.PostAsync(
         "api/user",
         new StringContent(
             JsonConvert.SerializeObject(newUser)
             , Encoding.UTF8
             , "application/json"
             )).Result;

            var createUser = JsonConvert.DeserializeObject<User>(
                await apiResponse.Content.ReadAsStringAsync());


            //Act

            var response = TestClient.DeleteAsync(
            "api/user/" + createUser.Id).Result;


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);


        }
        [Fact]
        public async Task DeleteUser_ShouldReturnBadRequest_WhenUserDoesNotExist()
        {
            // Arrange

            var userId = Guid.NewGuid();


            //Act

            var response = TestClient.DeleteAsync(
            "api/user/" + userId).Result;


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);


        }

    }
}
