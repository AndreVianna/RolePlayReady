﻿namespace RolePlayReady.Models;

public record InventoryEntry : IInventoryEntry {
    public required IGameObject Item { get; init; }
    public required decimal Quantity { get; init; }
}
