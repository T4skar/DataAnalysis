<?php

// Lo que habia antes en Sessions ahora está en Users ya que era todo el rollo de crear un user

$servername = "localhost";
$username = "xaviercb12";
$password = "8QGQefMvS38H";
$dbname = "xaviercb12";

$conn;

if ($_SERVER["REQUEST_METHOD"] == "POST") {

   global $debugMessages;
   
   $debugMessages = "";

   UpdateSession();
   
} else {
   echo "Sessions PHP: Método no permitido \n";
}

function UpdateSession() {

   // Acceder a los datos enviados desde Unity
   $sessionId = $_POST["sessionId"];
   $start = $_POST["start"];
   $userId = $_POST["userId"];
   $timeStamp = $_POST["timeStamp"];

   global $conn;

   if(ConnectToServer() == false){
       return;
   }

   // Comprobar si es una session nueva o no
   if($start == "True"){

    $sql = "INSERT INTO Sessions ( Start_Timestamp, User_id) VALUES ('$timeStamp','$userId')";

    if (mysqli_query($conn, $sql)) {
        echo $conn->insert_id;

        // Checkear la tabla
        $result = $conn->query($sql);
    } else {
        echo "Sessions PHP: Error al insertar datos: " . mysqli_error($conn);
    }

   }
   else{

    // Si no es una session nueva busca en la tabla la session q tiene la misma id

    $StartDate = "SELECT Date FROM Sessions WHERE Session_Id = $sessionId";
    $result = $conn->query($StartDate);
    
    while ($row = mysqli_fetch_array($result)) $startDate = $row[0];

    // Calcula la diferencia de tiempo entre la session cuando fué creada y ahora

    $getDuration = "SELECT TIMESTAMPDIFF(SECOND, $startDate, $timeStamp)";
    $result = $conn->query($getDuration);

    $duration = mysqli_fetch_array($result)[0];
    $sql = "UPDATE Sessions SET Duration = $duration";

    $result = $conn->query($sql);
   }

   CloseConnection();
}

function ConnectToServer() 
{
   global $servername, $username, $password, $dbname;
   global $conn;
   
   $conn = new mysqli($servername, $username, $password, $dbname);

   if ($conn->connect_error) {
       die("Sessions PHP: Conexión fallida: " . $conn->connect_error);
   }

   return true;
}

function CloseConnection() 
{
   global $conn;
   $conn->close();
}

?>