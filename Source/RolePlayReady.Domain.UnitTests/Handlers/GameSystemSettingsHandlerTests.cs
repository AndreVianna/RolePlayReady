using static RolePlayReady.Constants.Constants;

namespace RolePlayReady.Handlers;

public class GameSystemSettingsHandlerTests {
    private readonly GameSystemSettingsHandler _handler;
    private readonly IGameSystemSettingsRepository _repository;

    public GameSystemSettingsHandlerTests() {
        _repository = Substitute.For<IGameSystemSettingsRepository>();
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.Id.Returns(InternalUser);
        _handler = new GameSystemSettingsHandler(_repository, userAccessor);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsSettings() {
        // Arrange
        var expectedSettings = new[] { CreateInput() };
        _repository.GetManyAsync(InternalUser, Arg.Any<CancellationToken>()).Returns(expectedSettings);

        // Act
        var result = await _handler.GetManyAsync();

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsSetting() {
        // Arrange
        var id = Guid.NewGuid();
        var expectedSetting = CreateInput(id);

        _repository.GetByIdAsync(InternalUser, id, Arg.Any<CancellationToken>()).Returns(expectedSetting);

        // Act
        var result = await _handler.GetByIdAsync(id);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task AddAsync_ReturnsSetting() {
        // Arrange
        var input = CreateInput();
        _repository.InsertAsync(InternalUser, input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.AddAsync(input);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsSetting() {
        // Arrange
        var input = CreateInput();
        _repository.UpdateAsync(InternalUser, input, Arg.Any<CancellationToken>()).Returns(input);

        // Act
        var result = await _handler.UpdateAsync(input);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Remove_ReturnsTrue() {
        // Arrange
        var id = Guid.NewGuid();
        _repository.Delete(InternalUser, id).Returns<Result<bool>>(true);

        // Act
        var result = _handler.Remove(id);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    private static GameSystemSetting CreateInput(Guid? id = null)
        => new() {
            Id = id ?? Guid.NewGuid(),
            ShortName = "SM",
            Name = "Some Name",
            Description = "Some description.",
            AttributeDefinitions = Array.Empty<AttributeDefinition>(),
        };
}