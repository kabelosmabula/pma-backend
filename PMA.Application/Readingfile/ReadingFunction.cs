using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.Readingfile
{
    public class ReadingFunction
    {
        static void Main(string[] args)
        {
            //        try
            //        {
            //            var path = "Downloads/NAPPI-Coded-Essential-Medicines-List-_February-2026.xlsx";

            //            var json = NappiService.ImportNappiToJson(path);

            //            Console.WriteLine(json);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine($"Error: {ex.Message}");
            //        }
            //        Console.ReadLine();
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "C:\\Users\\siphi\\Downloads\\NAPPI-Coded-Essential-Medicines-List-_February-2026.xlsx");


                Console.WriteLine("Reading from: " + filePath);

                //var json = await NappiService.ImportFromExcelAsync(filePath);

               // Console.WriteLine(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

}
}
