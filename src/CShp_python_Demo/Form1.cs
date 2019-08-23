using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CShp_python_Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            runPythonP();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //按钮点击时发送 输入框的指令给 main.py 进程
            pythonP.StandardInput.WriteLine(textBox2.Text);
        }

         string fileName = "main.py";
         Process pythonP;

         void runPythonP()
        {

            pythonP = new Process();
            string scriptPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + fileName;// 获得python文件的绝对路径（为了方便我将文件放在c#的debug文件夹中）
            pythonP.StartInfo.FileName = "python";//没有配环境变量的话，可以写"xx\xx\python.exe"的绝对路径。如果配了，直接写"python"即可
            pythonP.StartInfo.Arguments = scriptPath;
            pythonP.StartInfo.UseShellExecute = false;
            pythonP.StartInfo.RedirectStandardOutput = true;
            pythonP.StartInfo.RedirectStandardInput = true;
            pythonP.StartInfo.RedirectStandardError = true;

            //后台模式设置
            pythonP.StartInfo.CreateNoWindow = true;
            pythonP.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //启动进程
            pythonP.Start();
            pythonP.BeginOutputReadLine();
            pythonP.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            Console.ReadLine();
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            var printedStr = e.Data;
            Action at = new Action(delegate ()
            {
                //接受main.py 进程打印的字符信息到文本显示框
                richTextBox1.AppendText(printedStr + "\n");
            });
            Invoke(at);
        }
    }
}
