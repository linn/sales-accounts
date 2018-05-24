namespace Linn.SalesAccounts.Facade.Facade.Csv
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Linn.SalesAccounts.Facade.Serialization;

    using Nancy;
    using Nancy.Responses.Negotiation;

    public class CsvSerializer : ISerializer
    {
        public IEnumerable<string> Extensions
        {
            get { yield return "csv"; }
        }

        public bool CanSerialize(MediaRange mediaRange)
        {
            return IsCsvType(mediaRange.ToString());
        }

        public void Serialize<TModel>(MediaRange mediaRange, TModel model, Stream outputStream)
        {
            using (var writer = new CsvStreamWriter(outputStream))
            {
                writer.WriteModel(model);
            }
        }

        public void Serialize<TModel>(string contentType, TModel model, Stream outputStream)
        {
            using (var writer = new CsvStreamWriter(outputStream))
            {
                writer.WriteModel(model);
            }
        }

        private static bool IsCsvType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                return false;
            }

            var contentMimeType = contentType.Split(';')[0];

            return contentMimeType.Equals("text/csv", StringComparison.InvariantCultureIgnoreCase) ||
                   contentMimeType.StartsWith("text/csv", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}