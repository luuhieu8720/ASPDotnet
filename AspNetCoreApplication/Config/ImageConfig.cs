using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Config
{
    public class ImageConfig
    {
        public string CloudinaryCloud { get; set; }
        public string ApiKey{ get; set; }
        public string ApiSecret{ get; set; }
        public int CoverLimitWidth { get; set; }
        public int CoverLimitHeight { get; set; }
    }
}
