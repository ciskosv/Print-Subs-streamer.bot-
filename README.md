# Print Subs for streamer.bot
Print, create an image and post it on Discord, for streamer.bot

It has been tested on Windows, supports thermal printers (tickets).

In case of needed, you will have to add the reference System.Net.Http:

- Double click on Execute Code (Print(EN))

![image](https://user-images.githubusercontent.com/494355/172022437-0538e020-560b-4ba3-b1f6-19f0165aa503.png)

- Select "References tab", then right click and "Add reference from file".

![image](https://user-images.githubusercontent.com/494355/172022479-48b88eed-0b82-462e-8a95-e4c1080d41b3.png)

- Look for the file "System.Net.Http.dll, select it and click on "Open".

![image](https://user-images.githubusercontent.com/494355/172022569-4222b8f2-a071-4e99-b79f-c4a6b44cab64.png)

- Click on "Compile" to confirm that there are no errors in the tab "Compiling Log".

![image](https://user-images.githubusercontent.com/494355/172022609-fdc16bc2-6f39-4506-843b-5bccee35cd1f.png)

- Double click on "Set argument %paperWidth% to '314'" sub-action and set your paper's width in hundreths of inches.  i.e. 314 for 3.14 inches.

![image](https://user-images.githubusercontent.com/494355/172022691-e9adea4d-5be0-423c-9fce-d63dd4e6aaea.png)

- Double click on "Set argument %paperHeight% to '0'" sub-action and set your paper's width in hundreths of inches.  Leave it on "0" if you have a thermal printer in order to calculate the height automatically based on content.

![image](https://user-images.githubusercontent.com/494355/172022736-225012a7-6e5c-4035-961b-9ccb9ab838c6.png)

- Double click on "Set argument %printer% to 'Microsoft Print to PDF'" and set your printer's name as shown on window's configuration.

![image](https://user-images.githubusercontent.com/494355/172023638-64a2d09e-700c-44a5-ac63-8b187fc97c07.png)
![image](https://user-images.githubusercontent.com/494355/172022351-f02fdd2b-5678-4e4b-acef-4889a6afe11b.png)

- Double click on "Set argument %postToDiscord% to 'False'" and set it to 'True' if you want to post the image on your Discord channel.

![image](https://user-images.githubusercontent.com/494355/172023747-12662d28-0479-4923-940a-fb5a51a1d75c.png)

- If you set the option to post on Discord to True, then double click on "Set argument %discordWebhook% to'none' and set your Discord's channel's webhook URL.

![image](https://user-images.githubusercontent.com/494355/172023803-470a3c58-7928-4956-9e2d-6447703d402a.png)


