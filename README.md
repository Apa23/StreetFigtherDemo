# StreetFigtherChipiado

## Desarrollado por: 
1. Andrés Felipe Aparicio Mestre
2. Angello Gomez
3. Natalia Patiño

## Descripción del proyecto:
El presente reositorio cuenta con el código fuente de un demo de un videojuego de lucha 2D para 2 jugadores. Los jugadores deberan seleccionar 1 personaje cada uno de entre los 4 personajes disponibles, y buscar agotar la barra de vida del contrincante, el primero que lo consiga será el ganador. 

## Información técnica:
1. Versión de Unity: v2021.3.20f1
2. Paquetes extras utilizados: Unity Input System
3. Hadware: Para el jugador 1 es posible jugar con un mando de Play o teclado. El jugador dos juega exclusivamente con teclado

### Key maping:
                                            Jugador 1                       Jugador 2
     Desplazamiento:                Flecha izquierda/derecha                Tecla A/D
     Saltar:                          Botón 2/Flecha arriba                  Tecla W
     Atacar:                        Botón 3/Barra espaciadora                Tecla J
     Aceptar:                       Botón 3/Barra espaciadora                Tecla J
     Cancelar:                        Botón 2/Tecla Escape                   Tecla K

## Movimientos especiales:
Para ambos jugadores existen dos dinámicas especiales.
1. Dashing: Un desplazamiento rápido hacia adelante o hacia atrás que se realiza cuando se presiona seguida y rapidamente dos veces una de las teclas de desplazamiento. Dos veces adelante -> Dash hacia adelante / Dos veces atras -> Dash hacia atras
2. Ataque en combo: Despues de realizar un ataque es posible en cadenar otro seguido si se presiona nuevamente el botón de ataque en el preciso momento en que se conecta el golpe. Estos ataques encadenados realizan más daño pero de no coordinarse bien harán que el personaje quede bloqueado por un breve momento, haciendolo vulnerable.
