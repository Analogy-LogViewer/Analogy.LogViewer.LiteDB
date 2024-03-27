using Analogy.Interfaces;
using Analogy.LogViewer.Template;
using System;
using System.Collections.Generic;

namespace Analogy.LogViewer.LiteDB.IAnalogy
{
    public class LiteDBDataProviderFactory : DataProvidersFactory
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override string Title { get; set; } = "Analogy LiteDB db browser";

        public override IEnumerable<IAnalogyDataProvider> DataProviders { get; set; } = new List<IAnalogyDataProvider>
        {
            new LiteDBDataProvider(),
        };
    }
}