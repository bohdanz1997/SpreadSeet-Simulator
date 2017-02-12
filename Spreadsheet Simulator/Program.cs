using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spreadsheet.Core;
using Spreadsheet.Core.ProcessingStrategies;
using System.IO;

namespace Spreadsheet_Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { "input.txt", "output.txt" };
            
            try
            {
                var input = File.Open(args[0], FileMode.Open);
                var output = File.Create(args[1]);
                using (var reader = new SheetReader(input))
                {
                    Console.WriteLine(Resources.ReadingTable);
                    var spreadsheet = reader.ReadSpreadsheet();

                    Console.WriteLine(Resources.Processing);
                    var processor = new SheetProcessor(spreadsheet);
                    var result = processor.Evaluate(new ParallelProcessingStrategy());

                    Console.WriteLine(Resources.WritingResult);
                    using (var writer = new SheetWriter(output))
                    {
                        writer.WriteSpreedsheat(result);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            Console.Write(Resources.PressAnyKeyForExit);
            Console.ReadKey();
        }
    }
}
