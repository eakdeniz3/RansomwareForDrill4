using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.Entities.Enum;
using RFD.WebUI.Components;
using RFD.WebUI.Infrastructer.Helpers;
using RFD.WebUI.Infrastructer.StateManagement;
using RFD.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Pages
{
    public partial class SummaryPage : ComponentBase, IDisposable
    {
        List<SelectOption<int>> select = new List<SelectOption<int>>(){

new SelectOption<int>{
Text="Insider",
    Value=0
},
new SelectOption<int>{
Text="Phishing",
    Value=1
}
};
        public bool IsBusy { get; set; } = false;

        bool indicator;
        private bool Indicator
        {
            get
            {
                return indicator;
            }
            set
            {
                indicator = value;
                StateHasChanged();
            }
        }
        [Inject]
        ISummaryHttpHelper _summaryHttpHelper { get; set; }


        [Inject]
        RFDStateManagement _stateManagement { get; set; }
        [Inject]
        public HubConnection _hubConnection { get; set; }
        [Inject]
        NavigationManager _navigationManager { get; set; }

        bool IsHubConnection => _hubConnection?.State == HubConnectionState.Connected;

        SummaryParamerters _paramerters { get; set; } = new SummaryParamerters();
        MetaData _metaData { get; set; } = new MetaData();
        MetronicAlert _alert = new MetronicAlert();

        List<SummaryModel> _summaries = new List<SummaryModel>();

        CountData _countData = new CountData();


        Insider _addModel { get; set; } = new Insider();
        Insider _updateModel { get; set; } = new Insider();
        bool _check = false;
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _stateManagement.OnStateChanged += async (source, flag) => await StateChange(source, flag);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        protected async override Task OnInitializedAsync()
        {
            await GetAllAsync();
            await GetCountAsync();


            //if (!IsHubConnection)
            //    await _hubConnection.StartAsync();
            //_hubConnection.On<string, string>("ReceiveInsider", async (flag, result) =>
            //  {
            //      var insider = JsonConvert.DeserializeObject<InsiderModel>(result);
            //      var phish = _summaryModels?.FirstOrDefault(x => x.Id == insider.Id);
            //      //if (phish != null)
            //      //{
            //      //    phish.Transections = insider.Transections;
            //      //    phish.Status = insider.Status;
            //      //    await InvokeAsync(StateHasChanged);
            //      //}
            //  });
        }
        public async Task GetAllAsync()
        {
            var result = await _summaryHttpHelper.GetAllAsync(_paramerters);

            if (result.IsSucceeded)
            {
                _summaries = result?.Data?.Items;
                _metaData = result?.Data?.MetaData;
            }
            else
            {
                _summaries = new List<SummaryModel>();
            }

        }

        public async Task GetCountAsync()
        {
            var result = await _summaryHttpHelper.GetCountDataAsync();

            if (result.IsSucceeded)
            {
                _countData = result.Data;
            }
            else
            {
                _countData = new CountData();
            }

        }


        private void SelectedPage(int page)
        {
            _paramerters.PageNumber = page;
            _stateManagement.Notify(this, "insider");
        }

        public void SelectedPageSize(ChangeEventArgs e)
        {
            _paramerters.PageSize = int.Parse(e?.Value.ToString());
            _stateManagement.Notify(this, "insider");
        }


        private void Search()
        {
            _stateManagement.Notify(this, "insider");
        }



        public async Task Delete(int id)
        {

            var result = await _summaryHttpHelper.DeleteAsync(id);

            Indicator = true;

            if (result.IsSucceeded)
            {
                _alert.Type = AlertType.Success;
                _alert.Message = new string[] { "Başarılı bir şekilde silindi." };
            }
            else
            {
                _alert.Type = AlertType.Error;
                _alert.Message = result.Message;
            }
            _alert.IsVisible = true;
            _stateManagement.Notify(this, "insider");
            Indicator = false;
        }
        public async Task StateChange(ComponentBase source, string flag)
        {
            switch (flag)
            {
                case "insider":
                    await GetAllAsync();
                    await GetCountAsync();
                    break;
            }
            await InvokeAsync(StateHasChanged);
        }
        public void CheckAll()
        {
            if (!_check)
                _summaries.ForEach(x => { x.IsChecked = true; });
            else
                _summaries.ForEach(x => { x.IsChecked = false; });
        }



        public async Task SelectedDelete()
        {
            var postData = _summaries.Where(x => x.IsChecked).Select(x => new Summary { Id = x.Id }).ToList();
            var result = await _summaryHttpHelper.SelectedDelete(postData);
            if (result.IsSucceeded)
            {
                _alert.Type = AlertType.Success;
                _alert.Message = new string[] { "Başarılı bir şekilde Silindi." };
            }
            else
            {
                _alert.Type = AlertType.Error;
                _alert.Message = result.Message;
            }
            _alert.IsVisible = true;
            _stateManagement.Notify(this, "insider");
        }
        public void Dispose()
        {
            _hubConnection?.Remove("ReceiveInsider");
        }
    }
}
