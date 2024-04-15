using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VM.Core;
using VM.PlatformSDKCS;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ImageSourceModuleCs;
using IMVSFastFeatureMatchModuCs;
using GlobalVariableModuleCs;


namespace WindowsFormsApp1
{
    public partial class WayMove : Form
    {
        private string txt_Path = "";
        static string currentFilePath = Directory.GetCurrentDirectory();
        string[] solFiles = Directory.GetFiles(currentFilePath, "*.sol", SearchOption.TopDirectoryOnly);

        public WayMove()
        {
            InitializeComponent();
            Bitmap picSource = new Bitmap(currentFilePath + "\\model_Pic\\logo_bg.jpg");
            pictureBox1.Image = picSource;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            My_Combobox();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }
        //选择路径

        private void button1_Click(object sender, EventArgs e)
        {
            // 如果找到.sol文件，则显示它们的路径
            Thread thread1 = new Thread(Vmload);
            
            if (solFiles.Length > 0)
            {
                thread1.Start();
                
            }
            else
            {
                ShowMsg("Can't find any File!");
            }

        }
        private void Vmload()
        {
            VmSolution.Load(string.Join("/", solFiles));
            VmSolution.Instance.SyncRun();
            VmSolution.Instance.ContinuousRunEnable = false;
            ShowMsg(string.Join("",solFiles));
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //设为全局变量用于通讯连接和接收数据
        Socket socketsend;
        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //创建用于通讯的socket
                socketsend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint IPEPoint = new IPEndPoint(IPAddress.Parse("192.168.1.115"), Convert.ToInt32(59151));
                ShowMsg(Convert.ToString(IPEPoint));
                //连接对应的端口
                await socketsend.ConnectAsync(IPEPoint);
                ShowMsg(socketsend.RemoteEndPoint + ": 连接成功!");
            }
            catch { }
        }
        public void SetText(string SText)
        {
            richTextBox3.Text = richTextBox3.Text + SText + Environment.NewLine;
            richTextBox3.Select(richTextBox3.TextLength, 0);
            richTextBox3.ScrollToCaret();
        }
        public void Receive()
        {
            while (true)
            {
                // try
                //{
                byte[] buffer = new byte[1024 * 1024 * 2];
                int i = socketsend.Receive(buffer);

                //接收到的数据长度为0时表示连接断开，跳出循环
                if (i == 0)
                {
                    break;
                }
                string str = Encoding.UTF8.GetString(buffer, 0, i);
                ShowMsg(socketsend.RemoteEndPoint + ":" + str);
                // }
                // catch { }
            }
        }
        public void My_Combobox()
        {
            comboBox1.Items.Add("Model3");
            comboBox1.Items.Add("Model5");
            comboBox1.Items.Add("Model11");
            comboBox1.Items.Add("personal");
            comboBox1.Items.Add("Model_1");
            comboBox1.Items.Add("Model_2");
            comboBox1.Items.Add("Model_3");
            comboBox1.Items.Add("Model_4");
            comboBox1.Items.Add("Model_5");
            comboBox1.Items.Add("Model_6");
            comboBox1.Items.Add("Model_7");

        }
        //委托
        public void ShowMsg(string str)
        {
            if (richTextBox3.InvokeRequired)
            {
                Action SetText111 = delegate { SetText(str); };
                richTextBox3.Invoke(SetText111);
            }
            else
            {
                richTextBox3.AppendText(str + "\r\n");
            }

            // richTextBox3.AppendText(str + "\r\n");
        }


        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button6_Click(object sender, EventArgs e)
        {
            string filePath = Directory.GetCurrentDirectory() + '\\' + txt_Path;
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }
            foreach (string line in lines)
            {
                Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(line);
                await socketsend.SendAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                ShowMsg(line);
                await Task.Delay(130);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string currentFilePath = Directory.GetCurrentDirectory();
            Bitmap Pic_Model_3 = new Bitmap(currentFilePath + "\\model_Pic\\Model_3.png");
            Bitmap Pic_Model_5 = new Bitmap(currentFilePath + "\\model_Pic\\Model_5.png");
            Bitmap Pic_Model_11 = new Bitmap(currentFilePath + "\\model_Pic\\Model_11.png");
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Model3":
                    pictureBox1.Image = Pic_Model_3;
                    txt_Path = "Model3.txt";
                    break;
                case "Model5":
                    pictureBox1.Image = Pic_Model_5;
                    txt_Path = "Model5.txt";
                    break;
                case "Model11":
                    pictureBox1.Image = Pic_Model_11;
                    txt_Path = "Model11.txt";
                    break;
                case "personal":
                    pictureBox1.Image = Pic_Model_11;
                    txt_Path = "personal.txt";
                    break;
                case "Model_1":
                    pictureBox1.Image = Pic_Model_11;
                    txt_Path = "Model_1.txt";
                    break;
                case "Model_2":
                    pictureBox1.Image = Pic_Model_11;
                    txt_Path = "Model_2.txt";
                    break;
                case "Model_3":
                    pictureBox1.Image = Pic_Model_11;
                    txt_Path = "Model_3.txt";
                    break;
                case "Model_4":
                    pictureBox1.Image = Pic_Model_11;
                    txt_Path = "Model_4.txt";
                    break;
                case "Model_5":
                    pictureBox1.Image = Pic_Model_11;
                    txt_Path = "Model_5.txt";
                    break;
                case "Model_6":
                    pictureBox1.Image = Pic_Model_11;
                    txt_Path = "Model_6.txt";
                    break;
                case "Model_7":
                    pictureBox1.Image = Pic_Model_11;
                    txt_Path = "Model_7.txt";
                    break;
            }
        }

        private void Find_button_Click(object sender, EventArgs e)
        {

            Thread thread2 = new Thread(scratchBall);
            thread2.Start();
            //if (exposure.Length>=5)
            //{
            //  
            //}
        }
        private async void scratchBall()
        {
            while (true)
            {
                var findBall = (IMVSFastFeatureMatchModuTool)VmSolution.Instance["流程1.快速匹配5"];
                GlobalVariableModuleTool globalVar = (GlobalVariableModuleTool)VmSolution.Instance["全局变量1"];
                string exposure = globalVar.GetGlobalVar("e");
                string q = globalVar.GetGlobalVar("q");
                int qInt = (int)float.Parse(q);
                float exposureFloat = float.Parse(exposure);
                int number = findBall.ModuResult.MatchNum; 
                if (exposureFloat >= 6000 && number == 4 - qInt)
                {
                    await Task.Delay(300);
                    var findBall1 = (IMVSFastFeatureMatchModuTool)VmSolution.Instance["流程1.快速匹配5"];
                    ShowMsg($"检测数量{number}");
                    var point = findBall1.ModuResult.MatchPoint;
                    await Task.Delay(100);
                    for (int i = 0; i < point.Count; i++)
                    {
                    ShowMsg(point.Count.ToString());
                    string str_2 = $"i_{point[i].X.ToString()}_j_{point[i].Y.ToString()}_k_3_theta_0_classwood_11_writeover_1_startwood_1_";
                    Byte[] buffer_send = System.Text.Encoding.UTF8.GetBytes(str_2);
                    await socketsend.SendAsync(new ArraySegment<byte>(buffer_send), SocketFlags.None);
                    ShowMsg(str_2);
                    await Task.Delay(150);
                }
                    await Task.Delay(200);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalVariableModuleTool globalVar = (GlobalVariableModuleTool)VmSolution.Instance["全局变量1"];

            globalVar.SetGlobalVar("p", "1");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string str_1 = "i_1_j_1_k_3_theta_0_classwood_11_writeover_1_startwood_1_";
            Byte[] buffer_send = System.Text.Encoding.UTF8.GetBytes(str_1);
            await socketsend.SendAsync(new ArraySegment<byte>(buffer_send), SocketFlags.None);

        }
    }
}
