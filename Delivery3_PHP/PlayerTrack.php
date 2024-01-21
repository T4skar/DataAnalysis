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

    $user = $_POST["user"];
    $timeStamp = $_POST["timeStamp"];
    $posX = $_POST["posX"];
    $posY = $_POST["posY"];
    $posZ = $_POST["posZ"];
    $rotX = $_POST["rotX"];
    $rotY = $_POST["rotY"];
    $rotZ = $_POST["rotZ"];

    global $conn;

    $sql = "INSERT INTO PlayerTrack (user_id, timestamp, posX, posY, posZ, rotX, rotY, rotZ) VALUES ($user, '$timeStamp',$posX, $posY, $posZ, $rotX, $rotY, $rotZ)";    

    if ($conn->query($sql) === TRUE) 
    {
        echo "Tracking Data Sent";
    } 
    else 
    {
        echo "PHP: Error al insertar datos: " . mysqli_error($conn);
    }
}

function GetInfo() {

    global $conn;

    $sql = "SELECT * FROM PlayerTrack";
    $result = $conn->query($sql);
    
    // Verificar si hay resultados en la consulta
    if ($result->num_rows > 0) 
    {
        while ($row = $result->fetch_assoc()) 
        {
            // Imprimir cada fila como JSON
            echo json_encode($row) . "\n";
        }
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
        //echo "PHP: Error al insertar datos: " . mysqli_error($conn);
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