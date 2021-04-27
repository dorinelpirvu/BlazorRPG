using BlazorRPG.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Client.Services
{
    public interface IRpgClassService
    {
        List<RpgClass> RpgClasses { get; set; }
        Task LoadRpgClasses();
    }
}
