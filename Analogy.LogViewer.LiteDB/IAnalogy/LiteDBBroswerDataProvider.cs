﻿using Analogy.Interfaces;
using Analogy.LogViewer.LiteDB.Properties;
using Analogy.LogViewer.Template.Managers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Analogy.LogViewer.LiteDB.IAnalogy
{
    public class LiteDBBroswerDataProvider : Template.OfflineDataProvider
    {
        public override Guid Id { get; set; } = new Guid("25b2b926-47f8-4f13-8db8-0803f8829eba");
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;

        public override string? OptionalTitle { get; set; } = "LiteDB db browser";
        public override string FileOpenDialogFilters { get; set; } = "LiteDB db file (*.db)|*.db";
        public override IEnumerable<string> SupportFormats { get; set; } = new[] { "*.db" };
        public override string? InitialFolderFullPath { get; set; } = Environment.CurrentDirectory;
        public override IEnumerable<(string OriginalHeader, string ReplacementHeader)> GetReplacementHeaders()
            => Array.Empty<(string, string)>();

        public override Task InitializeDataProvider(ILogger logger)
        {
            //do some initialization for this provider
            return base.InitializeDataProvider(logger);
        }

        public override IEnumerable<AnalogyLogMessagePropertyName> HideExistingColumns()
        {
            yield return AnalogyLogMessagePropertyName.Date;
            yield return AnalogyLogMessagePropertyName.Text;
            yield return AnalogyLogMessagePropertyName.Level;
            yield return AnalogyLogMessagePropertyName.Class;
            yield return AnalogyLogMessagePropertyName.Source;
            yield return AnalogyLogMessagePropertyName.User;
            yield return AnalogyLogMessagePropertyName.Class;
            yield return AnalogyLogMessagePropertyName.ProcessId;
            yield return AnalogyLogMessagePropertyName.ThreadId;
            yield return AnalogyLogMessagePropertyName.MachineName;
            yield return AnalogyLogMessagePropertyName.MethodName;
            yield return AnalogyLogMessagePropertyName.LineNumber;
            yield return AnalogyLogMessagePropertyName.RawText;
            yield return AnalogyLogMessagePropertyName.RawTextType;
            yield return AnalogyLogMessagePropertyName.Id;
        }

        public override void MessageOpened(IAnalogyLogMessage message)
        {
            //nop
        }

        public override async Task<IEnumerable<IAnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            var messages = new List<IAnalogyLogMessage>();
            
            //DataTable dt = new DataTable();
            //using (LiteDBCommand cmd = new LiteDBCommand(query, conn))
            //{
            //    using (LiteDBDataReader rdr = await cmd.ExecuteReaderAsync(token))
            //    {
            //        dt.Load(rdr);
            //    }
            //}
            //foreach (DataRow row in dt.Rows)
            //{
            //    DataTable dtData = new DataTable();
            //    try
            //    {
            //        string dataQuery = "SELECT * FROM " + row.ItemArray[0];
            //        using LiteDBCommand cmd = new LiteDBCommand(dataQuery, conn);
            //        using (LiteDBDataReader reader = await cmd.ExecuteReaderAsync(token))
            //        {
            //            DataTable schemaTable = reader.GetSchemaTable();

            //            foreach (DataRow rowName in schemaTable.Rows)
            //            {
            //                dtData.Columns.Add(rowName["ColumnName"].ToString(), typeof(object));
            //            }

            //            dtData.Load(reader);
            //        }
            //        foreach (DataRow entry in dtData.Rows)
            //        {
            //            AnalogyLogMessage m = new AnalogyLogMessage();
            //            m.Source = $"Table: {dtData.TableName}";
            //            StringBuilder sb = new StringBuilder(entry.ItemArray.Length);
            //            for (var i = 0; i < entry.ItemArray.Length; i++)
            //            {
            //                var key = dtData.Columns[i].ColumnName;
            //                var itm = entry.ItemArray[i];
            //                sb.AppendLine($"{key}: {itm}");
            //                m.AddOrReplaceAdditionalProperty(key, itm.ToString());
            //            }

            //            m.Text = sb.ToString();
            //            messages.Add(m);
            //            messagesHandler.AppendMessage(m, fileName);
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        LogManager.Instance.LogError(e, $"error:{e.Message}", e);
            //    }
            //}

            return messages;
        }
    }
}