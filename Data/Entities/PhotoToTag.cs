﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskanioPhotoSite.Data.Entities
{
    public class PhotoToTag : Entity
    {
        public int Id { get; set; }

        public int PhotoId { get; set; }

        public int TagId { get; set; }
    }
}
