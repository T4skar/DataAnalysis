<?php

$servername = "localhost";
$username = "xaviercb12";
$password = "8QGQefMvS38H";
$dbname = "xaviercb12";

$conn;

if ($_SERVER["REQUEST_METHOD"] == "POST") {
   CreatePlayer();
   
} else {
   echo "Users PHP: Método no permitido \n";
}

function CreatePlayer() {

   // Acceder a los datos enviados desde Unity
   $playerName = $_POST["playerName"];
   $playerAge = $_POST["playerAge"];
   $playerGender = $_POST["playerGender"];
   $playerCountry = $_POST["playerCountry"];
   $signUpTime = $_POST["signUpTime"];

   global $conn;

   if(ConnectToServer() == false){
       return;
   }

   $sql = "INSERT INTO Users ( User_Name, User_Age, User_Gender, User_Country, Sign_Up_Time) VALUES ('$playerName',$playerAge,'$playerGender','$playerCountry','$signUpTime')";

   //if (mysqli_query($conn, $sql)) {
   if ($conn->query($sql) === TRUE) 
   {
       echo $conn->insert_id;
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
       die("Users PHP: Conexión fallida: " . $conn->connect_error);
   }

   return true;
}

function CloseConnection() 
{
   global $conn;
   $conn->close();
}

?>