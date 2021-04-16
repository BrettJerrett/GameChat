using GameChat.ChatLib;
using GameChat.LogLib;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GameChat
{
    public partial class UserControl1: UserControl
    {
        //ncat -l -p 5555 -t -v
        Client networkingBase = new Client();
        Thread workerThread;
        Logger logger = new Logger();

        //String variable to hold User Message
        string userMessage;


        //Setting byte arrays
        Byte[] msg = new Byte[256];
        Byte[] msgOut;

        int i;

        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void networkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //<summary>
        //Start the client when Connect is clicked, enable the button for client input
        //Create a thread, then start it, then confirm connection via richTextBox and logger
        //</summary>
        private void connectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                networkingBase.Start();
                button1.Enabled = true;

                workerThread = new Thread(serverListen);
                workerThread.Name = "Worker Thread";
                workerThread.Start();

                richTextBox1.Text += "Client has connected! \n";
                logger.Log(DateTime.Now + ": Server has Conncted \n");
                logger.Log(DateTime.Now + ": Client has Connected \n");
            }
            catch (Exception error)
            {
                Console.WriteLine("Error - " + error.Message);
            }
        }

        //<summary>
        //When disconnected is clicked, disconnect the client, 
        //make the button disabled, log then add the message to the richTextBox
        //</summary>
        private void disconnectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                networkingBase.Disconnect();
                button1.Enabled = false;
                logger.Log(DateTime.Now + ": Client has disconnected! \n");
                richTextBox1.Text += "Client has disconnected! \n";
            }
            catch (Exception error)
            {
                Console.WriteLine("Error - " + error.Message);
            }
        }

        //<summary>
        //When button is clicked, add message to the richTextBox, log the message from client
        //then add Client's message to the NetworkStream and clear the textbox.
        //</summary>
        private void button1_Click_1(object sender, EventArgs e)
        {
            byte[] msgOut = System.Text.Encoding.ASCII.GetBytes(textBox1.Text + "\n");
            richTextBox1.Text += ">>" + textBox1.Text + "\n";
            logger.Log(DateTime.Now + ": Client: " + textBox1.Text + "\n");
            networkingBase.writeStream(msgOut);

            textBox1.Text = "";
        }

        //<summary>
        //Log that the server has disconnected, close the client,
        //then exit the application
        //</summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Log(DateTime.Now + ": Server has disconnected! \n");
            //close the thread
            networkingBase.Disconnect();
            Application.Exit();
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        //<summary>
        //Method that will be used in Thread, that checks if there is data in the NetworkStream
        //Creates a method invoker, then proceeds to grab the message from server and adds it to the
        //richTextBox and the log.
        private void serverListen()
        {
            while (true)
            {
                try
                {
                    while (networkingBase.CheckData())
                    {

                        MethodInvoker theInvoker = new MethodInvoker(delegate ()
                        {
                            i = networkingBase.readBytes(msg);
                            userMessage = Encoding.ASCII.GetString(msg, 0, i);
                            richTextBox1.Text += userMessage + "\n";
                            logger.Log(DateTime.Now + ": Server: " + userMessage + "\n");

                        });

                        richTextBox1.Invoke(theInvoker);
                        return;
                    }
                }
                catch (Exception)
                {
                    richTextBox1.Text += "Server Disconnected";
                    logger.Log(DateTime.Now + ": Server has disconnected. \n");
                    button1.Enabled = false;
                }
               
            }
        }
    }
}
