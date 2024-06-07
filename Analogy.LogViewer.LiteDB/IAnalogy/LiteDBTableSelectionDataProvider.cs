using Analogy.Interfaces;
using Analogy.LogViewer.LiteDB.Forms;
using Analogy.LogViewer.LiteDB.Properties;
using Analogy.LogViewer.Template.Managers;
using LiteDB;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.LiteDB.IAnalogy
{
    public class LiteDBTableSelectionDataProvider : Template.OfflineDataProvider
    {
        public override Guid Id { get; set; } = new Guid("641c90dc-6442-40c0-a31e-ffb350d3031e");
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;

        public override string? OptionalTitle { get; set; } = "Table Selection";
        public override string FileOpenDialogFilters { get; set; } = "LiteDB db file (*.db)|*.db";
        public override IEnumerable<string> SupportFormats { get; set; } = new[] { "*.db" };
        public override string? InitialFolderFullPath { get; set; } = Environment.CurrentDirectory;
        public override IEnumerable<(string OriginalHeader, string ReplacementHeader)> GetReplacementHeaders()
            => Array.Empty<(string, string)>();
        public string Collection { get; set; } = "";
        public string Sql { get; set; } = "";
        public const int RESULTLIMIT = 1000;
        public bool LimitExceeded { get; set; }
        public override Task InitializeDataProvider(ILogger logger)
        {
            //do some initialization for this provider
            return base.InitializeDataProvider(logger);
        }

        public override void MessageOpened(IAnalogyLogMessage message)
        {
            //nop
        }

        public override async Task<IEnumerable<IAnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            var messages = new List<IAnalogyLogMessage>();
            ConnectionString connection = new ConnectionString();
            connection.Connection = ConnectionType.Direct;

            connection.Filename = fileName;
            connection.ReadOnly = true;
            connection.Upgrade = false;
            connection.Password = null;
            connection.InitialSize = 0;
            var _db = new LiteDatabase(connection);
            try
            {
                // force open database
                var uv = _db.UserVersion;
                var names = _db.GetCollectionNames().ToList();
                TableSelectionForm selection = new TableSelectionForm(names, (name) =>
                {
                    var bd = new BsonDocument();
                    Sql = $"SELECT $ FROM {name};";
                    var sql = new StringReader(Sql.Trim());

                    while (sql.Peek() >= 0 && _db != null)
                    {
                        using var reader = _db.Execute(sql, bd);
                        var items = ReadResult(reader);
                        var sb = new StringBuilder();
                        using (var writer = new StringWriter(sb))
                        {
                            var json = new JsonWriter(writer) { Pretty = true, Indent = 2, };
                            foreach (var item in items)
                            {
                                sb.Clear();
                                AnalogyLogMessage m = new AnalogyLogMessage();
                                m.Source = $"Table: {name}";
                                var keys = ((BsonDocument)item).Keys.ToList();
                                var values = ((BsonDocument)item).Values.ToList();

                                for (var i = 0; i < keys.Count; i++)
                                {
                                    var key = keys[i];
                                    var itm = values[i];
                                    sb.AppendLine($"{key}: {itm}");
                                    m.AddOrReplaceAdditionalProperty(key, itm.ToString());
                                }

                                json.Serialize(item);
                                m.Text = sb.ToString();
                                m.RawText = JsonSerializer.Serialize(item);
                                m.RawTextType = AnalogyRowTextType.JSON;
                                messages.Add(m);
                                messagesHandler.AppendMessage(m, fileName);
                            }
                        }
                    }
                });
                selection.Show();
                selection.BringToFront();
                selection.Focus();
            }
            catch (Exception ex)
            {
                _db?.Dispose();
            }
            return messages;
        }
        public List<BsonValue> ReadResult(IBsonDataReader reader)
        {
            var result = new List<BsonValue>();
            this.LimitExceeded = false;
            this.Collection = reader.Collection;

            var index = 0;
            var hasLimit = this.Sql.IndexOf("LIMIT ", StringComparison.OrdinalIgnoreCase) >= 0;

            while (reader.Read())
            {
                if (index++ >= RESULTLIMIT && hasLimit == false)
                {
                    this.LimitExceeded = true;
                    break;
                }

                result.Add(reader.Current);
            }

            return result;
        }
    }
}