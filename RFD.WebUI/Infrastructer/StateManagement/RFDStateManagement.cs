using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.StateManagement
{
    public class RFDStateManagement
    {

        public event Action<ComponentBase, string> OnStateChanged;

        public void Notify(ComponentBase component, string flag)
        {
            OnStateChanged?.Invoke(component,flag);
        }

    }
}
