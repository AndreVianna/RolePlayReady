﻿namespace RolePlayReady.Models;

public record JournalEntry : IJournalEntry {
    public required string Section { get; init; }
    public required string Title { get; init; }
    public required string Text { get; init; }
}