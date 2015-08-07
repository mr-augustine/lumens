using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

	/// <summary>
	/// Pulls all of the high scores.
	/// </summary>
	public void PullHighScores ()
	{
		pop = PopulateHighScores.Instance;
		string args = "[{\"action\":\"run_sql\"},{\"session_id\":" + User.Instance.GetSessionID () + "},{\"query\":\"select * from HighScores order by SCORE desc\"}]";
		string checkSum = sha256Generator.getHashSha256 (args + User.Instance.GetSessionSalt ());
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22session_id%22:" + User.Instance.GetSessionID () + "},{%22query%22:%22select%20*%20from%20HighScores%20order%20by%20SCORE%20desc%22},{%22checksum%22:%22" + checkSum + "%22}]";
		StartCoroutine (PullHighScoresRoutine ());
	}

	/// <summary>
	/// The coroutine helper function for PullHighScores
	/// </summary>
	/// <returns>The high scores routine.</returns>
	IEnumerator PullHighScoresRoutine ()
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

	/// <summary>
	/// Pulls the high score of the current logged in user.
	/// </summary>
	public void PullHighScore ()
	{
		string args = "[{\"action\":\"run_sql\"},{\"session_id\":" + User.Instance.GetSessionID () + "},{\"query\":\"select SCORE from HighScores where USER='" + User.Instance.GetUserName () + "'\"}]";
		string checkSum = sha256Generator.getHashSha256 (args + User.Instance.GetSessionSalt ());
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22session_id%22:" + User.Instance.GetSessionID () + "},{%22query%22:%22select%20SCORE%20from%20HighScores%20where%20USER=%27" + User.Instance.GetUserName () + "%27%22},{%22checksum%22:%22" + checkSum + "%22}]";
		StartCoroutine (PullHighScoreRoutine ());
	}

	/// <summary>
	/// The coroutine helper function for PullHighScore
	/// </summary>
	/// <returns>The high score routine.</returns>
	IEnumerator PullHighScoreRoutine ()
	{
		www = new WWW (url);
		yield return www;
		response = System.Text.Encoding.ASCII.GetString (www.bytes);
		if (JSONParser.Success (response)) {
			string score = JSONParser.ParseDatabaseQuery (response, "SCORE").FirstOrDefault ();
			int temp = 0;
			int.TryParse (score, out temp);
			User.Instance.SetHighScore (temp);
		}
	}

	/// <summary>
	/// Posts the high score of the current logged in user.
	/// </summary>
	/// <param name="score">Score.</param>
	public void PostHighScore (int score)
	{
		string args = "[{\"action\":\"run_sql\"},{\"session_id\":" + User.Instance.GetSessionID () + "},{\"query\":\"insert into HighScores (USER,SCORE) values (:us,:sc)\"},{\"args\":[{\":us\":\"" + User.Instance.GetUserName () + "\"},{\":sc\":" + score + "}]}]";
		string checkSum = sha256Generator.getHashSha256 (args + User.Instance.GetSessionSalt ());
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22session_id%22:" + User.Instance.GetSessionID () + "},{%22query%22:%22insert%20into%20HighScores%20(USER,SCORE)%20values%20(:us,:sc)%22},{%22args%22:[{%22:us%22:%22" + User.Instance.GetUserName () + "%22},{%22:sc%22:" + score + "}]},{%22checksum%22:%22" + checkSum + "%22}]";
		StartCoroutine (PostHighScoreRoutine ());
	}
	
	/// <summary>
	/// The coroutine helper function for PostHighScore
	/// </summary>
	/// <returns>The high score routine.</returns>
	IEnumerator PostHighScoreRoutine ()
	{
		www = new WWW (url);
		yield return www;
		response = System.Text.Encoding.ASCII.GetString (www.bytes);
		Debug.Log (response);
		if (JSONParser.Success (response)) {
			Debug.Log ("Post Success");
		}
	}

	/// <summary>
	/// Updates the high score of the current logged in user.
	/// </summary>
	/// <param name="score">Score.</param>
	public void UpdateHighScore (int score)
	{
		string args = "[{\"action\":\"run_sql\"},{\"session_id\":" + User.Instance.GetSessionID () + "},{\"query\":\"update HighScores set SCORE=:sc where USER=:us\"},{\"args\":[{\":sc\":" + score + "},{\":us\":\"" + User.Instance.GetUserName () + "\"}]}]";
		string checkSum = sha256Generator.getHashSha256 (args + User.Instance.GetSessionSalt ());
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=[{%22action%22:%22run_sql%22},{%22session_id%22:" + User.Instance.GetSessionID () + "},{%22query%22:%22update%20HighScores%20set%20SCORE=:sc%20where%20USER=:us%22},{%22args%22:[{%22:sc%22:" + score + "},{%22:us%22:%22" + User.Instance.GetUserName () + "%22}]},{%22checksum%22:%22" + checkSum + "%22}]";
		StartCoroutine (UpdateHighScoreRoutine ());
	}
	
	/// <summary>
	/// The coroutine helper function for UpdateHighScore
	/// </summary>
	/// <returns>The high score routine.</returns>
	IEnumerator UpdateHighScoreRoutine ()
	{
		www = new WWW (url);
		yield return www;
		response = System.Text.Encoding.ASCII.GetString (www.bytes);
		if (JSONParser.Success (response)) {
			Debug.Log ("Update Success");
		}
	}
}