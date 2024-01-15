<?php

$servername = "localhost";
$username = "xavierlm9";
$password = "zMpmB6Drtvc2";
$dbname = "xavierlm9";

$conn;

if ($_SERVER["REQUEST_METHOD"] == "POST") {

    if(ConnectToServer() == false)
    {
        return;
    }

    $methodToCall = $_POST["methodToCall"];

    if($methodToCall == "Set Info")
    {
        UpdateData();
    }
    else if($methodToCall == "Get Info")
    {
        GetInfo();
    }
    

    CloseConnection();
} 
else 
{
   echo "PHP: Método no permitido \n";
}

function UpdateData() {

   // Acceder a los datos enviados desde Unity
   $timeStamp = $_POST["timeStamp"];
   $posX = $_POST["posX"];
   $posY = $_POST["posY"];
   $posZ = $_POST["posZ"];
   $isThrowing = $_POST["isThrowing"];

    global $conn;

    $sql = "INSERT INTO PlayerGetsDamage (Timestamp, PosX, PosY, PosZ, DamageCause) VALUES ('$timeStamp',$posX, $posY, $posZ,'$isThrowing')";    

    if ($conn->query($sql) === TRUE) 
    {
        echo "Data Sent";
    } 
    else 
    {
        echo "PHP: Error al insertar datos: " . mysqli_error($conn);
    }

  
}

function GetInfo() {

   // Acceder a los datos enviados desde Unity
   $timeStamp = $_POST["timeStamp"];
   $posX = $_POST["posX"];
   $posY = $_POST["posY"];
   $posZ = $_POST["posZ"];
   $isThrowing = $_POST["isThrowing"];

    global $conn;

    //$sql = "INSERT INTO PlayerGetsDamage (Timestamp, PosX, PosY, PosZ, DamageCause) VALUES ('$timeStamp',$posX, $posY, $posZ,'$isThrowing')";    

    $sql = "SELECT * FROM PlayerGetsDamage";
    $result = $conn->query($sql);
    
    // Verificar si hay resultados en la consulta
    if ($result->num_rows > 0) 
    {
        // Crear un array para almacenar los resultados
        $rows = array();

        // Iterar sobre los resultados y almacenarlos en el array
        while ($row = $result->fetch_assoc()) {
            $rows[] = $row;
        }

        // Convertir el array a formato JSON y mostrarlo
        echo json_encode($rows);
    } 
    else 
    {
    // Mostrar un mensaje si no hay resultados
    echo "0 resultados";
    }

    if ($conn->query($sql) === TRUE) 
    {
        //echo "Data Sent";
    } 
    else 
    {
        echo "PHP: Error al insertar datos: " . mysqli_error($conn);
    }

  
}

function ConnectToServer() 
{
   global $servername, $username, $password, $dbname;
   global $conn;
   
   $conn = new mysqli($servername, $username, $password, $dbname);

   if ($conn->connect_error) {
       die("PHP: Conexión fallida: " . $conn->connect_error);
   }

   return true;
}

function CloseConnection() 
{
   global $conn;
   $conn->close();
}

?>