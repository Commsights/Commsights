﻿using Commsights.Data.DataTransferObject;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class BaiVietUploadCountRepository : Repository<BaiVietUploadCount>, IBaiVietUploadCountRepository
    {
        private readonly CommsightsContext _context;

        public BaiVietUploadCountRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
    }
}
