<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unitybackend";

//variables submitted by user
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection faield: " . $conn->connect_error);
}

$sql = "SELECT password FROM users WHERE username = '" . $loginUser . "'";

$result = $conn->query($sql);

if ($result->num_rows  > 0) {
    while($row = $result->fetch_assoc()) {
        if($row["password"] == $loginPass) {
            echo "Login Success.";
        } else {
            echo "Wrong Credentials";
        }
    }
} else {
    echo "Username does not exist";
}
$conn->close();

?>