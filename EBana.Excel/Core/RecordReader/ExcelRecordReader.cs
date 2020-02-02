using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EBana.Excel.Core
{
    /// <summary>
    /// Permet de lire les lignes d'un fichier Excel (une ligne = un Record).
    /// Utilise la bibliothèque ExcelDataReader: https://github.com/ExcelDataReader/ExcelDataReader.
    /// 
    /// Note: Excel n'a pas besoin d'être installé pour que la bibliothèque fonctionne.
    /// </summary>
    public class ExcelRecordReader : IExcelRecordReader
    {
        private ExcelRecordReaderParams parameters;

        public ExcelRecordReader(ExcelRecordReaderParams parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            this.parameters = parameters;
        }

        public IEnumerable<Record> ReadAllRecordsFrom(ExcelSource source)
        {
            using (var stream = File.Open(source.FilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    return ReadRecordsFromReader(reader);
                }
            }
        }

        private IEnumerable<Record> ReadRecordsFromReader(IExcelDataReader reader)
        {
            SkipLines(reader, parameters.LineNumberWhereReadingStarts);

            var records = new List<Record>();
            var record = ReadNextRecord(reader);
            while (!record.IsEmpty())
            {
                records.Add(record);
                record = ReadNextRecord(reader);
            }

            return records;
        }

        private void SkipLines(IExcelDataReader reader, int lineToSkipCount)
        {
            foreach(var _ in Enumerable.Range(0, lineToSkipCount))
            {
                reader.Read();
            }
        }

        private Record ReadNextRecord(IExcelDataReader reader)
        {
            reader.Read();
            var fields = new List<string>();

            foreach (var fieldIndex in Enumerable.Range(0, parameters.FieldPerRecordCount))
            {
                fields.Add(reader.GetValue(fieldIndex)?.ToString());
            }

            return new Record(fields);
        }
    }
}
