using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Origin.Api.Handlers;
using Origin.Api.Logging;
using Origin.Api.Services.Interfaces;

namespace Origin.Api.Test.Unit.HandlerTests;

[TestFixture]
public class FilterHandlerTests
{
    private readonly Mock<IFilterService> _filterServiceMock = new();

    private const string FilterName = "filterName";

    [Test]
    public void GetFilters_ReturnsContentResult_WhenFilterServiceReturnsValidResponse()
    {
        // Arrange
        var loggerMock = new Mock<ILoggingService<FilterHandler>>();
        var validResponse = "valid response";
        _filterServiceMock.Setup(fs => fs.GetFilterListFromFiles(It.IsAny<string>())).Returns(validResponse);

        // Act
        var result = FilterHandler.GetFilters(loggerMock.Object, _filterServiceMock.Object, FilterName);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ContentHttpResult>(result);
        Assert.AreEqual(validResponse, ((ContentHttpResult)result).ResponseContent);
    }

    [Test]
    public void GetFilters_LogsErrorAndReturnsNotFound_WhenFilterServiceReturnsNull()
    {
        // Arrange
        var loggerMock = new Mock<ILoggingService<FilterHandler>>();

        _filterServiceMock.Setup(x => x.GetFilterListFromFiles(FilterName)).Returns((null as string)!);

        // Act
        var result = FilterHandler.GetFilters(loggerMock.Object, _filterServiceMock.Object, FilterName);

        // Assert
        loggerMock.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<string>(),It.IsAny<object>()), Times.Once);
        Assert.IsInstanceOf<NotFound<string>>(result);
    }

    [Test]
    public void GetFilters_CallsExpectedDependencies()
    {
        // Arrange
        var loggerMock = new Mock<ILoggingService<FilterHandler>>();
        var filterServiceMock = new Mock<IFilterService>();

        // Act
        FilterHandler.GetFilters(loggerMock.Object, filterServiceMock.Object, FilterName);

        // Assert
        filterServiceMock.Verify(x => x.GetFilterListFromFiles(FilterName), Times.Once);
    }

    [Test]
    public void GetFilters_HandlesNameParameterCaseSensitively()
    {
        // Arrange
        var loggerMock = new Mock<ILoggingService<FilterHandler>>();
        var filterServiceMock = new Mock<IFilterService>();

        // Act
        FilterHandler.GetFilters(loggerMock.Object, filterServiceMock.Object, FilterName);

        // Assert
        filterServiceMock.Verify(x => x.GetFilterListFromFiles(It.Is<string>(s => s == FilterName)), Times.Once);
    }

    [TestCase("")]
    //[TestCase(null)]
    [TestCase("!@#$%^&*()\"")]
    public void GetFilters_HandlesInvalidFilterNames(string name)
    {
        // Arrange
        var loggerMock = new Mock<ILoggingService<FilterHandler>>();
        var filterServiceMock = new Mock<IFilterService>();

        // Act
        var result = FilterHandler.GetFilters(loggerMock.Object, filterServiceMock.Object, name);

        // Assert
        loggerMock.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        filterServiceMock.Verify(x => x.GetFilterListFromFiles(name), Times.Never);
        Assert.IsInstanceOf<BadRequest<string>>(result);
    }
}
