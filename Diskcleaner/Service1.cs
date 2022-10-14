using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Diskcleaner
{
    public partial class Service1 : ServiceBase
    {
        Timer Timer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer = new Timer(2000)
            {
                AutoReset = true
            };
            Timer.Elapsed += new ElapsedEventHandler(timer_elapsed);
            Timer.Start();
        }
        private void timer_elapsed(object sender,ElapsedEventArgs args)
        {
            writeLog();
        }
        protected override void OnStop()
        {
            Timer.Stop();
            Timer = null;
        }
        public void Deletfiles()
        {
          Directory.GetFiles("D:tempfiles").Select(f => new FileInfo(f))
         .Where(f => f.CreationTime < DateTime.Now.AddMonths(-3))
         .ToList()
         .ForEach(f => f.Delete());
        }
        private void writeLog()
        {
            //string path = @"c:\lokeshTesting\Logger" + System.DateTime.Now.ToShortTimeString() + ".txt";
            //if (!File.Exists(path))
            //{
            //    using (StreamWriter sw = File.CreateText(path))
            //    {
            //        sw.WriteLine($"exe running on :{DateTime.Now}");
            //    }
            //}
            StreamWriter writer = null;
            writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt",true);
            writer.WriteLine($"exe running on :{DateTime.Now}");
            writer.Flush();
            writer.Close();
        }
    }
}
