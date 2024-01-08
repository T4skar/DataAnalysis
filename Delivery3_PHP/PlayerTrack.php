<?php

$servername = "localhost";
$username = "xaviercb12";
$password = "8QGQefMvS38H";
$dbname = "xaviercb12";

$conn;

if ($_SERVER["REQUEST_METHOD"] == "POST") {
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

   if(ConnectToServer() == false)
   {
       return;
   }

    // Comprobar si es una session nueva o no
    if($start == "True")
    {
        $sql = "INSERT INTO Sessions ( Start_Timestamp, User_id) VALUES ('$timeStamp','$userId')";

            if ($conn->query($sql) === TRUE) {
                echo $conn->insert_id;
            } 
            else 
            {
                echo "Sessions PHP: Error al insertar datos: " . mysqli_error($conn);
            }
    }
    else
    {

        if($sessionId != -1)
        {
            // Si no es una session nueva busca en la tabla la session q tiene la misma id

            $sql = "UPDATE Sessions SET End_Timestamp = '$timeStamp' WHERE Session_Id = $sessionId";

            if ($conn->query($sql) === TRUE) 
            {
                echo $sessionId;
            } 
            else 
            {
                echo "Error al actualizar el registro: " . $conn->error;
            }
        
        }
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