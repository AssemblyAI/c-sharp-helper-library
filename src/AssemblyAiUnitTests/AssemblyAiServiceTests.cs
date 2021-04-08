using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AssemblyAi;
using AssemblyAi.Common.Dtos;
using AssemblyAi.Common.Dtos.RequestModels;
using AssemblyAi.Common.Enums;
using AssemblyAi.Common.Helpers;
using AssemblyAi.Helpers;
using AssemblyAi.Helpers.Interfaces;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace AssemblyAiUnitTests
{
	public class AssemblyAiServiceTests
	{
		private HttpClient _httpClient;
		private Mock<HttpMessageHandler> _mockHandler;
		private AssemblyAiAccount _account;
		private string _jsonResult;
		private IServiceHelpers _helpers;
		private AssemblyAiService _sut;
		[SetUp]
		public void Setup()
		{
			
		
            
			//_expectedStringContentResult = new StringContent(_jsonResult, Encoding.UTF8, "application/json");

			
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
					Content = new StringContent(_jsonResult),
				})
				.Verifiable();
            
            
			_httpClient = new HttpClient(_mockHandler.Object)
			{
				BaseAddress = new Uri("http://test.com/"),
			};

			_helpers = new ServiceHelper(_httpClient, _account, TestModels.EnumJsonSerializeOptions);
			_sut = new AssemblyAiService(_account, _httpClient, _helpers);
		}

		[Test]
		public void Test1()
		{
			Assert.Pass();
		}
	}
}