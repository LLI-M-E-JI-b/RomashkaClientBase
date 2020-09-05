using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RomashkaClientBase.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContractStatuses ContractStatus { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
