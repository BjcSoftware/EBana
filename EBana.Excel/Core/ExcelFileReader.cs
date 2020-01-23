﻿using EBana.Excel.Core.Exceptions;
using ExcelDataReader;
using System;
using System.IO;

namespace EBana.Excel.Core
{
    /// <summary>
    /// Permet de lire le contenu d'un fichier Excel.
    /// Utilise la bibliothèque ExcelDataReader: https://github.com/ExcelDataReader/ExcelDataReader.
    /// 
    /// Note: Excel n'a pas besoin d'être installé pour que la bibliothèque fonctionne.
    /// </summary>
    public class ExcelFileReader : IExcelFileReader
    {
        private readonly FileStream fileStream;
        private readonly IExcelDataReader excelReader;

        public ExcelFileReader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            try
            {
                fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            } 
            catch (IOException)
            {
                throw new FileOpenedByAnotherProcessException();
            }

            try
            {
                excelReader = ExcelReaderFactory.CreateReader(fileStream);
            }
            catch (ExcelDataReader.Exceptions.HeaderException)
            {
                throw new NotAnExcelFileException();
            }
        }

        ~ExcelFileReader()
        {
            excelReader?.Close();
            fileStream?.Close();
        }

        public string[,] GetCellsAsStringInRange(RectangularRange range)
        {
            if (range == null)
                throw new ArgumentNullException("range");

            var cells = new string[range.RowCount, range.ColumnCount];

            PutReaderOnRow(range.UpperLeftCorner.Row);
            for (uint row = 0; row < range.RowCount; row++)
            {
                excelReader.Read();
                for (uint column = 0; column < range.ColumnCount; column++)
                {
                    cells[row, column] = excelReader.GetValue((int)column)?.ToString();
                }
            }

            return cells;
        }

        private void PutReaderOnRow(uint row)
        {
            excelReader.Reset();
            uint offsetFromTop = row - 1;
            SkipLines(offsetFromTop);
        }

        private void SkipLines(uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                excelReader.Read();
            }
        }

        public uint RowCount
        {
            get
            {
                excelReader.Reset();

                uint rowCount = 0;
                while (excelReader.Read() && excelReader.GetValue(0) != null)
                {
                    rowCount++;
                }
                
                return rowCount;
            }
        }
    }
}