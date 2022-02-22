using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Components
{
    public partial class MetronicAlertComponent : ComponentBase
    {
        private MetronicAlert metronicAlert;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Parameter]
        public MetronicAlert Model
        {
            get
            {
                return metronicAlert;
            }

            set
            {
                metronicAlert = value;


                //if (metronicAlert.IsVisible)
                //{


                //    Task.Run(async () =>
                //                               {

                //                                   await Task.Delay(5000);
                //                                   if (metronicAlert.IsVisible)
                //                                   {
                //                                       Close();
                //                                       await InvokeAsync(StateHasChanged)  ; 
                //                                   }
                                               

                //                               });

                //}

            }
        }




        public void Close()
        {
            Model.IsVisible = false;
        }


    }
    public enum AlertType
    {
        Error,
        Success
    }

    public class MetronicAlert
    {
        public AlertType Type { get; set; } = AlertType.Success;

        public bool IsVisible { get; set; }
        public string[] Message { get; set; }
    }
}

