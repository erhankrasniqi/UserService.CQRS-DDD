using Moq;
using SharedKernel;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using UserService.Application.Features.Users.Commands;
using UserService.Application.Responses;
using UserService.Domain.Aggregates.UsersAggregates;
using UserService.Infrastructure.Contracts;
using Xunit;

namespace UserService.Tests
{
    public class CreateUserTest
    {
        [Fact]
        public async Task CreateUser_Should_Return_Success_When_Valid()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockNotifier = new Mock<INotifier>(); // Shto mockun

            mockUserRepository.Setup(r => r.UsernameTakenByAnotherAccountAsync(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(false);

            mockUserRepository.Setup(r => r.GetByAccountIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync((Users)null);

            var handler = new CreateUserCommandHandler(
                mockUserRepository.Object,
                mockUnitOfWork.Object,
                mockNotifier.Object // Shto këtu
            );

            var command = new CreateUserCommand
            {
                AccountId = Guid.NewGuid(),
                Username = "TestUser123",
                TenantId = "tenant-001"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("User created successfully.", result.Message);
            mockUserRepository.Verify(r => r.Add(It.IsAny<Users>(), It.IsAny<CancellationToken>()), Times.Once);
            mockUnitOfWork.Verify(u => u.Save(It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
