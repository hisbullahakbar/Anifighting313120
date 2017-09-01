<?php
	// if use localhost 
	$connect = mysqli_connect('localhost','root', '') or die('Could not connect: ' . mysqli_connect_error());
	mysqli_select_db($connect, 'fairy_tail_fighting')or die('Could not select database');
	
	//if public host
	//$database = mysql_connect('10.9.2.215','hisbullah', 'hisbullah123') or die('Could not connect: ' . mysql_error());
	//mysql_select_db('dbQUIZ')or die('Could not select database');
	
	$idx = $_GET['idx'];
	$idx_fighting = $_GET['idx_fighting'];
	$previous_state = $_GET['previous_state'];
	$range_characters = $_GET['range_characters'];
	$total_lightattack = $_GET['total_lightattack'];
	$total_heavyattack = $_GET['total_heavyattack'];
	$total_rangedattack = $_GET['total_rangedattack'];
	$total_upattack = $_GET['total_upattack'];
	$total_middleattack = $_GET['total_middleattack'];
	$total_downattack = $_GET['total_downattack'];
	$percentage_idle = $_GET['percentage_idle'];
	$percentage_walk = $_GET['percentage_walk'];
	$percentage_walkbackward = $_GET['percentage_walkbackward'];
	$percentage_lightattack = $_GET['percentage_lightattack'];
	$percentage_heavyattack = $_GET['percentage_heavyattack'];
	$percentage_rangedattack = $_GET['percentage_rangedattack'];
	$percentage_jump = $_GET['percentage_jump'];
	$percentage_crouch = $_GET['percentage_crouch'];
	$choosen_state = $_GET['choosen_state'];

	$insertQuery = "insert into `fusms_log_history` values($idx, $idx_fighting, '$previous_state', 
		$range_characters, $total_lightattack, $total_heavyattack, 
		$total_rangedattack, $total_upattack, $total_middleattack, $total_downattack, $percentage_idle, $percentage_walk, 
		$percentage_walkbackward, $percentage_lightattack, $percentage_heavyattack, $percentage_rangedattack, $percentage_jump,
		$percentage_crouch, '$choosen_state');";
	$resultIQ = mysqli_query($connect, $insertQuery) or die('Query Failed : ' . mysqli_error($connect));
	echo "FuSMs Log History saved";
?>