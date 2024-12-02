using BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoCWPF.Solutions
{
    public abstract class DayBase
    {
        public List<string> Input { get; set; }
        public string RawInput { get; set; }

        public DayBase(int day, int part)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $@"InputFiles\Day{day}\part{part}.txt");
            Helper.GetFileData(path, out var input, out var rawInput);
            Input = input;
            RawInput = rawInput;
        }

        public abstract string Part1();
        public abstract string Part2();
    }
}
