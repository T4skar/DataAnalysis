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
    $entity = $_POST["entity"];
    $entityId = $_POST["entityId"];
    $timeStamp = $_POST["timeStamp"];
    $posX = $_POST["posX"];
    $posY = $_POST["posY"];
    $posZ = $_POST["posZ"];
    $deathCause = $_POST["deathCause"];

    global $conn;

    if($entity === 'Ellen'){
        $sql = "INSERT INTO PlayerDeath (user_id, Timestamp, PosX, PosY, PosZ, DeathCause) VALUES ($user,'$timeStamp',$posX, $posY, $posZ,'$deathCause')";   
    }
    else{
        $sql = "INSERT INTO EnemyDeath (user_id, entity_id, Timestamp, PosX, PosY, PosZ, DeathCause) VALUES ($user,$entityId,'$timeStamp',$posX, $posY, $posZ,'$deathCause')";   
    }

    if ($conn->query($sql) === TRUE) 
    {
        echo "Death Data Sent";
    } 
    else 
    {
        echo "PHP: Error al insertar datos: " . mysqli_error($conn);
    }
}

function GetInfo() {

    global $conn;
    $isEnemy = $_POST["isEnemy"];

    if($isEnemy == 1)
    {
        $sql = "SELECT * FROM EnemyDeath";
    }
    else
    {
        $sql = "SELECT * FROM PlayerDeath";
    }

    
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