<?php

$servername = "localhost";
$username = "xavierlm9";
$password = "zMpmB6Drtvc2";
$dbname = "xavierlm9";

$conn;

if ($_SERVER["REQUEST_METHOD"] == "POST") {
   UpdateData();
} else {
   echo "Sessions PHP: Método no permitido \n";
}

function UpdateData() {

   // Acceder a los datos enviados desde Unity
   $timeStamp = $_POST["timeStamp"];
   $posX = $_POST["posX"];
   $posY = $_POST["posY"];
   $posZ = $_POST["posZ"];
   $isThrowing = $_POST["isThrowing"];

   global $conn;

   if(ConnectToServer() == false)
   {
       return;
   }

    $sql = "INSERT INTO PlayerGetsDamage (Timestamp, PosX, PosY, PosZ, DamageCause) VALUES ('$timeStamp',$posX, $posY, $posZ,'$isThrowing')";    

    if ($conn->query($sql) === TRUE) 
    {
        echo "Fino señores";
    } else {
        echo "Users PHP: Error al insertar datos: " . mysqli_error($conn);
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