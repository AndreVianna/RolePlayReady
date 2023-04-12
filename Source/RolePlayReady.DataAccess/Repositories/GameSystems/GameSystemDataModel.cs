﻿namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemDataModel {
    public string? ShortName { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string[] Tags { get; set; }
}