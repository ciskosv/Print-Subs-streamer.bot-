# Imprime Subs para streamer.bot
Imprime, crea un imagen y posteala en Discord, para streamer.bot

Ha sido probado en Windows, soporta impresores térmicos (tickets.

# Setup

En caso de ser necesario, deberás agregar la referencia al archivo System.Net.Http.dll

- Doble click en "Execute Code (Print(ES))"

![image](https://user-images.githubusercontent.com/494355/172022437-0538e020-560b-4ba3-b1f6-19f0165aa503.png)

- Selecciona "References tab" y luego click derecho y selecciona "Add reference from file".

![image](https://user-images.githubusercontent.com/494355/172022479-48b88eed-0b82-462e-8a95-e4c1080d41b3.png)

- Busca el archivo "System.Net.Http.dll, seleccionalo y dar click en "Abrir".

![image](https://user-images.githubusercontent.com/494355/172022569-4222b8f2-a071-4e99-b79f-c4a6b44cab64.png)

- Click en "Compilar" para confirmar que no hayan errores en la ficha "Compiling Log".

![image](https://user-images.githubusercontent.com/494355/172022609-fdc16bc2-6f39-4506-843b-5bccee35cd1f.png)

- Doble click en la sub-acción "Set argument %paperWidth% to '314'" y escribe el ancho de tu papel, en céntimas de pulgada.  ej. 314 para 3.14 pulgadas.

![image](https://user-images.githubusercontent.com/494355/172022691-e9adea4d-5be0-423c-9fce-d63dd4e6aaea.png)

- Doble click en la sub-acción "Set argument %paperHeight% to '0'" " y escribe la altura de tu papel, en céntimas de pulgada.  Dejalo en "0" si tienes una impresora térmica, para calcular la altura automáticamente dependiendo del contenido.

![image](https://user-images.githubusercontent.com/494355/172022736-225012a7-6e5c-4035-961b-9ccb9ab838c6.png)

- Doble click en la sub-acción "Set argument %printer% to 'Microsoft Print to PDF'" y escribe el nombre de tu impresora, tal cual se muestra en tu configuración de Windows.

![image](https://user-images.githubusercontent.com/494355/172023638-64a2d09e-700c-44a5-ac63-8b187fc97c07.png)
![image](https://user-images.githubusercontent.com/494355/172022351-f02fdd2b-5678-4e4b-acef-4889a6afe11b.png)

- Doble click en la sub-acción "Set argument %postToDiscord% to 'False'" y escribe 'True' si quieres que se postee la imagen en tu canal de Discord.

![image](https://user-images.githubusercontent.com/494355/172023747-12662d28-0479-4923-940a-fb5a51a1d75c.png)

- Si elegiste la opción para postear en Discord, entonces has doble click en la sub-acción "Set argument %discordWebhook% to'none' y escribe la URL del webhook de tu canal de Discord.

![image](https://user-images.githubusercontent.com/494355/172023803-470a3c58-7928-4956-9e2d-6447703d402a.png)

Y listo, ya puedes asignar la acción a los eventos Subs de tu streamer.bot o agregarlo como una sub-acción a tu acción actual!
