
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AssemblyAi.Common.Dtos;
using AssemblyAi.Common.Enums;
using AssemblyAi.Common.Helpers;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using FluentAssertions;
using AssemblyAi.Helpers;
using AssemblyAi.Helpers.Interfaces;
using NUnit.Framework.Internal;

namespace AssemblyAiUnitTests.Helpers
{
    [TestFixture]
    public class RetrieveServiceHelpersTests
    {
        private HttpClient _httpClient;
        private Mock<HttpMessageHandler> _mockHandler;
        private IServiceHelpers _sut;
        
        
        [SetUp]
        public void Setup()
        { 
            _mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            
            _mockHandler
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(TestModels.RetrieveJsonResult),
                })
                .Verifiable();
            
            
            _httpClient = new HttpClient(_mockHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            

            _sut = new ServiceHelper(_httpClient, TestModels.Account, TestModels.EnumJsonSerializeOptions);
        }
        
        [Test]
        public async Task SuccessfulRetrieveAsync_ShouldReturn_OkStatusAndContentAndNoError()
        {
	       
            ServiceResponse<TranscriptionResponse> expectedResult =
                new ServiceResponse<TranscriptionResponse>
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    ErrorMessage = string.Empty,
                    Message = "Successful",
                    Content = TestModels.RetrieveTranscriptionResponse
                };
	        
            var result = await _sut.RetrieveAsync(TestModels.TranscriptionId);
	        
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}