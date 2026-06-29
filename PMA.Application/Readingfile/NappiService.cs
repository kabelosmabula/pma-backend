//using System;
//using System.Collections.Generic;
//using System.Text;
//using CsvHelper;
//using CsvHelper.Configuration;
//using PMA.Domain.Entities;
//using System.Formats.Asn1;
//using System.Globalization;
//using System.Text.Json;
using ClosedXML.Excel;
using System.Text.Json;
using PMA.Domain.Entities;

namespace PMA.Application.Readingfile
{
    //    public static class NappiService
    //    {
    //        public static async Task<string> ImportNappiToJson(string filePath)
    //        {
    //            if (!File.Exists(filePath))
    //                throw new FileNotFoundException("CSV file not found.", filePath);

    //           var records = new List<NappiItem>();

    //            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    //            {
    //                HeaderValidated = null,
    //                MissingFieldFound = null,
    //                BadDataFound = null
    //            };
    //            using var reader = new StreamReader(filePath);
    //            using var csv = new CsvReader(reader, config);

    //            csv.Read();
    //            csv.ReadHeader();
    //            try {
    //                await csv.ReadAsync();
    //                csv.ReadHeader();
    //                while (await csv.ReadAsync())
    //                {
    //                    var item = new NappiItem
    //                    {
    //                        Code = csv.GetField("NAPPI Code") ?? "empty",
    //                        Dosage = csv.GetField("Dosage\r\nForm") ?? "empty",
    //                        // PackSize = csv.GetField("PackSize") ?? "empty",
    //                        Name = csv.GetField("Product Name") ?? "empty"
    //                    };

    //                    records.Add(item);
    //                }
    //            } catch (Exception ex) 
    //            {
    //                throw new Exception($"Error processing CSV: {ex.Message}");
    //            }
    //            var filtered = records
    //                .Where(x => !string.IsNullOrEmpty(x.Code))
    //                .ToList();

    //            return JsonSerializer.Serialize(records, new JsonSerializerOptions
    //            {
    //                WriteIndented = true
    //            });
    //        }
    //    }
    //}




    public static class NappiService
    {
        public static async Task<string> ImportFromExcelAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found", filePath);

            var records = await Task.Run(() =>
            {
                var list = new List<NappiItem>();

                using var workbook = new XLWorkbook(filePath);
                var worksheet = workbook.Worksheet(1);

                var rows = worksheet.RangeUsed().RowsUsed().Skip(1); // skip header

                foreach (var row in rows)
                {
                    var item = new NappiItem
                    {
                        Code = row.Cell(4).GetValue<string>(),
                        Name = row.Cell(6).GetValue<string>(),
                        //PackSize = row.Cell(3).GetValue<string>(),
                        Dosage = row.Cell(5).GetValue<string>()
                    };

                    if (!string.IsNullOrWhiteSpace(item.Code))
                        list.Add(item);
                }

                return list;
            });

            return JsonSerializer.Serialize(records, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}