using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatabaseHandler : MonoBehaviour
{
	public static DatabaseHandler Instance;
	private string url, response;
	private WWW www;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}
	
	public void PullHighScores ()
	{
		string args = "[{\"action\":\"run_sql\"},{\"session_id\":" + User.Instance.GetSessionID () + "},{\"query\":\"select * from highscores\"}]";
		Debug.Log ("args + salt\t" + args + User.Instance.GetSessionSalt ());
		string checkSum = sha256Generator.getHashSha256 (args + User.Instance.GetSessionSalt());
		Debug.Log ("check\t" + checkSum);
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=" +
			"[{\"action\":\"run_sql\"},{session_id\":" + User.Instance.GetSessionID () + "}," +
			"{\"query\":\"select * from highscores\"},{\"checksum\":\"" + checkSum + "\"}]";
		Debug.Log (url);
		StartCoroutine (Query ());
	}

	IEnumerator Query ()
	{
		www = new WWW (url);
		yield return www;
		response = System.Text.Encoding.ASCII.GetString (www.bytes);
		Debug.Log (response);
		if (JSONParser.Success (response)) {
			Debug.Log ("Query SUCCESS");
		}
	}

	public static void UpdateHighScore (int score)
	{
//		string query = string.Format ("INSERT INTO `HighScores` (USER, SCORE) VALUES(?, ?);", user, score);
//		MySqlCommand command = new MySqlCommand (query, con);
//		command.ExecuteNonQuery ();
	}
	
}