using BlazorRPG.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorRPG.Shared.Dtos
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Frodo";
        public int Strength { get; set; } = 10;
        public int Points { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Inteligence { get; set; } = 10;
        public int RPGClassId { get; set; }
        //public RpgClass Class { get; set; } 
        public string DocumentPDF { get; set; }
    }
}
