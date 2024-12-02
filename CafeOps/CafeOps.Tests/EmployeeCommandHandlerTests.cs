using CafeOps.DAL;
using CafeOps.DAL.Entities;
using CafeOps.DAL.Repositories.Interfaces;
using CafeOps.Logic;
using FluentAssertions;
using Moq;
using Xunit;

namespace CafeOps.Tests
{
    public class EmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly Mock<ICafeRepository> _mockCafeRepository;
        private readonly CreateEmployeeCommandHandler _handler;

        public EmployeeCommandHandlerTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            _handler = new CreateEmployeeCommandHandler(_mockEmployeeRepository.Object, _mockCafeRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddEmployeeAndReturnId()
        {
            // Arrange
            var mockContext = new Mock<ApplicationDbContext>();

            var command = new CreateEmployeeCommand(
                  Id: "UI1234567",
                  Name: "John Doe",
                  EmailAddress: "john.doe@example.com",
                  PhoneNumber: "91234567",
                  Gender: "Male",
                  CafeId: Guid.NewGuid(),
                  StartDate: DateTime.UtcNow
             );

            mockContext.Setup(c => c.Employees.Add(It.IsAny<Employee>()));
            mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNullOrEmpty();
            mockContext.Verify(c => c.Employees.Add(It.IsAny<Employee>()), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
