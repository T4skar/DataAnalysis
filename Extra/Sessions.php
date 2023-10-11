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

    echo "Bombardeen Lideraje\n";

    $servername = "localhost";
    $username = "xaviercb12";
    $password = "8QGQefMvS38H";
    $dbname = "xaviercb12";

    $conn = new mysqli($servername, $username, $password, $dbname);

    if ($conn->connect_error) {
        die("Conexión fallida: " . $conn->connect_error);
        echo "Connection failed";
        return;
    }

    echo "Connection done correctly";
    

    $sql = "INSERT INTO Users ( `User_Name`, `User_Age`, `User_Gender`, `User_Country`, `Sign_Up_Time`) VALUES ("lolo",23,"F","Spain",'1998-01-23 12:45:56')";

    $conn->close();
    // Realizar acciones con los datos, por ejemplo, guardarlos en una base de datos
    // o realizar algún otro procesamiento

    // Enviar una respuesta de vuelta a Unity
    
} else {
    echo "Método no permitido";
}
