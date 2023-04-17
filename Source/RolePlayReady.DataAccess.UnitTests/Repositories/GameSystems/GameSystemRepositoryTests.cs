using static RolePlayReady.Constants.Constants;

namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemsRepositoryTests {
    private readonly ITrackedJsonFileRepository _files;
    private readonly GameSystemsRepository _repository;

    public GameSystemsRepositoryTests() {
        _files = Substitute.For<ITrackedJsonFileRepository>();
        _repository = new(_files);
    }

    [Fact]
    public async Task GetManyAsync_ReturnsAllSettings() {
        // Arrange
        var dataFiles = GenerateDataFiles();
        _files.GetAllAsync<GameSystemDataModel>(InternalUser, string.Empty).Returns(dataFiles);

        // Act
        var settings = await _repository.GetManyAsync(InternalUser);

        // Assert
        settings.HasValue.Should().BeTrue();
        settings.Value.Count().Should().Be(dataFiles.Length);
    }

    [Fact]
    public async Task GetByIdAsync_SettingFound_ReturnsSetting() {
        // Arrange
        var dataFile = GenerateDataFile();
        var tokenSource = new CancellationTokenSource();
        _files.GetByIdAsync<GameSystemDataModel>(InternalUser, string.Empty, dataFile.Name, tokenSource.Token).Returns(dataFile);

        // Act
        var setting = await _repository.GetByIdAsync(InternalUser, Guid.Parse(dataFile.Name), tokenSource.Token);

        // Assert
        setting.HasValue.Should().BeTrue();
        setting.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_SettingNotFound_ReturnsNull() {
        // Arrange
        var id = Guid.NewGuid();
        _files.GetByIdAsync<GameSystemDataModel>(InternalUser, string.Empty, id.ToString(), Arg.Any<CancellationToken>()).Returns((DataFile<GameSystemDataModel>?)null);

        // Act
        var setting = await _repository.GetByIdAsync(InternalUser, id);

        // Assert
        setting.HasValue.Should().BeFalse();
        setting.IsNull.Should().BeTrue();
    }

    [Fact]
    public async Task InsertAsync_InsertsNewSetting() {
        // Arrange
        var setting = GenerateSetting();
        _files.UpsertAsync(InternalUser, string.Empty, Arg.Any<string>(), Arg.Any<GameSystemDataModel>()).Returns(DateTime.Now);

        // Act
        var result = await _repository.InsertAsync(InternalUser, setting);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_UpdatesExistingSetting() {
        // Arrange
        var setting = GenerateSetting();
        _files.UpsertAsync(InternalUser, string.Empty, setting.Id.ToString(), Arg.Any<GameSystemDataModel>()).Returns(DateTime.Now);

        // Act
        var result = await _repository.UpdateAsync(InternalUser, setting);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    [Fact]
    public void Delete_RemovesSetting() {
        // Arrange
        var id = Guid.NewGuid();
        _files.Delete(InternalUser, string.Empty, id.ToString()).Returns<Result<bool>>(true);

        // Act
        var result = _repository.Delete(InternalUser, id);

        // Assert
        result.HasValue.Should().BeTrue();
    }

    private static DataFile<GameSystemDataModel>[] GenerateDataFiles()
        => new[] { GenerateDataFile() };

    private static DataFile<GameSystemDataModel> GenerateDataFile()
        => new() {
            Name = Guid.NewGuid().ToString(),
            Timestamp = DateTime.Now,
            Content = new() {
                ShortName = "SomeId",
                Name = "Some Name",
                Description = "Some Description",
                Tags = new[] { "SomeTag" },
            }
        };

    private static GameSystem GenerateSetting()
        => new() {
            Id = Guid.NewGuid(),
            ShortName = "SomeId",
            Timestamp = DateTime.Now,
            Name = "Some Name",
            Description = "Some Description",
            Tags = new[] { "SomeTag" },
        };

}