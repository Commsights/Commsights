﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class BaseViewModel
    {
        public int ID { get; set; }
        public int IndustryIDUploadScan { get; set; }
        public int IndustryIDUploadGoogleSearch { get; set; }
        public int IndustryIDUploadAndiSource { get; set; }
        public int IndustryIDUploadYounet { get; set; }
        public DateTime DatePublish { get; set; }

    }
}