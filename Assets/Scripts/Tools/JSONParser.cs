using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSONParser
{
	/// <summary>
	/// Returns whether the web service interaction was successful.
	/// </summary>
	/// <param name="response">Response.</param>
	public static bool Success (string response)
	{
		return response.Contains ("\"result\":\"success\"");
	}

	/// <summary>
	/// Parses the login credentials from the web service response.
	/// </summary>
	/// <returns>The login credentials.</returns>
	/// <param name="response">Response.</param>
	public static string[] ParseLoginCredentials (string response)
	{
		string userID = ExtractData ("user_id", "\":", "}", response);
		string sessionID = ExtractData ("session_id", "\":", "}", response);
		string sessionSalt = ExtractData ("session_salt", "\":", "\"", response);
		return new string[] {userID, sessionID, sessionSalt};
	}

	/// <summary>
	/// Parses the database query web service response.
	/// </summary>
	/// <returns>The database query.</returns>
	/// <param name="response">Response.</param>
	/// <param name="flag">Flag.</param>
	public static List<string> ParseDatabaseQuery (string response, string flag)
	{
		return ExtractDataLoop (flag, "\":", "\"", response);
	}

	/// <summary>
	/// Extracts data.
	/// </summary>
	/// <returns>The data.</returns>
	/// <param name="flag">Flag.</param>
	/// <param name="begin">Begin.</param>
	/// <param name="end">End.</param>
	/// <param name="response">Response.</param>
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

	/// <summary>
	/// Extracts multiple pieces of data.
	/// </summary>
	/// <returns>The data loop.</returns>
	/// <param name="flag">Flag.</param>
	/// <param name="begin">Begin.</param>
	/// <param name="end">End.</param>
	/// <param name="response">Response.</param>
	private static List<string> ExtractDataLoop (string flag, string begin, string end, string response)
	{
		List<string> values = new List<string> ();
		string temp;
		int start = 0;
		while (response.Contains(flag)) {
			temp = response.Substring (response.IndexOf (flag));
			temp = temp.Substring (temp.IndexOf (begin) + begin.Length);
			if (temp [0] == '"') {
				temp = temp.Substring (1);
			}
			start = temp.IndexOf (end);
			temp = temp.Substring (0, start);
			values.Add (temp);
			response = response.Substring (response.IndexOf (flag) + flag.Length);
		}
		return values;
	}

}
