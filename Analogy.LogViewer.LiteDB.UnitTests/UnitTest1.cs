using Analogy.LogViewer.LiteDB.IAnalogy;
using Analogy.LogViewer.Sqlite.UnitTests;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.LiteDB.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private string Folder { get; } = Environment.CurrentDirectory;
        [TestMethod]
        public async Task ReadDBTest()
        {
            //CancellationTokenSource cts = new CancellationTokenSource();
            //string fileName = @"C:\Lior\state.db";//Path.Combine(Folder, "db", "example.db");
            //MessageHandlerForTesting forTesting = new MessageHandlerForTesting();
            //LiteDBDataProvider reader = new LiteDBDataProvider();
            //LoggerFactory factory = new LoggerFactory();
            //Microsoft.Extensions.Logging.ILogger logger = factory.CreateLogger("test");
            //await reader.InitializeDataProvider(logger);
            //var messages = (await reader.Process(fileName, cts.Token, forTesting)).ToList();
        }
    }
}