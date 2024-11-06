using Athenas.EjemploTemplateApi.Application.Contracts;
using Athenas.EjemploTemplateApi.Application.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Athenas.EjemploTemplateApi.WebApi.Controllers;
using Xunit;
using Athenas.EjemploTemplateApi.Domain.Exceptions;
using Athenas.EjemploTemplateApi.Domain.Interfaces;

namespace WebApi.Tests.Controllers
{
    public class CompaniesControllerTest
    {
        private readonly Mock<ICompanyUseCase> _mockCompanyUseCase;
        public CompaniesControllerTest()
        {
            _mockCompanyUseCase = new Mock<ICompanyUseCase>();
        }

        [Fact]
        public async Task CreateCompany_ShouldReturn_CreatedAtRouteResult_WhenCompanyIsValid()
        {
            // Arrange
            var companyForCreationDto = new CompanyForCreationDto()
            {
                Name = "Colombina",
                Address = "Carrera 57 sur # 01 - 12",
                Country = "Colombia"

            };
            var createdCompany = new CompanyDto
            {
                Name = "Colombina",
                FullAddress = "Carrera 57 sur # 01 - 12"
            };

            _mockCompanyUseCase.Setup(x => x.CreateCompanyAsync(It.IsAny<CompanyForCreationDto>()))
                              .ReturnsAsync(createdCompany);

            var controller = new CompaniesController(_mockCompanyUseCase.Object);

            // Act
            var result = await controller.CreateCompany(companyForCreationDto);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);

            Assert.NotNull(result);
        }

       
    }
}
