using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AssemblyAi.Common.Enums;
using AssemblyAi.Common.Helpers;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using FluentAssertions;
using AssemblyAi.Helpers;
using AssemblyAi.Helpers.Interfaces;

namespace AssemblyAiUnitTests.Helpers
{
    [TestFixture]
    public class SubmitServiceHelpersTests
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _serializeOptions;
        private Mock<HttpMessageHandler> _mockHandler;
        private StringContent _expectedStringContentResult;

        private IServiceHelpers _sut;

        [SetUp]
        public void Setup()
        {

	        _expectedStringContentResult = new StringContent(TestModels.SubmitJsonResponse, Encoding.UTF8, "application/json");
            
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
		            Content = new StringContent(TestModels.SubmitJsonResponse),
	            })
	            .Verifiable();
            
            
            _httpClient = new HttpClient(_mockHandler.Object)
            {
	            BaseAddress = new Uri("http://test.com/"),
            };

            _serializeOptions = new JsonSerializerOptions
            {
	            Converters =
	            {
		            new EnumConvertor<AcousticModelEnum>(),
		            new EnumConvertor<BoostParamEnum>()
	            }
            };

            _sut = new ServiceHelper(_httpClient, TestModels.Account, _serializeOptions);
        }

        [Test]
        public void ConvertToStringContent_ShouldReturn_StringContent()
        {
	        var result = _sut.ConvertToStringContent(TestModels.SubmitTranscriptionRequest);

            result.Should().BeEquivalentTo(_expectedStringContentResult);
        }
        
        [Test]
        public async Task PostAsync_ShouldReturn_TranscriptionResponse()
        {
	        var result = await _sut.PostAsync(_expectedStringContentResult);

	        result.Should().BeEquivalentTo(TestModels.SubmitTranscriptionResponse);
        }
        
        [Test]
        public async Task SubmitAsync_ShouldReturn_TranscriptionResponse()
        {
	        var result = await _sut.SubmitAsync(TestModels.SubmitTranscriptionRequest);

	        result.Should().BeEquivalentTo(TestModels.SubmitTranscriptionResponse);
        }

       
    }
}