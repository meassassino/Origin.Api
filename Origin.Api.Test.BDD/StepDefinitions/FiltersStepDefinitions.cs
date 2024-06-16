using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using NUnit.Framework;
using Origin.Api.Handlers;
using Origin.Api.Logging;
using Origin.Api.Services.Interfaces;

namespace Origin.Api.Test.BDD.StepDefinitions
{
    [Binding]
    public class FilterHandlerSteps
    {
        private IResult _result;
        private readonly Mock<IFilterService> _filterService = new();
        private readonly Mock<ILoggingService<FilterHandler>> _logger = new();
        private string _filterName;

        [Given(@"a valid filter file name ""(.*)""")]
        public void GivenAValidFilterFileName(string fileName)
        {
            _filterService.Setup(x => x.GetFilterListFromFiles(It.IsAny<string>()))
                .Returns("file content");
        }

        [When(@"the GetFilterListFromFiles method is called with the file name ""(.*)""")]
        public void WhenCalledWithExistingFileName(string fileName)
        {
            _filterName = fileName;
            _result = FilterHandler.GetFilters(_logger.Object, _filterService.Object, _filterName);
        }

        [Then(@"the method should return the content of the file ""(.*)""")]
        public void ThenTheMethodShouldReturnTheContentOfTheFile(string expectedContent)
        {
            Assert.IsNotNull(_result);
            Assert.IsAssignableFrom<ContentHttpResult>(_result);
        }

        [Given(@"a non-existent filter file name ""(.*)""")]
        public void GivenANonExistentFilterFileName(string fileName)
        {
            _filterName = fileName;

            _filterService.Setup(x => x.GetFilterListFromFiles(It.IsAny<string>()))
                .Returns((string)null);
        }

        [Then(@"the method should log an error ""(.*)""")]
        public void ThenTheMethodShouldLogAnError(string errorMessage)
        {
            _logger.Verify(x => x.LogError(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        }

        [Then(@"the method should return 404 not found http response")]
        public void ThenTheMethodShouldReturn404()
        {
            Assert.IsNotNull(_result);
            Assert.IsAssignableFrom<NotFound<string>>(_result);
        }
    }
}