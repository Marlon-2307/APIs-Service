using AutoMapper;
using Moq;
using Athenas.EjemploTemplateApi.Application.DataTransferObjects;
using Athenas.EjemploTemplateApi.Application.UseCases;
using Athenas.EjemploTemplateApi.Domain.Entities;
using Athenas.EjemploTemplateApi.Domain.Interfaces;
using Athenas.EjemploTemplateApi.Domain.Request;
using Athenas.EjemploTemplateApi.Infrastructure.Repositories.Contracts;
using Athenas.EjemploTemplateApi.Infrastructure.Services.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Athenas.EjemploTemplateApi.WebApi.Test.Application.UseCases;

public class CompanyUseCasesTest
{
    [Fact]
    public async Task GetInformationClient_ShouldReturnCorrectInfo()
    {
        // Arrange
        var mockLogger = new Mock<IAppLogger<CompanyUseCase>>();
        var mockRabbitMqService = new Mock<IRabbitMqService>();
        var mockClienteinformacionServices = new Mock<IClienteinformacionServices>();
        var mockMapper = new Mock<IMapper>();
        var mockCompanyRepository = new Mock<ICompanyRepository>();
        var mockConfiguration = new Mock<IServiceConfiguration>();

        var getClientDto = new GetClientDto
        {
            // Initialize properties as needed
        };
        var clienteRequest = new ClienteRequest
        {
            // Initialize properties as needed
        };
        var infoClientResponse = new InfoClientResponse
        {
            // Initialize properties as needed
        };
        mockMapper.Setup(m => m.Map<ClienteRequest>(It.IsAny<GetClientDto>()))
                  .Returns(clienteRequest);
        mockClienteinformacionServices.Setup(s => s.GetInfoclientes(It.IsAny<ClienteRequest>()))
                                     .ReturnsAsync(infoClientResponse);

        var companyUseCase = new CompanyUseCase(
            mockLogger.Object,
            mockRabbitMqService.Object,
            mockClienteinformacionServices.Object,
            mockMapper.Object,
            mockCompanyRepository.Object,
            mockConfiguration.Object
        );

        // Act
        var result = await companyUseCase.GetInformationClient(getClientDto);

        // Assert
        Assert.Equal(infoClientResponse, result);
        mockMapper.Verify(m => m.Map<ClienteRequest>(It.IsAny<GetClientDto>()), Times.Once());
        mockClienteinformacionServices.Verify(s => s.GetInfoclientes(It.IsAny<ClienteRequest>()), Times.Once());
    }

    [Fact]
    public async Task GetAllCompanies_ShouldReturn_AllCompanies()
    {
        // Arrange
        var mockLogger = new Mock<IAppLogger<CompanyUseCase>>();
        var mockRabbitMqService = new Mock<IRabbitMqService>();
        var mockClienteinformacionServices = new Mock<IClienteinformacionServices>();
        var mockMapper = new Mock<IMapper>();
        var mockCompanyRepository = new Mock<ICompanyRepository>();
        var mockConfiguration = new Mock<IServiceConfiguration>();

        var companies = new List<Company>
    {
        new Company { /* ... */ },
        // ...
    };
        var companyDtos = new List<CompanyDto>
    {
        new CompanyDto { /* ... */ },
        // ...
    };
        mockCompanyRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(companies);
        mockMapper.Setup(m => m.Map<IEnumerable<CompanyDto>>(companies)).Returns(companyDtos);

        var companyUseCase = new CompanyUseCase(
        mockLogger.Object, mockRabbitMqService.Object, mockClienteinformacionServices.Object, mockMapper.Object, mockCompanyRepository.Object, mockConfiguration.Object
        );

        // Act
        var result = await companyUseCase.GetAllCompanies();

        // Assert
        Assert.Equal(companyDtos, result);
    }
}
