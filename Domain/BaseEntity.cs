﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public BaseEntity()
        {
        }

    }
}