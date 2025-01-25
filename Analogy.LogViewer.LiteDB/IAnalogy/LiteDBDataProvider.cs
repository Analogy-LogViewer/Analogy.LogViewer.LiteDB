using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
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
    public class LiteDBDataProvider : Template.OfflineDataProvider
    {
        public override Guid Id { get; set; } = new Guid("da3672d7-e16a-4bb2-a991-efaa3e2f7d3b");
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;

        public override string? OptionalTitle { get; set; } = "Full Database Load";
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
                foreach (var col in _db.GetCollectionNames())
                {
                    var bd = new BsonDocument();
                    Sql = $"SELECT $ FROM {col};";
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
                                m.Source = $"Table: {col}";
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
                }
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