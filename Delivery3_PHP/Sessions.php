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

    if($methodToCall == "Set Session")
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
    $timeStamp = $_POST["timeStamp"];
    $user = $_POST["user"];
    $starting = $_POST["starting"];
    $session = $_POST["session"];

    global $conn;

    // Fechas de inicio y fin de la session
    if ($starting == "True")
    {
        $sql = "INSERT INTO Sessions (User_Id, Start) VALUES ('$user', '$timeStamp')"; 
    }
    else
    {
        $sql = "UPDATE Sessions SET End = '$timeStamp' WHERE Session_Id = $session";
    }

    // Tratar de obtener la session id (autoincremental) de SQL
    if ($conn->query($sql) === TRUE) 
    {
        echo $conn->insert_id;
    } 
    else 
    {
        echo "PHP: Error al insertar datos: " . mysqli_error($conn);
    }
}

function GetInfo() {

    global $conn;

    $sql = "SELECT * FROM PlayerGetsDamage";
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