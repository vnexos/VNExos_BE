using VNExos.Domain.Common;
using VNExos.Domain.Entities;
using VNExos.Domain.Presistence;
using VNExos.Infrastructure.Repositories;

namespace VNExos.Test;

public class CommonRepositoryTests
{
    private async Task<VNExosContext> GetContext()
    {
        var options = new DbContextOptionsBuilder<VNExosContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new VNExosContext(options);
        databaseContext.Database.EnsureCreated();
        if (await databaseContext.Languages.CountAsync() <= 0)
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
            databaseContext.Languages.Add(new Language
            {
                Id = Guid.Parse("3ac27301-7d21-462f-8cc9-3b72e637c10b"),
                Code = "vi-us",
                Name = "Tchiếng Dziệt (Mỹỷ)",
                FlagUrl = "https://vnexos.com/flags/ex-ex.png",
                RightToLeft = true,
            });
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }

    [Fact]
    public async void Create_Should_ReturnEntity_And_SaveARecordToData()
    {
        // Arrange
        var dbContext = await GetContext();
        ACommonRepository<Language> repository = new LanguageRepository(dbContext);
        var sample = new Language
        {
            Code = "vi-US",
            Name = "Tchiếng Dziệt (Mỹ)",
            FlagUrl = "https://vnexos.com/flags/en-in.png",
            RightToLeft = true,
        };

        // Act
        var result = await repository.Create(sample);

        // Assert
        result.Should().BeOfType<Language>();
        result.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async void GetById_Should_ReturnEntity()
    {
        // Arrange
        var id = Guid.Parse("3ac27301-7d21-462f-8cc9-3b72e637c10b");
        var dbContext = await GetContext();
        ACommonRepository<Language> repository = new LanguageRepository(dbContext);

        // Act
        var result = await repository.GetById(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Language>();
        result?.Code.Should().Be("vi-us");
    }

    [Fact]
    public async void GetAll_Should_ReturnListOfEntities()
    {
        // Arrange
        var id = Guid.Parse("3ac27301-7d21-462f-8cc9-3b72e637c10b");
        var dbContext = await GetContext();
        ACommonRepository<Language> repository = new LanguageRepository(dbContext);

        // Act
        var result = await repository.GetAll();

        // Assert
        result.Should().NotBeEmpty();
        result.Should().BeOfType<List<Language>>();
        result.ElementAt(0).Code.Should().Be("vi-vn");
    }

    [Theory]
    [InlineData("vi-vn", null, null, null, null)]
    [InlineData(null, "Tiếng Việt (3 que)", null, null, null)]
    [InlineData(null, null, "https://vnexos.com/flags/ex-ex.png", null, null)]
    [InlineData(null, null, null, false, null)]
    [InlineData(null, null, null, null, false)]
    [InlineData("vi-vn", "Tiếng Việt (3 que)", "https://vnexos.com/flags/ex-ex.png", false, false)]
    public async void Update_Should_ChangeTheValueInsideRecord_And_ReturnEntity(string? code, string? name, string? flagUrl, bool? rtl, bool? isDefault)
    {
        // Arrange
        var id = Guid.Parse("3ac27301-7d21-462f-8cc9-3b72e637c10b");
        var dbContext = await GetContext();
        ACommonRepository<Language> repository = new LanguageRepository(dbContext);
        var entity = new Language
        {
            Id = id,
            Code = code,
            Name = name,
            FlagUrl = flagUrl,
            RightToLeft = rtl,
            IsDefault = isDefault
        };

        // Act
        var result = await repository.Update(entity);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Language>();

        if (code != null)
            result?.Code.Should().Be(code);
        if (name != null)
            result?.Name.Should().Be(name);
        if (flagUrl != null)
            result?.FlagUrl.Should().Be(flagUrl);
        if (rtl != null)
            result?.RightToLeft.Should().Be(rtl);
        if (isDefault != null)
            result?.IsDefault.Should().Be(isDefault);
    }

    /*
     * !!! XÓA DỮ LIỆU LỖI KHỎI CUỘC ĐỜI !!!
     */
    [Fact]
    public async void Delete_Should_RemoveRecordFromDatabase_And_ReturnEntity()
    {
        // Arrange
        var id = Guid.Parse("3ac27301-7d21-462f-8cc9-3b72e637c10b");
        var dbContext = await GetContext();
        ACommonRepository<Language> repository = new LanguageRepository(dbContext);

        // Act
        var result = await repository.Delete(id);
        var testDelete = await repository.GetById(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Language>();
        testDelete.Should().BeNull();
    }
}
