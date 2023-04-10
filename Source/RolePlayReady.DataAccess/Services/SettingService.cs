﻿using RolePlayReady.Repositories;

namespace RolePlayReady.DataAccess.Services;

public class SettingService {
    private readonly IGameSettingsRepository _gameSettings;
    private readonly string _owner;

    public SettingService(IGameSettingsRepository gameSettings, IUserAccessor user) {
        _gameSettings = gameSettings;
        _owner = user.Id;
    }

    public async Task<GameSetting> LoadAsync(string id, CancellationToken cancellation = default)
        => await _gameSettings.GetByIdAsync(_owner, id, cancellation)
           ?? throw new InvalidOperationException($"Game setting '{id}' was not found.");
}