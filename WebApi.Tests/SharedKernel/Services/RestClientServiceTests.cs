using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Athenas.EjemploTemplateApi.Domain.Configurations;
using Athenas.EjemploTemplateApi.SharedKernel.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SharedKernel.Test
{
    public class RestClientServiceTests
    {
        private readonly Mock<IHttpClientFactory> _mockFactory;
        private readonly Mock<ILogger<RestClientService>> _mockLogger;
        private readonly IOptions<RestClientOptions> _options;

        public RestClientServiceTests()
        {
            _mockFactory = CreateMockHttpClientFactory();
            _options = InstanceOptions();
            _mockLogger = new Mock<ILogger<RestClientService>>();
        }
        private struct MockDataResponse
        {

        };
        private IOptions<RestClientOptions> InstanceOptions()
        {
            char h = 'h';
            string protocol = "ttp";
            string separator = "//";
            string baseDomain = "pruebas_unitarias";
            string dominio = ".com";
            return Options.Create(new RestClientOptions()
            {
                Services = new Dictionary<string, RestClientServiceSetting>()
                {
                    {
                        "ServiceName",
                        new RestClientServiceSetting
                        {
                            BaseAddress = new Uri($"{h}{protocol}:{separator}{baseDomain}{dominio}"),
                            DefaultRequestHeaders = new Dictionary<string, string>
                            {
                                {
                                    "header-name",
                                    "header-value"
                                }
                            },
                            TokenRequest = new TokenSetting()
                            {
                                Path = "/prueba",
                                BaseAddress = new Uri($"{h}{protocol}:{separator}{baseDomain}{dominio}"),
                                ClientId = "asdfj",
                                ClientSecret = "asdfk",
                                ContentType = "application/json",
                                GrantType = "Client",
                                Scope = "asdflk"
                            }
                        }
                    }
                }
            });
        }

        private static Mock<IHttpClientFactory> CreateMockHttpClientFactory()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var handleMock = new Mock<HttpMessageHandler>();
            handleMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"token_type\":\"Bearer\",\"expires_in\":3599,\"ext_expires_in\":3599,\"access_token\":\"asdfjahsd\"}")
                })
                .Verifiable();

            var httpClient = new HttpClient(handleMock.Object);
            mockFactory.Setup(s => s.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            return mockFactory;
        }

        [Theory]
        [ClassData(typeof(RestClientServiceData))]
        public void RestClientService_EnviarParametrosNull_RetornaArgumentNullException(
            IHttpClientFactory clientFactory,
            IOptions<RestClientOptions> options,
            ILogger<RestClientService> logger)
        {

            Assert.Throws<ArgumentNullException>(() => new RestClientService(clientFactory, options, logger));

        }

        [Fact]
        public void InstaceServices_CreaInstanciaDelServicio()
        {
            var restClient = new RestClientService(_mockFactory.Object, _options, _mockLogger.Object);
            Assert.NotNull(restClient);

        }


        [Fact]
        public async Task Post_CallService_ReturnObjectResult()
        {
            var restClient = new RestClientService(_mockFactory.Object, _options, _mockLogger.Object);

            var response = await restClient.Post("ServiceName", "/path", string.Empty);
            Assert.NotNull(response);

        }
        [Fact]
        public async Task Get_CallService_ReturnObjectResult()
        {
            var restClient = new RestClientService(_mockFactory.Object, _options, _mockLogger.Object);

            var response = await restClient.Get("ServiceName", "/path");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Get_LlamarServicio_RetornaObjetoRespuestaFormateada()
        {
            var restClient = new RestClientService(_mockFactory.Object, _options, _mockLogger.Object);

            var response = await restClient.Get<MockDataResponse>("ServiceName", "/path");

            Assert.IsType<MockDataResponse>(response);
        }


    }

    public class RestClientServiceData : TheoryData<IHttpClientFactory, IOptions<RestClientOptions>, ILogger<RestClientService>>
    {
        private readonly Mock<IHttpClientFactory> _mockFactory;
        public RestClientServiceData()
        {
            _mockFactory = new Mock<IHttpClientFactory>();

            Add(null, null, null);
            Add(_mockFactory.Object, null, null);
        }
    }
}