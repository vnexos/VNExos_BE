using VNExos.Domain.Entities;
using VNExos.Domain.Presistence;
using VNExos.Domain.Repositories;
using VNExos.Infrastructure.Repositories;

namespace VNExos.Test.Repository;

public class LanguageRepositoryTests
{
    private async Task<VNExosContext> GetContext()
    {
        var options = new DbContextOptionsBuilder<VNExosContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new VNExosContext(options);
        databaseContext.Database.EnsureCreated();
        if(await databaseContext.Languages.CountAsync() <= 0 )
        {
            var codeArr = new[]
            {
                "vi-vn", "en-us", "en-uk", "zh-cn", "ja-jp", "ar-sa", "he-il", "tr-tr", "th-th", "ko-kr"
            };
            var nameArr = new[]
            {
                "Tiếng Việt",
                "English (United States)",
                "English (United Kingdom)",
                "简体中文",
                "日本語",
                "اَلْعَرَبِيَّةُ",
                "עברית",
                "Türkçe",
                "ภาษาไทย",
                "한국어"
            };
            var rtl = new[]
            {
                false, false, false, false, false, true, true, false, false, false
            };
            for (int i = 0; i < 5; i++)
            {
                databaseContext.Languages.Add(new Language
                {
                    Code = codeArr[i],
                    Name = nameArr[i],
                    FlagUrl = "https://vnexos.com/flags/ex-ex.png",
                    RightToLeft = rtl[i],
                });
            }
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }

    [Fact]
    public async void GetByCode_Should_ReturnLanguage()
    {
        // Arrange
        var code = "vi-vn";
        var dbContext = await GetContext();
        ALanguageRepository repository = new LanguageRepository(dbContext);

        // Act
        var result = await repository.GetByCode(code);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Language>();
        result?.Code.Should().Be(code);
    }
}
