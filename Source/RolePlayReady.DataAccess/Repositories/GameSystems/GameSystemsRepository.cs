﻿using System.Extensions;

namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemsRepository : IGameSystemsRepository {
    private readonly ITrackedJsonFileRepository<GameSystemData> _files;

    public GameSystemsRepository(ITrackedJsonFileRepository<GameSystemData> files) {
        _files = files;
    }

    public async Task<IEnumerable<Row>> GetManyAsync(string owner, CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(owner, string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.ToArray(i => i.MapToRow());
    }

    public async Task<GameSystem?> GetByIdAsync(string owner, Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(owner, string.Empty, id, cancellation)
            .ConfigureAwait(false);
        return file.Map();
    }

    public async Task<GameSystem> InsertAsync(string owner, GameSystem input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, input.Map(), cancellation).ConfigureAwait(false);
        return result.Map()!;
    }

    public async Task<GameSystem> UpdateAsync(string owner, GameSystem input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, input.Map(), cancellation);
        return result.Map()!;
    }

    public Result<bool> Delete(string owner, Guid id)
        => _files.Delete(owner, string.Empty, id);
}
