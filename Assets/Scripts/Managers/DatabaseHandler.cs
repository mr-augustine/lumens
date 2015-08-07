using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DatabaseHandler : MonoBehaviour
{
	public static DatabaseHandler Instance;
	private PopulateHighScores pop;
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
		pop = PopulateHighScores.Instance;
		string args = "[{\"action\":\"run_sql\"},{\"session_id\":" + User.Instance.GetSessionID () + "},{\"query\":\"select * from HighScores order by SCORE desc\"}]";
		string checkSum = sha256Generator.getHashSha256 (args + User.Instance.GetSessionSalt ());
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22session_id%22:" + User.Instance.GetSessionID () + "},{%22query%22:%22select%20*%20from%20HighScores%20order%20by%20SCORE%20desc%22},{%22checksum%22:%22" + checkSum + "%22}]";
		StartCoroutine (SelectQuery ());
	}

	IEnumerator SelectQuery ()
	{
		www = new WWW (url);
		yield return www;
		response = System.Text.Encoding.ASCII.GetString (www.bytes);
		if (JSONParser.Success (response)) {
			string[] users = JSONParser.ParseDatabaseQuery (response, "USER").ToArray ();
			string[] scores = JSONParser.ParseDatabaseQuery (response, "SCORE").ToArray ();
			string[] concat = new string[scores.Length];
			for (int i = 0; i < concat.Length; i ++) {
				concat [i] = users [i] + "\t\t" + scores [i];
			}
			pop.Populate (concat);
		}
	}

	public static void PostHighScore (int score)
	{

	}
}