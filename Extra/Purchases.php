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

   UpdatePurchase();
   
} else {
   echo "Purchases PHP: Método no permitido \n";
}

function UpdatePurchase() {

   // Acceder a los datos enviados desde Unity
   $itemId = $_POST["itemId"];
   $userId = $_POST["userId"];
   $timeStamp = $_POST["timeStamp"];

   global $conn;

   if(ConnectToServer() == false){
       return;
   }

   // Recibes itemId de unity
   // itemid mirar el precio en la tabla de Items
   // ese precio enviarselo a purchases sumandoselo al spend money

   $sql = "SELECT Price FROM Items WHERE Item_Id = $itemId";
   $result = $conn->query($sql);
   if ($result->num_rows > 0) {
      // Si se encontró un resultado, obten el valor de precio
      $row = $result->fetch_assoc();
      $price = $row["Price"];
      echo $price;
   } else {
      echo "No se encontraron resultados para la ID $itemId.";
   }


   $sql = "INSERT INTO Purchases ( User_Id, Spent_Money, Timestamp) VALUES ('$userId', '$price' ,'$timeStamp')";

      if ($conn->query($sql) === TRUE) 
      {
        echo $conn->insert_id;
      } else {
        echo "Purchases PHP: Error al insertar datos: " . mysqli_error($conn);
    }

   CloseConnection();
}

function ConnectToServer() 
{
   global $servername, $username, $password, $dbname;
   global $conn;
   
   $conn = new mysqli($servername, $username, $password, $dbname);

   if ($conn->connect_error) {
       die("Purchases PHP: Conexión fallida: " . $conn->connect_error);
   }

   return true;
}

function CloseConnection() 
{
   global $conn;
   $conn->close();
}

?>