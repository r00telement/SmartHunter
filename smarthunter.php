<?php

$_POST = json_decode(file_get_contents('php://input'), true);

$version = $_POST["version"];
$key = $_POST["key"];
$command = $_POST["command"];
$host = $_POST["host"] == "true";
$player = $_POST["player"];
$damage = $_POST["damage"];
$status = "error";
$obj = "";

$development = false;
$currentVersion = "1.2-1";

if ($development) {
	$obj = "dev";
}else if (strcmp($currentVersion, $version) != 0) {
	$obj = "v";
}else if ((isset($key) && strlen($key) == 28 && strcmp(substr($key, -1), "=") == 0) || strcmp($command, "alive") == 0) {
  	$servername = "<server of your db>";
    $username = "<username of your db>";
    $password = "<password of your db>";
    $dbname = "<name of your db>";
    $conn = new mysqli($servername, $username, $password, $dbname);
    if (!$conn->connect_error) {
        $status = "ok";
        if (strcmp($command, "alive") == 0) {
            //Nothing
        }else if (strcmp($command, "damage") == 0 && strlen($player) > 0) {
        	$sql = "SELECT `damage`, `player_count` FROM `SmartHunter` WHERE `key` = '" . $key . "'";
            $result = $conn->query($sql);
            if ($result->num_rows == 1) {
            	$damages = "";
                $row = $result->fetch_assoc();
                if ($row["damage"] != null) {
                	$damages = $row["damage"];
                }
                $damages = json_decode($damages, true);
                $damages[$player] = $damage;
                $sql = "UPDATE `SmartHunter` SET `damage` = '" . json_encode($damages) . "' WHERE `key` = '" . $key . "'";
                if (!$conn->query($sql)) {
                    $status = "error";
                    $obj = $conn->error;
                }else{
                	if ($row["player_count"] > 1) {
                    	unset($damages[$player]);
                    	$obj = json_encode($damages);
                    }else{
                    	$status = "error";
                		$obj = "false";
                    }
                }
            }else{
                $status = "error";
                $obj = "0";
            }
        }else if (strcmp($command, "elevate") == 0) {
            $sql = "SELECT * FROM `SmartHunter` WHERE `key` = '" . $key . "'";
            $result = $conn->query($sql);
            if ($result->num_rows == 1) {
                $sql = "UPDATE `SmartHunter` SET `host_connected` = 1, `last_modify` = CURRENT_TIMESTAMP WHERE `key` = '" . $key . "'";
                if (!$conn->query($sql)) {
                    $status = "error";
                    $obj = $conn->error;
                }else{
                  	$obj = $key;
                }
            }else{
                $status = "error";
                $obj = "0";
            }
        }else if (strcmp($command, "hello") == 0) {
        	$sql = "SELECT * FROM `SmartHunter` WHERE `key` = '" . $key . "'";
            $result = $conn->query($sql);
            if ($result->num_rows == 0) {
                $sql = "INSERT INTO `SmartHunter` (`key`, `host_connected`, `player_count`, `damage`, `data`, `last_modify`) VALUES ('" . $key . "', " . ($host ? "1" : "0") . ", 1, NULL, NULL, CURRENT_TIMESTAMP)";
                if (!$conn->query($sql)) {
                    $status = "error";
                    $obj = $conn->error;
                }else{
                  	$obj = $key;
                }
            }else{
                if ($host) {
                  	$sql = "UPDATE `SmartHunter` SET `host_connected` = 1, `player_count` = `player_count` + 1, `last_modify` = CURRENT_TIMESTAMP WHERE `key` = '" . $key . "'";
                }else{
                  	$sql = "UPDATE `SmartHunter` SET `player_count` = `player_count` + 1, `last_modify` = CURRENT_TIMESTAMP WHERE `key` = '" . $key . "'";
                }
                if (!$conn->query($sql)) {
                	$status = "error";
                	$obj = $conn->error;
                }else{
                  	$obj = $key;
                }
            }
        }else if (strcmp($command, "done") == 0) {
            if ($host) {
                $sql = "UPDATE `SmartHunter` SET `host_connected` = 0, `player_count` = `player_count` - 1, `last_modify` = CURRENT_TIMESTAMP WHERE `key` = '" . $key . "'; ";
            }else{
                $sql = "UPDATE `SmartHunter` SET `player_count` = `player_count` - 1, `last_modify` = CURRENT_TIMESTAMP WHERE `key` = '" . $key . "'; ";
            }
            $sql .= "DELETE FROM `SmartHunter` WHERE `player_count` <= 0 OR CURRENT_TIMESTAMP - `last_modify` > 60; ";
            if (!$conn->multi_query($sql)) {
                $status = "error";
                $obj = $conn->error;
            }else{
                $obj = $key;
            }
        }else if (strcmp($command, "check") == 0) {
            $sql = "SELECT `host_connected`, `player_count` FROM `SmartHunter` WHERE `key` = '" . $key . "'";
            $result = $conn->query($sql);
            if ($result->num_rows == 1) {
                $row = $result->fetch_assoc();
                $sql = "UPDATE `SmartHunter` SET `last_modify` = CURRENT_TIMESTAMP WHERE `key` = '" . $key . "'";
                if ($conn->query($sql)) {
                	$obj = array("host" => $row["host_connected"] == 1 ? "true" : "false", "players" => $row["player_count"] > 1 ? "true" : "false");
                }else{
                	$status = "error";
                	$obj = $conn->error;
                }
            }else{
                $status = "error";
                $obj = "0";
            }
        }else if (strcmp($command, "push") == 0) {
            $data = $_POST["data"];
            if (isset($data)) {
                $sql = "UPDATE `SmartHunter` SET `data` = '" . $data . "', `last_modify` = CURRENT_TIMESTAMP WHERE `key` = '" . $key . "'";
                if (!$conn->query($sql)) {
                    $status = "error";
                    $obj = $conn->error;
                }else{
                    $sql = "SELECT `player_count` FROM `SmartHunter` WHERE `key` = '" . $key . "'";
                    $result = $conn->query($sql);
                    if ($result->num_rows == 1) {
                        $row = $result->fetch_assoc();
                        if ($row["player_count"] > 1) {
                            $obj = "true";
                        }else{
                            $status = "error";
                            $obj = "false";
                        }
                    }else{
                        $status = "error";
                        $obj = "0";
                    }
                }
            }
        }else if (strcmp($command, "pull") == 0) {
            $sql = "SELECT `data`, `host_connected` FROM `SmartHunter` WHERE `key` = '" . $key . "'";
            $result = $conn->query($sql);
            if ($result->num_rows == 1) {
                $row = $result->fetch_assoc();
                if ($row["host_connected"] == 1) {
                    $obj = $row["data"];
                }else{
                    $status = "error";
                    $obj = "false";
                }
            }else{
                $status = "error";
                $obj = "0";
            }
        }
        $conn->close();
    }
}

header('Content-type: application/json');
echo json_encode(['status' => $status, 'result' => $obj]);
