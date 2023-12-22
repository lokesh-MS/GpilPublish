#region File Header
/*
--------------------------------------
TeamLiftss IT Systems & solutions pvt. ltd.
Copyright (c) 2021, All rights reserved

Author      : ANANDARAJ G 


Revision History:
Rev   Date                   Who                    Description
1.0   28/July/2021          Anandaraj G            Intial version.
--------------------------------------
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Configuration;

namespace GPI
{
    public class Utils
    {
        //testing
        public enum LogEntry
        {
            VERBOSE = 0,
            DEBUG = 1,
            TRACE = 2,
            CRITICAL = 3,
            EXCEPTION = 4
        }

        public Utils()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void LogError(string errorMessage, System.Web.HttpRequest req)
        {
            LogError(errorMessage, req, LogEntry.EXCEPTION);
        }

        public static void LogError(string message, System.Web.HttpRequest req, LogEntry entry)
        {
            try
            {
                if (string.IsNullOrEmpty(message)) return;
                string ipAddress = null;
                if (req != null)
                {
                    string ip = req.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (!string.IsNullOrEmpty(ip))
                    {
                        if (!string.IsNullOrEmpty(ip))
                        {
                            string[] ipRange = ip.Split(',');
                            string trueIP = ipRange[0];
                            ipAddress = trueIP;
                        }
                    }
                    else
                    {
                        ip = req.ServerVariables["REMOTE_ADDR"];
                        ipAddress = ip;
                    }
                }
                else ipAddress = " Could not retrieve the address ";

                string path = "~/Error/" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }
                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    string err = null;
                    if (entry == LogEntry.EXCEPTION)
                    {
                        w.WriteLine("\r\nLog Entry : ");
                        //w.WriteLine("{0}", DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        try
                        {
                            w.WriteLine("{0}", DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));

                        }
                        catch (Exception ex)
                        {
                            w.WriteLine(" >>1<< " + ex.Message);
                            w.WriteLine("{0}", DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                        }

                        try
                        {
                            err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                                          ". Error Message:" + message;
                        }
                        catch (Exception ex)
                        {
                            w.WriteLine(" >>2<< " + ex.Message);
                            err = "Error Message:" + message;
                        }
                        w.WriteLine(err);
                        w.Flush();

                        err = "\r\n Accessed from : " + ipAddress;
                        err += "\r\n User Agent : " + req.UserAgent;

                        w.WriteLine(err);
                        w.Flush();
                    }
                    else
                    {
                        //get webconfig value. If logging value is greater than entry then write
                        int errorLevel = Convert.ToInt32(ConfigurationManager.AppSettings["ErrorLevel"]);
                        if (errorLevel <= (int)entry)
                        {
                            err = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                            err += " Message : " + message;
                            err += Environment.NewLine + "Accessed from : " + ipAddress;
                            err += Environment.NewLine + "User Agent : " + req.UserAgent;
                            w.WriteLine("###" + err + "###");
                        }
                    }
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                // LogError(ex.Message);
            }
        }

        internal static void LogError(string v, object request)
        {
           // throw new NotImplementedException();
        }

        public static string CheckString(string strValue)
        {
            strValue = strValue.Replace("'", "''");
            return strValue;
        }
        public static DateTime ParseDate(string strDate)
        {
            DateTime dt = DateTime.Parse(strDate, System.Globalization.CultureInfo.CurrentCulture);
            return dt;
        }
        public static string GetDateString(string strDate)
        {
            //string strTempDate = Convert.ToString(date);
            string[] strDate1 = strDate.Split('/', ' ', ':');
            if (Convert.ToInt32(strDate1[0]) <= 9)
                strDate1[0] = "0" + strDate1[0];
            if (Convert.ToInt32(strDate1[1]) <= 9)
                strDate1[1] = "0" + strDate1[1];
            strDate = strDate1[1] + '/' + strDate1[0] + '/' + strDate1[2];
            return strDate;
        }


        public static string EncryptPassword(string password)
        {
            //    string strPlainText = password;
            //    byte[] hashedDataBytes;
            //    UTF8Encoding encoder = new UTF8Encoding();
            //    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            //    hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(strPlainText));
            //    StringBuilder sb = new StringBuilder();
            //    for (int i = 0; i < hashedDataBytes.Length; i++)
            //    {
            //        sb.Append(hashedDataBytes.GetValue(i).ToString());
            //    }
            //    return sb.ToString();

            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }

        }
        public static string EncryptPasswordold(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public static string DecryptPassword(string Message, string Passphrase)
        {
            //byte[] Results;
            //System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            //MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            //byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));
            //TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            //TDESAlgorithm.Key = TDESKey;
            //TDESAlgorithm.Mode = CipherMode.ECB; TDESAlgorithm.Padding = PaddingMode.PKCS7;
            //byte[] DataToDecrypt = Convert.FromBase64String(Message);
            //try
            //{
            //    ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
            //    Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            //}
            //finally
            //{
            //    TDESAlgorithm.Clear();
            //    HashProvider.Clear();
            //}
            //return UTF8.GetString(Results);


            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(Passphrase);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        public static string EncryptPassword1(string password)
        {
            string strPlainText = password;
            byte[] hashedDataBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(strPlainText));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashedDataBytes.Length; i++)
            {
                sb.Append(hashedDataBytes.GetValue(i).ToString());
            }
            return sb.ToString();
        }


        public static void SendEmail(string BatchNo, string result)
        {
            try
            {
                //  SmtpClient SmtpMail = new SmtpClient();
                //  MailMessage mail = new MailMessage();
                //  System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                //  string strFromAddress = ConfigurationSettings.AppSettings["FromEmailAddress"].ToString();
                //  string strpassword = ConfigurationSettings.AppSettings["FromEmailPassword"].ToString();
                ////  mail.From = new System.Net.Mail.MailAddress(strFromAddress, strpassword);
                //  mail.CC.Add("awantikashukla03@gmail.com");
                //  mail.To.Add("Premkishore.Makesh@itc.in");
                //  mail.Subject = ConfigurationSettings.AppSettings["SubjectNewPassword"].ToString();
                //  //string toEmail = email;
                //  MailAddress fromaddress = new MailAddress(strFromAddress);
                //  // mail.To.Add(email);
                //  mail.From = fromaddress;
                //  mail.Body = ConfigurationSettings.AppSettings["BodyResetPassword"] + result;
                //  SmtpMail.Host = ConfigurationSettings.AppSettings["SMTPServerName"];
                //  SmtpMail.Credentials = new System.Net.NetworkCredential(strFromAddress, strpassword);
                //  SmtpMail.UseDefaultCredentials = false;



                //string strFromAddress = ConfigurationSettings.AppSettings["FromEmailAddress"].ToString();
                //string strToAddress = ConfigurationSettings.AppSettings["ToEmail"].ToString();
                
                //string to = strToAddress;//To address    
                //string from = strFromAddress;
                //string host = ConfigurationSettings.AppSettings["SMTPServerName"].ToString();


                //MailMessage message = new MailMessage(from, to);
                //string mailbody = ConfigurationSettings.AppSettings["BodyResetPassword"] + result;
                //message.Subject = ConfigurationSettings.AppSettings["SubjectNewPassword"].ToString();
                //message.Body = mailbody;
                //message.BodyEncoding = Encoding.UTF8;
                //message.IsBodyHtml = true;
                //SmtpClient client = new SmtpClient(host, 587); //Gmail smtp    
                //System.Net.NetworkCredential basicCredential1 = new
                //System.Net.NetworkCredential("anandarajg1981@gmail.com", "prithiv143");
                //client.EnableSsl = true;
                //client.UseDefaultCredentials = false;
                //client.Credentials = basicCredential1;
                //try
                //{
                //    client.Send(message);
                //}

                //catch (Exception ex)
                //{
                //    throw ex;
                //}



            }
            catch (SmtpException ex)
            {
                throw ex;
            }

        }


    }
}
