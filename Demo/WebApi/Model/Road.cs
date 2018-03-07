﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Model
{
    public class Road
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Paths { get; set; }
        public string Color { get; set; }
        public double Distance { get; set; }
        public string Direction { get; set; }
        public int MapId { get; set; }
        public virtual Map Map { get; set; }
    }
}