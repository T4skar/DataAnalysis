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

$debugMessages = "";

 function ConnectToServer() 
{
    global $debugMessages;

    global $servername, $username, $password, $dbname;
    global $conn;
    
    $conn = new mysqli($servername, $username, $password, $dbname);

    if ($conn->connect_error) {
        $debugMessages .= "Conexión fallida: " . $conn->connect_error;
        die($debugMessages);
    }

    $debugMessages .= "Connection done correctly\n";
    echo $debugMessages;

    return true;
}

function CloseConnection() 
{
    global $conn;
    $conn->close();
}

/* `xaviercb12`.`Sessions` */
if ($_SERVER["REQUEST_METHOD"] == "POST") {

    global $debugMessages;
    
    $debugMessages = "PHP: ";

    $methodToCall = $_POST["methodToCall"];

    switch($methodToCall){
        case "CreatePlayer":
            CreatePlayer();
            break;

        case "GetPlayerID":
            GetPlayerID();
            break;
    }

    // Realizar acciones con los datos, por ejemplo, guardarlos en una base de datos
    // o realizar algún otro procesamiento

    // Enviar una respuesta de vuelta a Unity
    
} else {
    $debugMessages .= "Método no permitido \n";
    echo $debugMessages;
}

function CreatePlayer() {
    // Acceder a los datos enviados desde Unity
    $playerName = $_POST["playerName"];
    $playerAge = $_POST["playerAge"];
    $playerGender = $_POST["playerGender"];
    $playerCountry = $_POST["playerCountry"];
    $signUpTime = $_POST["signUpTime"];

    global $conn;
    global $debugMessages;

    if(ConnectToServer() == false){
        return;
    }

    $sql = "INSERT INTO Users ( User_Name, User_Age, User_Gender, User_Country, Sign_Up_Time) VALUES ('$playerName',$playerAge,'$playerGender','$playerCountry','$signUpTime')";

    if (mysqli_query($conn, $sql)) {
        $debugMessages .= "Datos insertados con éxito. User ID: " . $conn->insert_id . "\n";
        echo $debugMessages;
    } else {
        $debugMessages .= "Error al insertar datos: " . mysqli_error($conn) + "\n";
        echo $debugMessages;
    }

    CloseConnection();
}

function GetPlayerID() {

    global $conn;

    if(!ConnectToServer()){
        return;
    }
    
    $sql = "SELECT * FROM Users WHERE User_Id = (SELECT MAX(User_Id) FROM Users)";
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
