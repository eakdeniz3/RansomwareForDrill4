using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Components
{
    public partial class MetronicModalComponent
    {
        [Parameter]
        public RenderFragment? Body { get; set; }
        [Parameter]
        public RenderFragment? Header { get; set; }
        [Parameter]
        public RenderFragment? Footer { get; set; }
        [Inject] IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public EventCallback<bool> IsShowChanged { get; set; }


      
      
        protected async override Task OnInitializedAsync()
        {
            await JsRuntime.InvokeVoidAsync("outsideClickHandler.addEvent", Id, DotNetObjectReference.Create(this));

        }
        [JSInvokable("MetronicPopupClose")]
        public async Task ClosePopupAsync()
        {
            IsShow = false;
            await IsShowChanged.InvokeAsync(IsShow);            // IsPopupVisible = false;
        }


        string _id;
        [Parameter]
        public string Id
        {
            get => _id ?? $"modal-{uid}";
            set => _id = value;
        }

        readonly string uid = Guid.NewGuid().ToString().ToLower().Replace("-", "");

        [Parameter]
        public bool IsShow { get; set; } = false;

     
        
    }
}
