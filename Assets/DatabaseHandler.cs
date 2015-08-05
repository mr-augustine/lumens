using UnityEngine;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

public class DatabaseHandler : MonoBehaviour
{
	public static DatabaseHandler dbcon;
	[SerializeField]
	private string
		server, uid, pwd, database;
	private string credentials;
	private static MySqlConnection con;
	
	void Start ()
	{
		dbcon = this;
		try {
			credentials = string.Format ("server={0};uid={1};pwd={2};database={3};", server, uid, pwd, database);
			con = new MySqlConnection ();
			con.ConnectionString = credentials;
			con.Open ();
		} catch (System.Exception e) {
			Debug.Log (e.Message + "\tFailed to connect to database.");
		}
	}
	
	void OnApplicationQuit ()
	{
		con.Close ();
	}
	
	public static List<string> PullHighScores ()
	{
		List<string> highscores = new List<string> ();
		string query = "SELECT * FROM `HighScores` ORDER BY `Score` DESC;";
		MySqlCommand command = new MySqlCommand (query, con);
		MySqlDataReader reader = command.ExecuteReader ();
		while (reader.Read()) {
			highscores.Add (reader [0] + "\t\t" + reader [1]);
		}
		reader.Close ();
		return highscores;
	}

	public static void AddHighScore (string user, int score)
	{
		string query = string.Format ("INSERT INTO `HighScores` (USER, SCORE) VALUES(?, ?);", user, score);
		MySqlCommand command = new MySqlCommand (query, con);
		command.ExecuteNonQuery ();
	}
	
}