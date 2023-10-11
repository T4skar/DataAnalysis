<?php
/**
 * Export to PHP Array plugin for PHPMyAdmin
 * @version 5.0.4deb2
 */

/**
 * Database `xaviercb12`
 */

/* `xaviercb12`.`Sessions` */
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Acceder a los datos enviados desde Unity
    $playerName = $_POST["playerName"];
    $playerAge = $_POST["playerAge"];
    $playerGender = $_POST["playerGender"];
    $playerCountry = $_POST["playerCountry"];
    $signUpTime = $_POST["signUpTime"];

    // Realizar acciones con los datos, por ejemplo, guardarlos en una base de datos
    // o realizar algún otro procesamiento

    // Enviar una respuesta de vuelta a Unity
    echo "Bombardeen Lideraje";
} else {
    echo "Método no permitido";
}
