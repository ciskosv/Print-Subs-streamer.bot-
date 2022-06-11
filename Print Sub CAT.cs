using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Net;
using System.Threading;
using System.Drawing.Drawing2D;

//for discord option
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

public class Globals
{
	public static String ts = DateTime.Now.ToString("yyyyMMddHHmmssfff");
	public static String myTempFile = Path.Combine(Path.GetTempPath(), "lastSub" + ts + ".jpg");
}

public class CPHInline
{
	private Font printFont;

	public bool Execute()
	{
		String timestamp = DateTime.Now.ToString();
		String subtype = args["__source"].ToString(); 
		String msg = "";
		String title ="";
		String user = args["user"].ToString();
		String tier = args["tier"].ToString();
		String[] line = new String[10]; //10 lines are going to be used in total
		System.Drawing.Font[] pfont = new System.Drawing.Font[10]; //
		String total="";
		String cumulative="";
		String monthStreak="";
		String profilepic = args["targetUserProfileImageUrl"].ToString(); 
		int extra =0;
		int cut = 0;

		//DOWNLOADING PROFILEPIC
          WebRequest   webreq = WebRequest.Create(profilepic);
          WebResponse  webres = webreq.GetResponse();
          Stream       stream = webres.GetResponseStream();
          Image photo = Image.FromStream(stream);
          stream.Close();

		//Customizing for each type of sub
		//Traducció gràcies a Jordi Rossell
		switch(subtype) 
		{
		  case "TwitchSub":
			title = "SUBSCRIPCIÓ";
			photo = resizeImage(photo, new Size(200, 200));
			extra = 40;
			cut = 0;
			break;
		  case "TwitchReSub":
			title = "RE-SUBSCRIPCIÓ";
			//Check for plurals
			if(Convert.ToInt32(args["cumulative"])>1)
			{
				cumulative = args["cumulative"].ToString() + " mesos";
			}else{
				cumulative = args["cumulative"].ToString() + " mes";
			}
			if(Convert.ToInt32(args["monthStreak"])>1)
			{
				monthStreak = " - " + args["monthStreak"].ToString() + " mesos seguits";
			}
			line[5] = "Subscrit per " + cumulative  + monthStreak;
			photo = resizeImage(photo, new Size(200, 200));
			extra = 40;
			cut = 0;
			break;
		  case "TwitchGiftSub":
			title = "SUBSCRIPCIÓ REGALADA";
				if((int)args["totalSubsGifted"]>0){ 
					total = " (Total de: " + args["totalSubsGifted"].ToString() + ")";
				}
			line[3] = "REGALADOR" + total;
			line[5] = "DESTINATARI";
			line[6] = args["recipientUser"].ToString();
			if(Convert.ToInt32(args["monthsGifted"])>1){
				line[7] = args["monthsGifted"].ToString() + " Mesos";
			}else{
				line[7] = args["monthsGifted"].ToString() + " Mes";
			}
			
			photo = resizeImage(photo, new Size(145, 145));
			extra = 25;
			cut = 30;
			break;
		  case "TwitchGiftBomb":
			title = "PLUJA DE SUBSCRIPCIONS";
				if((int)args["totalGifts"]>0){ 
					total = " (Total de: " + args["totalGifts"].ToString()+ ")";
				}
			line[3] = "REGALADOR" + total;

			if(Convert.ToInt32(args["gifts"])>1){
				line[5] = "Pluja de " + args["gifts"].ToString() + " subscripcions";
			}else{
				line[5] = "Pluja de " + args["gifts"].ToString() + " subscripció";
			}

			photo = resizeImage(photo, new Size(165, 165));
			extra = 40;
			cut = 20;
			break;
		}

		line[0] = title;
		line[1] = timestamp;
		line[2] = "profilepic";
		line[4] = user;

		//Comentar la siguiente linea si no se quiere traducir
		tier = tier.Replace("tier", "Nivell");

		line[8] = tier;

		if (args.ContainsKey("rawInput")){
			line[9] = args["rawInput"].ToString();
		}

		//Customizing font, size and fontstyle for each line
		for (int i = 0; i < 10; i++){
			if(line[i]==null){continue;}
			//Font specifications for each line
			//We can set the Font, size and style
			if(i==0){ pfont[i] = new Font("Arial", 16,FontStyle.Bold);} 
			if(i==1){ pfont[i] = new Font("Arial", 7);}
			if(i==3){ pfont[i] = new Font("Arial", 12,FontStyle.Bold);}
			if(i==4){ pfont[i] = new Font("Arial", 20,FontStyle.Bold);}
			if(i==5){ pfont[i] = new Font("Arial", 12);}
			if(i==6){ pfont[i] = new Font("Arial", 12,FontStyle.Bold);}
			if(i==7){ pfont[i] = new Font("Arial", 12);}
			if(i==8){ pfont[i] = new Font("Arial", 12);}
			if(i==9){ pfont[i] = new Font("Arial", 6);}
		}

		////////////CREATING IMAGE/////////////
		float linesPerPage = 0;
		float leftMargin = 0;
		float topMargin = 0;
		float pageWidth = Convert.ToSingle(args["paperWidth"]);
		float pageHeight =  Convert.ToSingle(args["paperHeight"]);
		float yPos = topMargin;
		int linespace = 2; //space between lines

		//////
		const int dotsPerInch = 135;    // define the quality in DPI
		double widthInInch = (double)pageWidth/100;   // width of the bitmap in INCH
		double heightInInch = (double)11;  // height of the bitmap in INCH - We use 11 inches just to have a number with plenty room
		

		////CALCULATE FINAL HEIGHT
		using (Bitmap bitmapTmp = new Bitmap((int)(widthInInch * dotsPerInch), (int)(heightInInch * dotsPerInch)))
		{
			bitmapTmp.SetResolution(dotsPerInch, dotsPerInch);
			using (Graphics graphicsTmp = Graphics.FromImage(bitmapTmp))
			{
				graphicsTmp.Clear(Color.White);

				pageHeight = 0; //adding some bottom margin
				////CALCULATING FINAL PAGE HEIGHT NEEDED
				for (int i = 0; i < 10; i++){
					if(line[i]==null){continue;}
					if(i==0){ printFont = pfont[i];} 
					if(i==1){ printFont = pfont[i];}
					if(i==3){ printFont = pfont[i];}
					if(i==4){ printFont = pfont[i];}
					if(i==5){ printFont = pfont[i];}
					if(i==6){ printFont = pfont[i];}
					if(i==7){ printFont = pfont[i];}
					if(i==8){ printFont = pfont[i];}
					if(i==9){ printFont = pfont[i];}
					if(line[i]=="profilepic"){			
						pageHeight = pageHeight + linespace*extra + photo.Height;
					}else{
						float stringWidth = graphicsTmp.MeasureString(line[i], printFont).Width+20; //adding +20 to give some margin
						int stringLines = (int)Math.Ceiling(stringWidth / pageWidth );
						pageHeight = pageHeight + (linespace) + printFont.GetHeight(graphicsTmp)*stringLines;
					}
				}
			}
		}

		//Now pageHeight has the real height needed
		pageHeight = (float)(pageHeight/1.35)-cut;
		heightInInch = (double)pageHeight/100;

		//CREATING FINAL IMAGE
		using (Bitmap bitmap = new Bitmap((int)(widthInInch * dotsPerInch), (int)(heightInInch * dotsPerInch)))
		{
			bitmap.SetResolution(dotsPerInch, dotsPerInch);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.Clear(Color.White);

				for (int i = 0; i < 10; i++){
					if(line[i]==null){continue;}
					if(i==0){ printFont = pfont[i];} 
					if(i==1){ printFont = pfont[i];}
					if(i==3){ printFont = pfont[i];}
					if(i==4){ printFont = pfont[i];}
					if(i==5){ printFont = pfont[i];}
					if(i==6){ printFont = pfont[i];}
					if(i==7){ printFont = pfont[i];}
					if(i==8){ printFont = pfont[i];}
					if(i==9){ printFont = pfont[i];}
					if(line[i]=="profilepic"){
						int proPicX = ((int)((pageWidth*1.35) - photo.Width)/2)-35; //fix of -20 in X position

						Point ulCorner = new Point(proPicX, (int)yPos);

						// Sending to image
						graphics.DrawImage(photo,ulCorner); 
						yPos = yPos + linespace*extra + photo.Height;

					}else{
						float stringWidth = graphics.MeasureString(line[i], printFont).Width+20; //adding +20 to give some margin
						int stringLines = (int)Math.Ceiling(stringWidth / (pageWidth*1.35) );
							
						// Sending to image
						float ancho = Convert.ToSingle(pageWidth*1.35)-20;
						graphics.DrawString(line[i], printFont, Brushes.Black, new RectangleF(10, yPos, ancho, printFont.GetHeight(graphics)*stringLines), new StringFormat() { Alignment = StringAlignment.Center });
						
						yPos = yPos + (linespace) + printFont.GetHeight(graphics)*stringLines;
					}	
				}
			}
			// Save the bitmap
			bitmap.Save(Globals.myTempFile);
		}

