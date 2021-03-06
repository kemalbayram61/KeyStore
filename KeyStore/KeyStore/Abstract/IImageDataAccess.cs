﻿using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyStore.Abstract
{
    interface IImageDataAccess
    {
        Imagee AddImage(Imagee image);
        List<PackageObject> GetAllImage();
        Imagee GetImageById(int image_id);
        bool DeleteImage(int image_id);
        Imagee UpdateImage(Imagee image);
    }
}
