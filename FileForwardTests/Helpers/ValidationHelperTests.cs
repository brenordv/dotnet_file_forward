using System;
using System.Reflection;
using FileForward.Exceptions;
using FileForward.Helpers;
using Xunit;

namespace FileForwardTests.Helpers
{
    public class ValidationHelperTests
    {
        #region CheckEndpointUrl Tests
        
        [Theory]
        [InlineData("/")]
        [InlineData("/foo/bar")]
        [InlineData("/foo/bar?q=success=1")]
        [InlineData("/really any text that starts with a forward slash")]
        public void CheckEndpointUrl_Success(string endpointUrl)
        {
            ValidationHelper.CheckEndpointUrl(endpointUrl);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("      ")]
        public void CheckEndpointUrl_NullEndpoint_Error(string endpointUrl)
        {
            var exception = Assert.Throws<OperationException>(() => ValidationHelper.CheckEndpointUrl(endpointUrl));
            Assert.Equal("Endpoint cannot be null!", exception.Message);
        }
        
        [Theory]
        [InlineData("foo")]
        [InlineData(" /bar")]
        public void CheckEndpointUrl_InvalidEndpoint_Error(string endpointUrl)
        {
            var exception = Assert.Throws<OperationException>(() => ValidationHelper.CheckEndpointUrl(endpointUrl));
            Assert.Equal("Endpoint must start with (or be equal to) a forward slash (/)!", exception.Message);
        }        
        
        #endregion
        
        #region MustHaveContent Tests
        [Fact]
        public void MustHaveContent_Success()
        {
            var rnd = new Random();
            var b = new byte[10];
            rnd.NextBytes(b);
            ValidationHelper.MustHaveContent(b);
        }
        
        [Fact]
        public void MustHaveContent_EmptyArray_Error()
        {
            var b = new byte[0];
            
            var exception = Assert.Throws<OperationException>(() => ValidationHelper.MustHaveContent(b));
            Assert.Equal("The file must have a content to be sent!", exception.Message);
        }
        
        #endregion
        
        #region CheckFilename Tests

        [Fact]
        public void CheckFilename_Success()
        {
            var file = Assembly.GetExecutingAssembly().Location;
            ValidationHelper.CheckFilename(file);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("      ")]
        public void CheckFilename_NullFilename_Error(string filename)
        {
            var exception = Assert.Throws<OperationException>(() => ValidationHelper.CheckFilename(filename));
            Assert.Equal("Filename cannot be null!", exception.Message);
        }
        
        [Fact]
        public void CheckFilename_NonExistingFilename_Error()
        {
            var filename = "c:\\this\\file\\does\\not\\exists.txt";
            var errorMessage = $"File not found! File: {filename}";
            
            var exception = Assert.Throws<OperationException>(() => ValidationHelper.CheckFilename(filename));
            Assert.Equal(errorMessage, exception.Message);
        }
        #endregion
    }
}