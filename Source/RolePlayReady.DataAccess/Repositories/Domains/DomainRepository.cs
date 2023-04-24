﻿using System.Extensions;

namespace RolePlayReady.DataAccess.Repositories.Domains;

public class DomainRepository : IDomainRepository {
    private readonly ITrackedJsonFileRepository<DomainData> _files;

    public DomainRepository(ITrackedJsonFileRepository<DomainData> files) {
        _files = files;
    }

    public async Task<IEnumerable<Row>> GetManyAsync(string owner, CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(owner, string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.ToArray(i => i.MapToRow());
    }

    public async Task<Domain?> GetByIdAsync(string owner, Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(owner, string.Empty, id, cancellation)
            .ConfigureAwait(false);
        return file.Map();
    }

    public async Task<Domain> InsertAsync(string owner, Domain input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, input.Map(), cancellation).ConfigureAwait(false);
        return result.Map()!;
    }

    public async Task<Domain> UpdateAsync(string owner, Domain input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, input.Map(), cancellation);
        return result.Map()!;
    }

    public Result<bool> Delete(string owner, Guid id)
        => _files.Delete(owner, string.Empty, id);
}