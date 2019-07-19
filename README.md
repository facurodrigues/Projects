# Ejemplo de Azure Functions

Azure Function, Http Trigger, con Docker.

El ejemplo es un http trigger, utiliza un wrapper de Clash Royale, del cual obtengo la información (Cartas, Arenas, Cofres), en este caso hice un ejemplo sencillo, por query string obtengo el nombre de la carta, por ejemplo "fireball", "arrows", "bomber", para luego obtener toda la información de dicha carta. Luego, serializo a un json y lo retorno al action result.

Uso

http://192.168.99.100:8080/api/Function1?name=[CardName]
