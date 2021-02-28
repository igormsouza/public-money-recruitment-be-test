using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Api.Models;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class PostAdminChangeTests
    {
        private readonly HttpClient _client;

        public PostAdminChangeTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostAdminChangePreparationTime_ThenAGetReturnsTheValue()
        {
            var request = new AdminReArangeViewModel
            {
                PreparationTimeIndays = 1
            };

            AdminReArangeResultViewModel postResult;
            using (var postResponse = await _client.PostAsJsonAsync($"/api/v1/admin", request))
            {
                Assert.True(postResponse.IsSuccessStatusCode);
            }

            var postRentalRequest = new RentalBindingModel
            {
                Units = 2
            };

            ResourceIdViewModel postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            var postBooking1Request = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = 1,
                Start = new DateTime(2021, 03, 01)
            };

            ResourceIdViewModel postBooking1Result;
            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
                postBooking1Result = await postBooking1Response.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            var postBooking2Request = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = 1,
                Start = new DateTime(2021, 03, 04)
            };

            ResourceIdViewModel postBooking2Result;
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
                postBooking2Result = await postBooking2Response.Content.ReadAsAsync<ResourceIdViewModel>();
            }

            var requestChangeCrash = new AdminReArangeViewModel
            {
                PreparationTimeIndays = 2
            };

            AdminReArangeResultViewModel postResultChangeCrash;
            await Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                using (var postResponse = await _client.PostAsJsonAsync($"/api/v1/admin", requestChangeCrash))
                {
                }
            });
        }
    }
}
