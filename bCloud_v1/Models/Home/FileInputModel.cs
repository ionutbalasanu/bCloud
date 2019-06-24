using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bCloud_v1.Models.Home
{
    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
    }
}
