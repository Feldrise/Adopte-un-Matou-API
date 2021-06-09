using AdopteUnMatou.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Settings
{
    public class AdopteUnMatouSettings : IAdopteUnMatouSettings
    {
        public string ApiSecret { get; set; }
    }
}
