using VNExos.Domain.Entities;
using VNExos.Domain.Presistence;
using VNExos.Domain.Repositories;
using VNExos.Infrastructure.Repositories;

namespace VNExos.Test.Repository;

public class TranslationRepositoryTests
{
    private static async Task<VNExosContext> GetContext()
    {
        var databaseContext = await LanguageRepositoryTests.GetContext();
        databaseContext.Database.EnsureCreated();
        if (!await databaseContext.Translations.AnyAsync())
        {
            var languages = await databaseContext.Languages
                .Where(language => language.Code == "vi-vn" || language.Code == "en-us")
                .ToListAsync();
            var origTrans = new Dictionary<string, string>()
            {
                { "KEY_1", "VAL_1" },
                { "KEY_2", "VAL_2" },
                { "KEY_3", "VAL_3" },
                { "KEY_4", "VAL_4" },
                { "KEY_5", "VAL_5" },
            };
            for (var i = 0; i < 2; i++)
            {
                var LangId = languages[i].Id;
                foreach (var orig in origTrans)
                {
                    databaseContext.Translations.Add(new Translation
                    {
                        LanguageId = LangId,
                        Origin = orig.Key,
                        Translate = orig.Value
                    });

                }
            }
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }

    [Fact]
    public async void CreateTranslations_Should_SaveARecordToData_And_ReturnTranslation()
    {
        // Arrange
        var context = await GetContext();
        ATranslationRepository translationRepository = new TranslationRepository(context);
        var sampleData = new List<Translation>();
        sampleData.Add(new Translation { LanguageId = Guid.NewGuid(), Origin = "ABC", Translate = "Test translate" });
        sampleData.Add(new Translation { LanguageId = Guid.NewGuid(), Origin = "TEST", Translate = "Hello world!" });

        // Act
        var result = await translationRepository.CreateTranslations(sampleData);

        // Assert
        result.Should().BeOfType<List<Translation>>();
        result.Count.Should().Be(2);
        foreach (var translation in result)
        {
            translation.Should().NotBeNull();
            translation.Id.Should().NotBeEmpty();
        }
        
    }

    [Fact]
    public async void UpdateTranslationsByCodeAndOrigin_Should_UpdateInfoInsideRecord_And_ReturnTranslation()
    {
        // Arrange
        var context = await GetContext();
        ATranslationRepository translationRepository = new TranslationRepository(context);
        var code = "vi-vn";
        var origin = "KEY_1";
        var translation = new Translation
        {
            Origin = "VALUE",
            Translate = "Giá trị"
        };

        // Act
        var result = await translationRepository.UpdateTranslationsByCodeAndOrigin(code, origin, translation);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Translation>();
        var testAssert = await translationRepository.GetById(result!.Id);
        testAssert.Should().NotBeNull();
        testAssert!.Origin.Should().Be(translation.Origin);
        testAssert!.Translate.Should().Be(translation.Translate);
    }
}
