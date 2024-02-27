<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unitybackend";

//User submitted variables
$userID = $_POST["userID"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection faield: " . $conn->connect_error);
}


$sql = "SELECT itemID FROM usersitems WHERE userID = '". $userID . "'";
$result = $conn->query($sql);

if ($result->num_rows  > 0) {
    $rows = array();
    while($row = $result->fetch_assoc()) {
        $rows[] = $row;
    }
    echo json_encode($rows);
} else {
    echo "0";
}
$conn->close();

?>