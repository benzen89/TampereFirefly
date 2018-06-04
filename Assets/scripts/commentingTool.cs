using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class commentingTool : MonoBehaviour {

	/*
	Make sure Player settings/Other settings / Api compatability level is set to .Net 2.0 not .Net 2.0 Subset
	On the Gmail account, make sure "Access for less secure apps" has been turned on
	*/

	public GameObject commentBtn;
	public GameObject commentingObjs;
	public GameObject textInputObj;

	public void StartCommenting(){
		commentBtn.SetActive(false);
		commentingObjs.SetActive(true);
		ScreenCapture.CaptureScreenshot(Application.persistentDataPath+ "/screenshot.png");
		Debug.Log(Application.persistentDataPath);
	}

	public void CancelCommenting(){
		commentBtn.SetActive(true);
		commentingObjs.SetActive(false);
		Debug.Log(textInputObj.GetComponent<Text>().text);
	}

	public void SendEmailWithScreenshot(string myComment) {
		MailMessage mail = new MailMessage();
		mail.From = new MailAddress("tampere3dbot@gmail.com");
		mail.To.Add("tampere3dbot@gmail.com");
        mail.Subject = "Kommentti";
        mail.Body = textInputObj.GetComponent<Text>().text; 							//"This is for testing SMTP mail from GMAIL"
        string filename = "screenshot.png"; 			//"screenshot.jpg"
        mail.Attachments.Add (new Attachment(Application.persistentDataPath+ "/"+ filename));

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("tampere3dbot@gmail.com", "tiilisilakkasavurauta") as ICredentialsByHost;

        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = 
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
                { return true; };
        smtpServer.Send(mail);
        Debug.Log("success");
	}
}