		//POSTING TO DISCORD
		if(args["postToDiscord"].ToString().ToLower()=="true"){

			string discordWebhookURL = args["discordWebhook"].ToString();
			string FileName = Globals.myTempFile;
				
			//Taken from StreamUP Tools - OBSCord by Andilippi
			string displayName = args["user"].ToString();
			string userName = args["userName"].ToString();
			string userProfilePicture = args["targetUserProfileImageUrl"].ToString();
			string discordMessage = JsonConvert.SerializeObject(new {embeds = new[] { new { title = "Nova subscripció!", color = 7009535, author = new { name = $"~ {displayName}", url = $"https://twitch.tv/{userName}", icon_url = userProfilePicture } } } });
			// Open and Read the created file by discord and make it into a BLOB
			FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(fs);
			byte[] imageBlob = reader.ReadBytes((int)fs.Length);
			
			// Close the open file
			fs.Close();
			reader.Close();

			// Creating a HttpClient so that we can send the data to discord and upload it.
			HttpClient client = new HttpClient();

			// Set the correct type of data to send and put in the discordMessage also
			MultipartFormDataContent formData = new MultipartFormDataContent();
			formData.Add(new ByteArrayContent(imageBlob, 0, imageBlob.Length), "image", $"{FileName}");		
			formData.Add(new StringContent(discordMessage), "payload_json");
			
			// Send it and after it is done dispose of client.
			client.PostAsync(discordWebhookURL, formData).Wait();
			client.Dispose();
		}

