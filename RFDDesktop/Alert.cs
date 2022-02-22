using RFDDesktop.Infrastructer.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFDDesktop
{
    public partial class Alert : Form
    {
        public static Timer _timer;
        public RFDHttpHelper _httpHelper = new RFDHttpHelper();
        public Alert()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            pictureBox1.Parent = pictureBox2;
            lbl_Count.Parent = pictureBox2;
            lbl_closeTimer.Parent = pictureBox2;
            label1.Parent = pictureBox2;
            label2.Parent = pictureBox2;
            label3.Parent = pictureBox2;
            label4.Parent = pictureBox2;
            label5.Parent = pictureBox2;
            label6.Parent = pictureBox2;
            label7.Parent = pictureBox2;
            label8.Parent = pictureBox2;
            label9.Parent = pictureBox2;
            label10.Parent = pictureBox2;
            pictureBox3.Parent = pictureBox2;
            pictureBox4.Parent = pictureBox2;
            pictureBox5.Parent = pictureBox2;
            pictureBox6.Parent = pictureBox2;

        }
        private void Alert_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Alert_FormClosing);
            int counter = 386;
            Random random = new Random();
            _timer = new Timer();
            _timer.Interval = 50;
            _timer.Start();
            _timer.Tick += (s, _) =>
            {
                Task.Run(async () =>
                       {
                           try
                           {
                               counter++;
                               await Task.Delay(random.Next(50, 2000));
                               if (counter < 7500)
                               {
                                   if (lbl_Count.InvokeRequired)
                                       SetControlThreadSafe(lbl_Count, args =>
                                       {
                                           lbl_Count.Text = counter.ToString();
                                       }, null);
                                   else
                                       lbl_Count.Text = counter.ToString();
                               }
                           }
                           catch 
                           {
                               _timer.Stop();
                           }
                       });
            };
        }
        private void Alert_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Stop();
            Environment.Exit(1);
        }
        public void SetControlThreadSafe(Control control, Action<object[]> action, object[] args)
        {
            if (control.InvokeRequired)
                try { control.BeginInvoke(new Action<Control, Action<object[]>, object[]>(SetControlThreadSafe), control, action, args); } catch { }
            else action(args);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            lbl_closeTimer.Visible = true; 
          
            Task.Run(async () =>
            {
                int count = 3;
                bool isTrue = true;
                                 Task.Run(async () => { await _httpHelper.DidTrueClosedAsync();});  
                while (isTrue)
                {
                    if (lbl_closeTimer.InvokeRequired)
                        SetControlThreadSafe(lbl_closeTimer, args =>
                        {
                            lbl_closeTimer.Text = $"kapanmasına {count} saniye kaldı";
                        }, null);
                    else
                        lbl_closeTimer.Text = $"kapanmasına {count} saniye kaldı";
                    await Task.Delay(1000);
                    count--;
                    if (count == default(int))
                        isTrue = false;
                }
                _timer.Stop();
                Environment.Exit(1);
            });         
        }
    }
}
