using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
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
    public partial class PhishingPage : ComponentBase, IDisposable
    {
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
        IPhishingHttpHelper _phishingHttpHelper { get; set; }

       
        [Inject]
        RFDStateManagement _stateManagement { get; set; }
        [Inject]
        public HubConnection _hubConnection { get; set; }
        [Inject]
        NavigationManager _navigationManager { get; set; }

        bool IsHubConnection => _hubConnection?.State == HubConnectionState.Connected;

        PhishingParamerters _paramerters { get; set; } = new PhishingParamerters();
        MetaData _metaData { get; set; } = new MetaData();
        MetronicAlert _alert = new MetronicAlert();
        bool _updateModal= false;
        bool _addModal = false;
        bool _transection = false;

        List<PhishingModel> _phishings = new List<PhishingModel>();
    

        Phishing _addModel { get; set; } = new Phishing();
        Phishing _updateModel { get; set; } = new Phishing();
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
           


            if (!IsHubConnection)
                await _hubConnection.StartAsync();
            _hubConnection.On<string, string>("ReceivePhishing", async (flag, result) =>
              {
                  var phishing = JsonConvert.DeserializeObject<PhishingModel>(result);
                  var phish = _phishings?.FirstOrDefault(x => x.Id == phishing.Id);
                  if (phish != null)
                  {
                      phish.Transections = phishing.Transections;
                      phish.Status = phishing.Status;
                      await InvokeAsync(StateHasChanged);
                  }
              });
        }
        public async Task GetAllAsync()
        {
            var result = await _phishingHttpHelper.GetAllAsync(_paramerters);

            if (result.IsSucceeded)
            {
                _phishings = result?.Data?.Items;
                _metaData = result?.Data?.MetaData;
            }
            else
            {
                _phishings = new List<PhishingModel>();
            }

        }
        private void SelectedPage(int page)
        {
            _paramerters.PageNumber = page;
            _stateManagement.Notify(this, "phishing");
        }
        private void Search()
        {
            _stateManagement.Notify(this, "phishing");
        }
       
        public async Task Submit()
        {
            var result = await _phishingHttpHelper.AddAsync(_addModel);

            Indicator = true;

            if (result.IsSucceeded)
            {
                _alert.Type = AlertType.Success;
                _alert.Message = new string[] { "Başarılı bir şekilde kaydedildi." };
            }
            else
            {
                _alert.Type = AlertType.Error;
                _alert.Message = result.Message;
            }
            _alert.IsVisible = true;
            _stateManagement.Notify(this, "phishing");
            Indicator = false;
            _addModal = false;
        }
        public void SetUpdateModel(Phishing model)
        {
            _updateModel = model;
            _updateModal = true;
        }
        public async Task Update()
        {
            var result = await _phishingHttpHelper.UpdateAsync(_updateModel);

            Indicator = true;

            if (result.IsSucceeded)
            {
                _alert.Type = AlertType.Success;
                _alert.Message = new string[] { "Başarılı bir şekilde güncellendi." };
            }
            else
            {
                _alert.Type = AlertType.Error;
                _alert.Message = result.Message;
            }
            _alert.IsVisible = true;
            _stateManagement.Notify(this, "phishing");
            Indicator = false;

            _updateModal = false;

        }
        public async Task Delete(int id)
        {
            var result = await _phishingHttpHelper.DeleteAsync(id);

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
            _stateManagement.Notify(this, "phishing");
            Indicator = false;
        }
        public async Task StateChange(ComponentBase source, string flag)
        {
            switch (flag)
            {
                case "phishing":
                    await GetAllAsync();
                    break;
            }
            await InvokeAsync(StateHasChanged);
        }
        public void CheckAll()
        {
            if (!_check)
                _phishings.ForEach(x => { x.IsChecked = true; });
            else
                _phishings.ForEach(x => { x.IsChecked = false; });
        }
        public async Task Start(List<PhishingModel> phishings)
        {
            List<Task> tasks = new List<Task>();
            List<string> errors = new List<string>();
            foreach (var item in phishings)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var result = await _phishingHttpHelper.StartCampaign(item);
                    if (!result.IsSucceeded)
                    {
                        _alert.Type = AlertType.Error;
                        _alert.Message = result.Message;
                        _alert.IsVisible = true;
                    }
                }));

            }
            await Task.WhenAll(tasks);
        }
        public async Task PauseCampaignAsync(PhishingModel phishing)
        {
            var result = await _phishingHttpHelper.PauseCampaign(phishing.Id);
            if (!result.IsSucceeded)
            {
                _alert.Type = AlertType.Error;
                _alert.Message = result.Message;
                _alert.IsVisible = true;
            }
        }
        public void ViewTransection(PhishingModel phishing)
        {
            _phishings.ForEach(x => x.IsSelect = false);
            _phishings.FirstOrDefault(x => x.Id == phishing.Id).IsSelect = true;
            _transection = true;
        }
        public async Task Duplicate(Phishing phishing)
        {
            var result = await _phishingHttpHelper.Duplicate(phishing);
            if (result.IsSucceeded)
            {
                _alert.Type = AlertType.Success;
                _alert.Message = new string[] { "Başarılı bir şekilde kopyalandı." };
            }
            else
            {
                _alert.Type = AlertType.Error;
                _alert.Message = result.Message;
            }
            _alert.IsVisible = true;
            _stateManagement.Notify(this, "phishing");
        }
        public async Task SelectedDelete()
        {
            var postData = _phishings.Where(x => x.IsChecked).Select(x => new Phishing { Id = x.Id }).ToList();
            var result = await _phishingHttpHelper.SelectedDelete(postData);
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
            _stateManagement.Notify(this, "phishing");
        }
        public void Dispose()
        {
            _hubConnection?.Remove("ReceivePhishing");
        }
    }
}
