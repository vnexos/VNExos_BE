using NetArchTest.Rules;

namespace VNExos.Test;

public class ArchitectureTests
{
    private const string APINamespace = "VNExos.API";
    private const string ApplicationNamespace = "VNExos.Application";
    private const string CommonNamespace = "VNExos.Common";
    private const string DomainNamespace = "VNExos.Domain";
    private const string InfrastructureNamespace = "VNExos.Infrastructure";

    [Fact]
    public void Common_ShouldNot_HaveDependencyOnOtherProject()
    {
        // Arrange
        var assembly = typeof(VNExos.Common.AssemblyReference).Assembly;

        var otherProject = new[]
        {
            APINamespace, ApplicationNamespace, DomainNamespace, InfrastructureNamespace
        };


        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful, "Common should not be dependenced on other projects.");
    }

    [Fact]
    public void Domain_ShouldNot_HaveDependencyOnOtherProject_Except_CommonProject()
    {
        // Arrange
        var assembly = typeof(VNExos.Domain.AssemblyReference).Assembly;

        var otherProject = new[]
        {
            APINamespace, ApplicationNamespace, InfrastructureNamespace
        };


        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProject)
            .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful, "Domain should not be dependenced on other projects except Common.");
    }

    [Fact]
    public void Application_ShouldNot_HaveDependencyOnInfrastructureProjectAndApiProject()
    {
        // Arrange
        var assembly = typeof(VNExos.Application.AssemblyReference).Assembly;

        var targetProjects = new[]
        {
            APINamespace, InfrastructureNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(targetProjects)
            .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful, "Application should not have dependency on Infrastructure and API.");
    }

    [Fact]
    public void Infrastructure_Should_HaveDependencyOnDomainOnly()
    {
        // Arrange
        var assembly = typeof(VNExos.Infrastructure.AssemblyReference).Assembly;

        var targetProjects = new[]
        {
            APINamespace, ApplicationNamespace, CommonNamespace
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(targetProjects)
            .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful, "Infrastructure should have dependency on Domain only.");
    }

    [Fact]
    public void InfrastructureClasses_Should_BeInternal()
    {
        // Arrange
        var assembly = typeof(VNExos.Infrastructure.AssemblyReference).Assembly;

        var targetClassname = new[]
        {
            "AssemblyReference", "InfrastructureExtension"
        };

        // Act
        var testResult = Types
            .InAssembly(assembly)
            .That()
            .ArePublic()
            .Should()
            .HaveName("AssemblyReference")
            .Or()
            .HaveName("InfrastructureExtension")
            .GetResult();

        // Assert
        Assert.True(testResult.IsSuccessful, "The classes in the Infrastructure should be internal.");
    }
}