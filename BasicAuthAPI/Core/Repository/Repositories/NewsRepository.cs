using BasicAuthAPI.Core.Repository.Interfaces;

namespace BasicAuthAPI.Core.Repository.Repositories;

public class NewsRepository : INewsRepository
{
    
    
    
    public async Task RebuildDatabase()
    { 
        using var activity = _tracer.StartActiveSpan("Rebuild DB");
        
        Logging.Log.Information("Called RebuildDatabase function");

        await _context.Database.EnsureDeletedAsync(); 
        await _context.Database.EnsureCreatedAsync();
    }
}