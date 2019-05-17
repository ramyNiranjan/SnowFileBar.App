using SnowFileBar.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFileBar.App.FileManagerService
{
    public class FileParse
    {
        public List<FileBarData> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                      .Select(ParsingFile)
                      .ToList();
        }

        private static FileBarData ParsingFile(string line)
        {
            var colums = line.Split(':');
            return new FileBarData
            {
                ColourName = colums[1],
                Size = int.Parse(colums[2])
            };


        }
    }
}
