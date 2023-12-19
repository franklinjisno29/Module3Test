using GoRestAPI.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoRestAPI
{
    [TestFixture]
    internal class GoRestAPITests : CoreCodes
    {
        string token = "205ec63528ce9f80c1be09bd9eeb565b9494cf97a1bcee4603a787a4a3646474"; // token used for authorization of the API

        [Test, Order(1)]
        public void ListUsers()
        {
            test = extent.CreateTest("List Users");
            Log.Information("List Users Test Started"); //adding to log
            var listusersRequest = new RestRequest("users", Method.Get);
            listusersRequest.AddHeader("Content-Type", "application/json"); //Request header
            listusersRequest.AddHeader("Accept", "application/json");
            listusersRequest.AddHeader("Authorization", "Bearer "+token);
            var listusersResponse = client.Execute(listusersRequest);
            try
            {
                Assert.That(listusersResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK)); //Assertion
                Log.Information("API Response:" + listusersResponse.Content);
                List<UsersData> users = JsonConvert.DeserializeObject<List<UsersData>>(listusersResponse.Content);
                Assert.NotNull(users);
                Log.Information("Users Returned");
                Log.Information("List Users test passed all Asserts");
                test.Pass("List Users test passed all Assert"); //report created
            }
            catch (AssertionException)
            {
                test.Fail("List Users test Failed");
            }
        }

        [Test, Order(2)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User");
            Log.Information("Create User Test Started");
            var createUserRequest = new RestRequest("users", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddHeader("Accept", "application/json");
            createUserRequest.AddHeader("Authorization", "Bearer " + token);
            createUserRequest.AddJsonBody(new
            {
                name = "Tenali Ramakrish",
                gender = "male",
                email = "tenali.ramakrishna@34.com",
                status = "active"
            });  //requestbody
            var createUserResponse = client.Execute(createUserRequest);
            try
            {
                Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information("API Response:" + createUserResponse.Content);
                var user = JsonConvert.DeserializeObject<UsersData>(createUserResponse.Content);
                Assert.NotNull(user);
                Log.Information("User Created & Returned");
                Log.Information("Create User test passed all Asserts");
                test.Pass("Create User test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Create User test Failed");
            }
        }

        [Test, Order(3)]
        [TestCase(5839450)]  //parameterization
        public void UpdateUser(int uid)
        {
            test = extent.CreateTest("Update User");
            Log.Information("Update User Test Started");
            var updateUserRequest = new RestRequest("users/"+uid, Method.Put);
            updateUserRequest.AddHeader("Content-Type", "application/json");
            updateUserRequest.AddHeader("Accept", "application/json");
            updateUserRequest.AddHeader("Authorization", "Bearer " + token);
            updateUserRequest.AddJsonBody(new
            {
                name = "Allasani Peddana",
                email = "allasani.peddana@13.com",
                status = "active"
            });
            var updateUserResponse = client.Execute(updateUserRequest);
            try
            {
                Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + updateUserResponse.Content);
                var user = JsonConvert.DeserializeObject<UsersData>(updateUserResponse.Content);
                Assert.NotNull(user);
                Log.Information("User Updated & Returned");
                Log.Information("Update User test passed all Asserts");
                test.Pass("Update User test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Update User test Failed");
            }
        }

        [Test, Order(4)]
        [TestCase(5839450)]
        public void DeleteBooking(int uid)
        {
            test = extent.CreateTest("Delete User");
            Log.Information("Delete User Test Started");
            var deleteUserRequest = new RestRequest("users/"+uid, Method.Delete);
            deleteUserRequest.AddHeader("Content-Type", "application/json");
            deleteUserRequest.AddHeader("Accept", "application/json");
            deleteUserRequest.AddHeader("Authorization", "Bearer " + token); 
            var deleteUserResponse = client.Execute(deleteUserRequest);
            try
            {
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                Log.Information("User Deleted");
                Log.Information("Delete User test passed all Asserts");
                test.Pass("Delete User test passed all Assert");
            }
            catch (AssertionException)
            {
                test.Fail("Delete User test Failed");
            }
        }
    }
}
