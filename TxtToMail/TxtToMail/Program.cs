using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mail;
using System.Threading;

namespace TxtToMail
{
	class Program
	{
		static void Main(string[] args)
		{

			String directoryName = @"C:\01-sendt";
			//String convertert = @"C:\02-conv\";
			DirectoryInfo dirInfo = new DirectoryInfo(directoryName);
			if (dirInfo.Exists == false)
				Directory.CreateDirectory(directoryName);

			List<String> FileList = Directory
				   .GetFiles(@"C:\Prosessing", "*.*", SearchOption.AllDirectories).ToList();

			foreach (string file in FileList)
			{
				FileInfo mFile = new FileInfo(file);


				// to remove name collisions
				if (new FileInfo(dirInfo + "\\" + mFile.Name).Exists == false)
				{
					String filnavn = mFile.Name;
					String subj = filnavn.Remove(filnavn.Length - 4);

					//
					string Array som leser FileNotFoundException getnxtline
					//
					//String body = File.ReadAllText(file);
					//Console.WriteLine(subj + ": " + body);

					//mail start
					//mail
					MailMessage message = new MailMessage();
					message.From = new MailAddress("mymail@my-mail.com");
					message.To.Add(new MailAddress("reciver@my-mail.com"));
					message.Subject = subj;
					message.SubjectEncoding = System.Text.Encoding.UTF8;

					message.Body = body;
					message.BodyEncoding = System.Text.Encoding.UTF8;
					message.IsBodyHtml = true;
					//mail slutt

					//mail head

					SmtpClient client = new SmtpClient("Smtp.localhost.foe");
					client.Port = 587;
					client.Credentials = new System.Net.NetworkCredential(@"un", "pw");
					//client.UseDefaultCredentials = true;
					client.DeliveryMethod = SmtpDeliveryMethod.Network;
					//client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
					//client.PickupDirectoryLocation = convertert;

					//client.EnableSsl = true;
					//mail head slutt

					try
					{
						client.Send(message);
						//flytte fil for ?olde oversikt over behandlede filer
						mFile.MoveTo(dirInfo + "\\" + mFile.Name);
					}
					catch (Exception ex)
					{
						Console.WriteLine("Feilmelding: Mail er ikke sendt!", ex.ToString());


					}
					Thread.Sleep(500);
					//mail stopp 
				}
			}
			Console.ReadLine();
		}
	}
}