		//PRINT
		if(args["print"].ToString().ToLower()=="true"){
			pageHeight = pageHeight + 30;
			if(Convert.ToInt32(args["paperHeight"])>0)
			{
				pageHeight = Convert.ToInt32(args["paperHeight"]);
			}
  
			PrintDocument pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
			pd.PrinterSettings.PrinterName = args["printer"].ToString();
			pd.DefaultPageSettings.PaperSize = new PaperSize("SubsPaper", Convert.ToInt32(pageWidth)+6, Convert.ToInt32(pageHeight)+6);
			pd.Print();
			Thread.Sleep(2000);
		}

		File.Delete(Globals.myTempFile);

		return true;
	}


	private void pd_PrintPage(object sender, PrintPageEventArgs ev)
    {
		using (System.Drawing.Image photo = System.Drawing.Image.FromFile(Globals.myTempFile))
		{
			Point ulCorner = new Point(-11,0);
			//Sending to printer
			ev.Graphics.DrawImage(photo, ulCorner); 
		}
	}

	//TO RESIZE PICTURES
	//use:
	//photo = resizeImage(photo, new Size(200, 200));
	private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)  
	{  
		//Get the image current width  
		int sourceWidth = imgToResize.Width;  
		//Get the image current height  
		int sourceHeight = imgToResize.Height;  
		float nPercent = 0;  
		float nPercentW = 0;  
		float nPercentH = 0;  
		//Calulate  width with new desired size  
		nPercentW = ((float)size.Width / (float)sourceWidth);  
		//Calculate height with new desired size  
		nPercentH = ((float)size.Height / (float)sourceHeight);  
		if (nPercentH < nPercentW)  
			nPercent = nPercentH;  
		else  
		 nPercent = nPercentW;  
		 //New Width  
		 int destWidth = (int)(sourceWidth * nPercent);  
		 //New Height  
		 int destHeight = (int)(sourceHeight * nPercent);  
		 Bitmap b = new Bitmap(destWidth, destHeight);  
		 Graphics g = Graphics.FromImage((System.Drawing.Image)b);  
		 g.InterpolationMode = InterpolationMode.HighQualityBicubic;  
		 // Draw image with new width and height  
		 g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);  
		 g.Dispose();
		 return (System.Drawing.Image)b;  
	}
}
