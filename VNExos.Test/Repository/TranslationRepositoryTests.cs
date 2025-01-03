using VNExos.Domain.Presistence;

namespace VNExos.Test.Repository;

public class TranslationRepositoryTests
{
    private static async Task<VNExosContext> GetContext()
    {
        var options = new DbContextOptionsBuilder<VNExosContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new VNExosContext(options);
        databaseContext.Database.EnsureCreated();
        if (!await databaseContext.Translations.AnyAsync())
        {
        }
        return databaseContext;
    }
}
