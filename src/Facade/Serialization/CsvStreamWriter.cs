namespace Linn.SalesAccounts.Facade.Serialization
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;

    public class CsvStreamWriter : StreamWriter
    {
        public CsvStreamWriter(Stream stream)
            : base(stream)
        {
        }

        public void WriteModel<T>(T value)
        {
            if (value is IEnumerable enumerable)
            {
                this.WriteRows(enumerable);
            }
            else
            {
                this.WriteRows((IEnumerable)new[] { value });
            }
        }

        public void WriteRows(IEnumerable rows)
        {
            // in the abscence of a generic type param, pull the type from the first
            // element
            var enumerator = rows.GetEnumerator();
            if (enumerator.MoveNext())
            {
                var first = enumerator.Current;

                this.DoWriteRows(rows, first.GetType());
            }
            else
            {
                this.DoWriteRows(rows, typeof(object));
            }
        }

        public void WriteRows<T>(IEnumerable<T> rows)
        {
            this.DoWriteRows(rows, typeof(T));
        }

        private void DoWriteRows(IEnumerable rows, Type modelType)
        {
            // this header line defines the CSV seperator as comma, fixes an issue with opening CSVs in Excel with unusual regional settings
            this.Write("sep=,");
            this.WriteLine();

            var columns = modelType.GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreDataMemberAttribute)))
                .Select(pi => pi.Name);

            foreach (var column in columns)
            {
                this.WriteValue(column);
            }

            this.WriteLine();

            // render data
            var en = rows.GetEnumerator();
            while (en.MoveNext())
            {
                foreach (var column in columns)
                {
                    var value = modelType.GetProperty(column).GetValue(en.Current, null);
                    this.WriteValue(value == null ? string.Empty : value.ToString());
                }

                this.WriteLine();
            }

            this.Flush();
        }

        private void WriteValue(string literal)
        {
            // enclose values in quote
            this.Write("\"");
            var line = literal.Replace("\"", "\"\"");
            this.Write(line);
            this.Write("\",");
        }
    }
}