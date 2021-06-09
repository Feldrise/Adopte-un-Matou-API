using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API.Settings.Interfaces
{
    public interface IAdopteUnMatouSettings
    {
        string ApiSecret { get; set; }
    }
}
