using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomashkaClientBase.Model
{
    public enum ContractStatuses
    {
        NotYetConcluded = 1,    // Ещё не заключён
        Concluded,              // Заключён
        Terminated              // Расторгнут
    }
}
