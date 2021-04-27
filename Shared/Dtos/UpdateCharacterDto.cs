﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Shared.Dtos
{
    public class UpdateCharacterDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int Strength { get; set; } = 10;
        public int Points { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Inteligence { get; set; } = 10;
        public RpgClass Class { get; set; }
        public int RpgClassId { get; set; }
        public string  DocumentPDF { get; set; }
    }
}
