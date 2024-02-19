<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unitybackend";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection faield: " . $conn->connect_error);
}
echo "Connected Successfully, here are the users. <br><br>";

$sql = "SELECT username, level FROM users";
$result = $conn->query($sql);

if ($result->num_rows  > 0) {
    while($row = $result->fetch_assoc()) {
        echo "Username: " . $row["username"] . " - Level: " . $row["level"] . "<br>";
    }
} else {
    echo "0 results";
}
$conn->close();

?>