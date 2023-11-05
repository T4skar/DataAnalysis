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
   $purchaseId = $_POST["purchaseId"];
   $userId = $_POST["userId"];
   $timeStamp = $_POST["timeStamp"];

   global $conn;

   if(ConnectToServer() == false){
       return;
   }

   // Habria q hacer algo con Items.xlsx para mirar los precios y tal

   $sql = ""; //= "INSERT INTO Sessions ( Start_Timestamp, User_id) VALUES ('$timeStamp','$userId')";

    if (mysqli_query($conn, $sql)) {
        echo $conn->insert_id;
    } else {
        echo "Sessions PHP: Error al insertar datos: " . mysqli_error($conn);
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