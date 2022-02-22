using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.Entities.Enum;
using RFD.WebUI.Components;
using RFD.WebUI.Infrastructer.Extensions;
using RFD.WebUI.Infrastructer.Helpers;
using RFD.WebUI.Infrastructer.StateManagement;
using RFD.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Pages
{
    public partial class InsiderPage : ComponentBase, IDisposable
    {
        public bool IsBusy { get; set; } = false;
        public static volatile Dictionary<int, bool> _pauseDict = new Dictionary<int, bool>();
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
        IInsiderHttpHelper _insiderHttpHelper { get; set; }

        [Inject]
        IRFDHelper _rFDHelper { get; set; }

        //[Inject]
        //IUnitOfWork _unitOfWork { get; set; }
        [Inject] IRFDStarterExtension _starterExtension { get; set; }

        [Inject]
        RFDStateManagement _stateManagement { get; set; }
        [Inject]
        public HubConnection _hubConnection { get; set; }
        [Inject]
        NavigationManager _navigationManager { get; set; }

        bool IsHubConnection => _hubConnection?.State == HubConnectionState.Connected;

        InsiderParamerters _paramerters { get; set; } = new InsiderParamerters();
        MetaData _metaData { get; set; } = new MetaData();
        MetronicAlert _alert = new MetronicAlert();
        bool _updateModal = false;
        bool _addModal =false;
        bool _transection = false;

        List<InsiderModel> _insiders = new List<InsiderModel>();


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



            if (!IsHubConnection)
                await _hubConnection.StartAsync();
            _hubConnection.On<string, string>("ReceiveInsider", async (flag, result) =>
              {
                  var insider = JsonConvert.DeserializeObject<InsiderModel>(result);
                  var phish = _insiders?.FirstOrDefault(x => x.Id == insider.Id);
                  if (phish != null)
                  {
                      phish.Transections = insider.Transections;
                      phish.Status = insider.Status;
                      await InvokeAsync(StateHasChanged);
                  }
              });
        }
        public async Task GetAllAsync()
        {
            var result = await _insiderHttpHelper.GetAllAsync(_paramerters);

            if (result.IsSucceeded)
            {
                _insiders = result?.Data?.Items;
                _metaData = result?.Data?.MetaData;
            }
            else
            {
                _insiders = new List<InsiderModel>();
            }

        }
        private void SelectedPage(int page)
        {
            _paramerters.PageNumber = page;
            _stateManagement.Notify(this, "insider");
        }
        private void Search()
        {
            _stateManagement.Notify(this, "insider");
        }

        public async Task Submit()
        {
            var result = await _insiderHttpHelper.AddAsync(_addModel);

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
            _stateManagement.Notify(this, "insider");
            Indicator = false;
            _addModal = false;
        }
        public void SetUpdateModel(Insider model)
        {
            _updateModel = model;
            _updateModal = true;
        }
        public async Task Update()
        {
            var result = await _insiderHttpHelper.UpdateAsync(_updateModel);

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
            _stateManagement.Notify(this, "insider");
            Indicator = false;

            _updateModal = false;

        }
        public async Task Delete(Insider insider)
        {
            var result = await _insiderHttpHelper.DeleteAsync(insider.Id);

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
                    break;
            }
            await InvokeAsync(StateHasChanged);
        }
        public void CheckAll()
        {
            if (!_check)
                _insiders.ForEach(x => { x.IsChecked = true; });
            else
                _insiders.ForEach(x => { x.IsChecked = false; });
        }
        public async Task Start(List<InsiderModel> insiders)
        {

            IsBusy = true;
            List<Task> tasks = new List<Task>();
            List<string> errors = new List<string>();
            foreach (var item in insiders)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var result = await _insiderHttpHelper.StartCampaign(item);
                    if (!result.IsSucceeded)
                    {
                        _alert.Type = AlertType.Error;
                        _alert.Message = result.Message;
                        _alert.IsVisible = true;
                    }
                }));

            }
            await Task.WhenAll(tasks);
            IsBusy = false;
        }
        public async Task PauseCampaignAsync(InsiderModel phishing)
        {

            var result = await _insiderHttpHelper.PauseCampaign(phishing.Id);
            if (!result.IsSucceeded)
            {
                _alert.Type = AlertType.Error;
                _alert.Message = result.Message;
                _alert.IsVisible = true;
            }

        }
        public void ViewTransection(InsiderModel insider)
        {
            _insiders.ForEach(x => x.IsSelect = false);
            _insiders.FirstOrDefault(x => x.Id == insider.Id).IsSelect = true;
            _transection = true;
        }
        public async Task Duplicate(Insider insider)
        {
            var result = await _insiderHttpHelper.Duplicate(insider);
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
            _stateManagement.Notify(this, "insider");
        }
        public async Task SelectedDelete()
        {
            var postData = _insiders.Where(x => x.IsChecked).Select(x => new Insider { Id = x.Id }).ToList();
            var result = await _insiderHttpHelper.SelectedDelete(postData);
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
