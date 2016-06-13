using System;
using CommandLine;

namespace QuickMediaSorter
{
    public class Arguments
    {


        [Argument(ArgumentType.AtMostOnce, LongName = "Folder", ShortName = "f", HelpText = "")]
        public string Folder = "";

        [Argument(ArgumentType.AtMostOnce, LongName = "QmsProjectFile", ShortName = "q", HelpText = "")]
        public string QmsFile = "";


    }

}
