namespace Linn.SalesAccounts.Facade.Facade.Csv
{
    using System;
    using System.IO;

    using Nancy;

    public class CsvResponse<TModel> : Response
    {
        public CsvResponse(TModel model, ISerializer serializer)
        {
            if (serializer == null)
            {
                throw new InvalidOperationException("CSV Serializer not set");
            }

            this.Contents = GetCsvContents(model, serializer);
            this.ContentType = "text/csv";
            this.StatusCode = HttpStatusCode.OK;
        }

        private static Action<Stream> GetCsvContents(TModel model, ISerializer serializer)
        {
            return stream => serializer.Serialize("text/csv", model, stream);
        }
    }

    public class CsvResponse : CsvResponse<object>
    {
        public CsvResponse(object model, ISerializer serializer)
            : base(model, serializer)
        {
        }
    }
}