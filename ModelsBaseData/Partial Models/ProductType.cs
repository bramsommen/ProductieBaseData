using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsBaseData
{
    public partial class CyclusType
    {
        public override string ToString()
        {
            if (this.MachineOnderdeel != null)
            {
                return $"{Naam} - Machine Onderdeel: {this.MachineOnderdeel.Naam}";
            }

            return $"{Naam}";
        }
    }
}
