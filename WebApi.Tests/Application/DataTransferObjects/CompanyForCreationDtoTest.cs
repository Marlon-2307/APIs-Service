using Xunit;
using Athenas.EjemploTemplateApi.Application.DataTransferObjects;

namespace Application.Tests
{
    public class CompanyForCreationDtoTest
    {
         

        [Fact]
        public void Validator_CompanyForCreationDto_ReturnBadModel()
        {
            //Arrange
            CompanyForCreationDto company = new();
            CompanyForCreationDtoValidator customerValidator = new();

            //Act
            var validatorResult = customerValidator.Validate(company);
           
            //Assert            
            Assert.False(validatorResult.IsValid);
        }
        [Fact]
        public void Validator_CompanyForCreationDto_ReturnGoodModel()
        {
            //Arrange
            CompanyForCreationDto company = new() {Address="Carrera 12 # 34 -23, Medellín",Country="Colombia",Name="Compañia de Financiamiento Athenas" };
            CompanyForCreationDtoValidator customerValidator = new();

            //Act
            var validatorResult = customerValidator.Validate(company);

            //Assert            
            Assert.True(validatorResult.IsValid);
        }

    }


}