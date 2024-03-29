﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bCloud_v1.Models.Home
{
     public class FileDetails
    {
        public string Name { get; set; }
        public string Path { get; set; }

    }
    public class DirectoryDetails
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
    public class FilesViewModel
    {
        public List<FileDetails> Files { get; set; } 
            = new List<FileDetails>();
        public List<DirectoryDetails> Directories { get; set; }
            = new List<DirectoryDetails>();
    }
}
