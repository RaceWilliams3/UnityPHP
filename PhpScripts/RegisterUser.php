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

$sql = "SELECT username FROM users WHERE username = '" . $loginUser . "'";

$result = $conn->query($sql);

if ($result->num_rows  > 0) {
    //Tell user that the name already exists
    echo "Username is already taken";
} else {
    echo "creating user...";
    //Insert the user and password into the database
    $sql = "INSERT INTO users (username, password, level, coins) VALUES ('" . $loginUser . "', '" . $loginPass . "',1,0)";
    if ($conn->query($sql) === TRUE) {
        echo "New record created successfully";
    } else {
        echo "Error: ".$sql."<br>".$conn->error;
    }
}
$conn->close();

?>