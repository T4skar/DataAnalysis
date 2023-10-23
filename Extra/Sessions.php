<?php
/**
 * Export to PHP Array plugin for PHPMyAdmin
 * @version 5.0.4deb2
 */

/**
 * Database `xaviercb12`
 */

 $servername = "localhost";
 $username = "xaviercb12";
 $password = "8QGQefMvS38H";
 $dbname = "xaviercb12";

 $conn;

function ConnectToServer() 
{
    global $servername, $username, $password, $dbname;
    global $conn;
    
    $conn = new mysqli($servername, $username, $password, $dbname);

    if ($conn->connect_error) {
        die("Conexión fallida: " . $conn->connect_error);
        echo "Connection failed";
        return false;
    }

    echo "Connection done correctly\n";

    return true;
}

function CloseConnection() 
{
    global $conn;
    $conn->close();
}

/* `xaviercb12`.`Sessions` */
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Acceder a los datos enviados desde Unity
    $playerName = $_POST["playerName"];
    $playerAge = $_POST["playerAge"];
    $playerGender = $_POST["playerGender"];
    $playerCountry = $_POST["playerCountry"];
    $signUpTime = $_POST["signUpTime"];

    if(ConnectToServer() == false){
        return;
    }

    $sql = "INSERT INTO Users ( User_Name, User_Age, User_Gender, User_Country, Sign_Up_Time) VALUES ('$playerName',$playerAge,'$playerGender','$playerCountry','$signUpTime')";

    if (mysqli_query($conn, $sql)) {
        echo "Datos insertados con éxito";
    } else {
        echo "Error al insertar datos: " . mysqli_error($conn);
    }

    CloseConnection();

    // Realizar acciones con los datos, por ejemplo, guardarlos en una base de datos
    // o realizar algún otro procesamiento

    // Enviar una respuesta de vuelta a Unity
    
} else {
    echo "Método no permitido";
}

function GetPlayerID() {

    if(!ConnectToServer()){
        return;
    }
    
    $sql = "SELECT columna_deseada FROM nombre_de_la_tabla WHERE condición";
    $result = $conn->query($sql);

    if ($result->num_rows > 0) {
        $row = $result->fetch_assoc();
        $valor = $row["columna_deseada"];
        echo json_encode(array("valor" => $valor));
    } else {
        echo json_encode(array("error" => "No se encontraron resultados"));
    }

    CloseConnection();
}
