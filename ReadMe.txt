https://lucid.app/lucidchart/1e5d0416-2e7a-4ded-b6cd-90ea2b3b9019/edit?viewport_loc=2583%2C1452%2C1997%2C916%2C0_0&invitationId=inv_667ac75b-bdb2-4d96-8c47-51668ab9097b
# MY ECOMMERCE
Crear la api como la de pókemonApi En este caso es un mini ejemplo de un ecommerce
- En este caso no hace falta hacer las tablas JOIN porque no tenermos relacions de many to many
- hacer prubas con swagger 
-Hacer validaciones en los dto

- diseñar y creac el frontend

**TODO:**
 (aviso: en el SSMS me dice que CategroyId para la tabla productos no es un nombre valido(pero lo pilla xd)
 Creo que es por la migracion (havia categroy en vez de categroy "ya he hecho la migacion y update pero se ha quedado este error")
-------mirar las passwords
-------mirar auth users..   (algo de los tokens xd)

[[[-HACER LOS SERVICES HAY UNO DE EJEMPLO Y DEBERÍAN DE SER TASK AWAITEDS CREO!]]]
[[[-Y HACER UNA NUEVA MIGRATION YA QUE HE MODIFICADO EL MODEL USER!]]]

-hacer frontend










Quizases:
Crear quizas el getter by name de category? or product?






________________________________________README DE POKEAPI__________________________________________________________________

https://www.youtube.com/watch?v=EmV_IBYIlyo&list=PL82C6-O4XrHdiS10BLh23x71ve9mQCln0&index=5
https://drive.google.com/file/d/1EbYYjY7ubkpVKgBVE3Dloa9tr-oqo58g/view
________________________________________tutorial api aspnet + diagram pokemon_______________________
https://www.youtube.com/watch?v=NYpOaPC6jrg
________________explicacion de [fromquery]/[frombody etc]_________

----------------->EN LA CLASS POKEMON HAY EXPLICACIONES EN EL POKEOMON todos los files.<-----------------

Models,clases normals amb les seves relacions de 1 to 1 or 1 to many
Y si es Many to Many creas la clase Join que es la de Many to Many y 
Ejemplo:PokemnOwner.cs and PokemonCategory.cs
Dentro la clase de Pokemon pongo(como atributos) ambas y dentro de la clase
Owner Pongo PokemonOwner (como atributo"Sino mejor ver las clases")
En la clase Category pongo la clase Join de PokemonCategory(as atribute)

setps:::::::::::::::::::::::::::::::::::::::::

·CREO LOS MODELS, EL DATACONTEXT..

·DB: Creo una DB con el SSMS...
1-Clickderecho en databsases dentro del servidor SSMS y Crear nueva database
(el paso 2 no hace falta pq ya estoy conectado a este servidor)
2-Accedemos al ssms copiamos el nombre del servidor(DESKTOP-14D02GT\SQLEXPRESS01)
y en Explorador de ServerObject en VisualStudio creamos un nuevo server con este mismo nombre y nos conectamos
a la db creada anteriormente. (en mi caso ya estoy conectado al server por otros proyectos)
3-Vamos a las propiedades dela database desde SQLVisualstudio y la cedana de la db la copiamos y lo ponemos
en el conecctionsetting (defaultconnedction) del apsettings.json.
4-configuramos en el program.cs que se conecte al defaultconnection(que hemos establecido)
5-Usamos la consola nugget:
//Comandos para inicializar la Migracion una vez se ha creado o modificado las clases y por lo tanto
        //Se habran modificado las tablas: Comando:::
        //EntityFrameworkCore\Add-Migration (Migration'sName)
        //Después de esto el comando para actualizar la Database es:
        //EntityFrameworkCore\Update-DataBase
(paso3)Ejemplo cadena conexion a la db:
Accedo a las properties de la DB creada y copio la cadena de conexion.(Data Source=DESKTOP-14D02GT\SQLEXPRESS01;Initial Catalog=pokemonreview;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False)
En el appsettings.json Hemos puesto la cadena de conexion.

RECORDAR: instalar el EntityFrameworkCore, SQLServer, Design, Tools.  +Automapper, quizas
tambien el automapper dependency injeccion.

·CREO LAS INTERFACES, RESPOSITORY, DTO, HELPER..


PROBLEMAS:
-OWNER POST ((ya no es problema))
 Para crearlo no debo tocar el id porque se crea solo, y debo poner el idCountry antes
 pq se necesita poner de qué Country es el nuevo Owner.

-Poner validaciones, por ejemplo si el pokemon post, si lo intento crear con un ownerid no 
existente o un categoyId no existente no deja i rebienta al save();