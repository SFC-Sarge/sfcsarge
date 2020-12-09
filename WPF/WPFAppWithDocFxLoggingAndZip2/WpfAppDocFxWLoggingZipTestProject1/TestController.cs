using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppDocFxWLoggingZipTestProject1
{
    /// <summary>Class TestController</summary>
    public class TestController
    {
        private readonly ILogger _logger;
        private string passedMessage;
        public TestController(ILogger<TestController> logger, string message)
        {
            _logger = logger;
            passedMessage = message;
        }

        /// <summary>Gets the message.</summary>
        /// <returns>System.String.</returns>
        public string GetMessage()
        {
            _logger.LogDebug("Index Method Called!!!");
            return passedMessage;
        }
    }
}
