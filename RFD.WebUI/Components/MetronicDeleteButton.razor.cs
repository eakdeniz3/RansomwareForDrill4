using Microsoft.AspNetCore.Components;
using RFD.Entities.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Components
{
    public partial class MetronicDeleteButton
    {
        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public EventCallback<int> OnDelete { get; set; }

        public bool IsVisible { get; set; }

        private bool isBusy = false;


        private async Task Delete(int id)
        {
            isBusy = true;
            await OnDelete.InvokeAsync(id);
            isBusy = false;
            IsVisible = false;
            
        }
    }

}
