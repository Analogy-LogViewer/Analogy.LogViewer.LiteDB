using Analogy.Interfaces;
using Analogy.Interfaces.WinForms;
using Analogy.LogViewer.Template;
using Analogy.LogViewer.Template.WinForms;
using System;
using System.Collections.Generic;

namespace Analogy.LogViewer.LiteDB.IAnalogy
{
    public class LiteDBDataProviderFactory : DataProvidersFactoryWinForms
    {
        public override Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public override string Title { get; set; } = "LiteDB File";

        public override IEnumerable<IAnalogyDataProvider> DataProviders { get; set; } = new List<IAnalogyDataProvider>
        {
            new LiteDBDataProvider(),
            new LiteDBTableSelectionDataProvider(),
        };
    }
}