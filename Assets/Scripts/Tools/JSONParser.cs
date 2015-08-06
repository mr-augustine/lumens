using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSONParser
{

	public static bool Success (string response)
	{
		return response.Contains ("\"result\":\"success\"");
	}

	public static string[] ParseLoginCredentials (string response)
	{
		string userID = ExtractData ("user_id", "\":", "}", response);
		string sessionID = ExtractData ("session_id", "\":", "}", response);
		string sessionSalt = ExtractData ("session_salt", "\":", "\"", response);
		return new string[] {userID, sessionID, sessionSalt};
	}

	public static List<string> ParseDatabaseQuery (string response, string flag)
	{
		foreach (string s in ExtractDataLoop (flag, "\":", "\"", response)) {
			Debug.Log (s);
		}
		return null;
	}

	private static string ExtractData (string flag, string begin, string end, string response)
	{
		string temp = response.Substring (response.LastIndexOf (flag));
		temp = temp.Substring (temp.IndexOf (begin) + begin.Length);
		if (temp [0] == '"') {
			temp = temp.Substring (1);
		}
		temp = temp.Substring (0, temp.IndexOf (end));
		return temp;
	}

	private static List<string> ExtractDataLoop (string flag, string begin, string end, string response)
	{
		List<string> values = new List<string> ();
		string temp;
		int start = 0;
		while (response.Contains(flag)) {
			temp = response.Substring (response.LastIndexOf (flag));
			temp = temp.Substring (temp.IndexOf (begin) + begin.Length);
			if (temp [0] == '"') {
				temp = temp.Substring (1);
			}
			start = temp.IndexOf (end);
			temp = temp.Substring (0, start);
			values.Add (temp);
			response = response.Substring (start);
		}
		return values;
	}

}
