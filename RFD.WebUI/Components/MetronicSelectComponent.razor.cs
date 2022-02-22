using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Components
{
    public partial class MetronicSelectComponent<T>
    {

        [Inject] IJSRuntime JsRuntime { get; set; }




        string _id;
        [Parameter]
        public string Id
        {
            get => _id ?? $"{uid}";
            set => _id = value;
        }

        readonly string uid = Guid.NewGuid().ToString().ToLower().Replace("-", "");

        bool isPopupVisible;
        [Parameter]
        public bool IsPopupVisible
        {
            get => isPopupVisible;
            set
            {

                isPopupVisible = value;
                if (value)
                {
                    Focus(Id);
                }
            }
        }

        public async Task Focus(string elementId)
        {
            await JsRuntime.InvokeVoidAsync(
            "eval",
           $"document.getElementById('{Id.ToString()}').focus();");
        }

        [Parameter]
        public T? Value { get; set; }
        [Parameter]
        public string Placeholder { get; set; }
        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<string> OnFilter { get; set; }


        [Parameter]
        public IList<SelectOption<T>> Data { get; set; }
        [Parameter]
        public bool IsSearchLocal { get; set; } = false;
        [Parameter]
        public bool Multiple { get; set; } = false;
        [Parameter]
        public int SelectCount { get; set; } = 5;

        //private bool isVisible = false;
        public IList<SelectOption<T>> LocalData { get; set; }
        public IList<SelectOption<T>> SelectedData { get; set; } = new List<SelectOption<T>>();
        public string searchText;
        string search;
        string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnFiltered();

            }
        }
        //[JSInvokable("MetronicSelectClose")]
        //public void Close()
        //{
        //    isVisible = false;
        //    StateHasChanged();
        //}

        protected override async Task OnInitializedAsync()
        {

            await JsRuntime.InvokeVoidAsync("outsideClickHandler.addEvent", Id, DotNetObjectReference.Create(this));
            LocalData = Data;
            if (!Value.Equals(default(T)))
            {
                if (Multiple)
                {
                    var multipleValue = Value.ToString().Split(',');
                    foreach (var item in multipleValue)
                    {
                        LocalData.ToList().ForEach(x =>
                        {
                            if (x.Value.ToString() == item)
                                SetItem(x);
                        });
                    }
                }
                else
                {
                    var item = LocalData.FirstOrDefault(x => x.Value.Equals(Value));
                    if (item is not null)
                        SetItem(item);
                }
            }
            await base.OnInitializedAsync();
        }

        [JSInvokable("MetronicPopupClose")]
        public void ClosePopup()
        {
            IsPopupVisible = false;
            StateHasChanged();
            // IsPopupVisible = false;
        }

        protected override void OnParametersSet()
        {
            LocalData = Data;
            base.OnParametersSet();
        }
        public void OnFiltered()
        {
            if (IsSearchLocal)
            {
                if (!string.IsNullOrEmpty(search))
                {
                    LocalData = Data.Where(x => x.Text.ToLower().Contains(search.ToLower())).ToList();
                }
                else
                {
                    LocalData = Data;
                }
                StateHasChanged();

            }
            else
            {
                OnFilter.InvokeAsync(search);
            }
        }
        public void Clear()
        {
            IsPopupVisible = false;
            SelectedData.Clear();
            Value = default(T);
            ValueChanged.InvokeAsync(Value);
        }

        private Task OnValueChanged(ChangeEventArgs e)
        {
            Value = (T)e.Value;
            return ValueChanged.InvokeAsync(Value);
        }


        public void SetItem(SelectOption<T> model)
        {
            if (!Multiple)
            {
                SelectedData.Clear();
                SelectedData.Add(model);
                IsPopupVisible = false;
            }
            else
            {
                var selected = SelectedData.FirstOrDefault(x => x.Value.Equals(model.Value));
                if (selected is not null)
                    SelectedData.Remove(selected);
                else if (SelectedData.Count() < SelectCount)
                    SelectedData.Add(model);
                StateHasChanged();
            }
            var val = String.Join(",", SelectedData.Select(x => x.Value.ToString()));
            Value = (T)Convert.ChangeType(val, typeof(T));
            ValueChanged.InvokeAsync(Value);
        }



    }

    public class SelectOption<T>
    {
        public T Value { get; set; }
        public string Text { get; set; }
    }
}
