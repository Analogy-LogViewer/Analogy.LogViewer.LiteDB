using Analogy.Interfaces;
using Analogy.LogViewer.LiteDB.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Analogy.LogViewer.LiteDB.IAnalogy
{
    public class PrimaryFactory : Analogy.LogViewer.Template.PrimaryFactory
    {
        internal static readonly Guid Id = new Guid("46147e9b-0f3d-410a-948e-f165af903f6a");
        public override Guid FactoryId { get; set; } = Id;

        public override string Title { get; set; } = "Analogy LiteDB";
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;

        public override IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = new List<AnalogyChangeLog>
        {
            new AnalogyChangeLog("Initial version", AnalogChangeLogType.None, "Lior Banai", new DateTime(2024, 03, 27), ""),
        };
        public override IEnumerable<string> Contributors { get; set; } = new List<string> { "Lior Banai" };
        public override string About { get; set; } = "Analogy LiteDB Data Provider";
    }
}