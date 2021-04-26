using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace WindowsServiceRunExe
{
    public partial class ServiceRunExe : ServiceBase
    {
        public ServiceRunExe()
        {
            InitializeComponent();
        }

        Timer tmr = new Timer();
        Process myProcess = Process.Start(@"C:\Deneme\metin.exe");
        string filePath = "C:\\Deneme\\log.txt";

        protected override void OnStart(string[] args)
        {
            File.AppendAllText(filePath, "Basladim -" + DateTime.Now.ToString() + "\r\n");
            tmr.Elapsed += new ElapsedEventHandler(t_Elapsed);
            tmr.Interval = 10000;
            tmr.Start();
        }


        private void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!myProcess.HasExited)
            {
                File.AppendAllText(filePath, "if Exe Durduruldu -" + DateTime.Now.ToString() + "\r\n");
                myProcess.Kill();

            }
            else
            {
                File.AppendAllText(filePath, "else Exe Tekrar Başlatıldı -" + DateTime.Now.ToString() + "\r\n");
                myProcess.Start();
            }
        }

        protected override void OnStop()
        {
            File.AppendAllText(filePath, "Durdu -" + DateTime.Now.ToString() + "\r\n");
            tmr.Enabled = false;
        }
        protected override void OnPause()
        {
            File.AppendAllText(filePath, "Bekletildi -" + DateTime.Now.ToString() + "\r\n");
            tmr.Enabled = false;
        }

        protected override void OnContinue()
        {
            File.AppendAllText(filePath, "Devam Ettirildi -" + DateTime.Now.ToString() + "\r\n");
            tmr.Enabled = true;
        }

        protected override void OnShutdown()
        {
            File.AppendAllText(filePath, "Kapandi -" + DateTime.Now.ToString() + "\r\n");
            tmr.Enabled = false;
        }
    }
}
