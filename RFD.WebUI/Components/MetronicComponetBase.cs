using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Components
{
    public partial class MetronicComponetBase:ComponentBase
    {
      [Inject]  IJSRuntime JsRuntime { get; set; }


      

        string _id;
        [Parameter]
        public string Id
        {
            get => _id ?? $"{uid}";
            set => _id = value;
        }

        readonly string uid = Guid.NewGuid().ToString().ToLower().Replace("-", "");
        [Parameter]
        public bool IsPopupVisible { get; set; }

        public async Task OnInitAsync()
        {
            var lDotNetReference = DotNetObjectReference.Create(this);
            await JsRuntime.InvokeVoidAsync("GLOBAL.SetDotnetReference", lDotNetReference, Id);

        }
        //protected override async Task OnInitializedAsync()
        //{
        //    await OnInitAsync();
        //  //  return base.OnInitializedAsync();
        //}

        [JSInvokable("MetronicPopupClose")]
        public void ClosePopup(DotNetObjectReference<MetronicComponetBase> dotNet)
        {
            dotNet.Value.IsPopupVisible = false;
            dotNet.Value.StateHasChanged();
           // IsPopupVisible = false;
        }

    }
}
