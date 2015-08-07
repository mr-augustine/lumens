using UnityEngine;
using System.Collections;

public class User : MonoBehaviour
{
	public static User Instance;
	private string userName;
	private string userID;
	private string sessionID;
	private string sessionSalt;
	private int highScore;
	private bool loggedIn;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}

	/// <summary>
	/// Logs the user in.
	/// </summary>
	/// <param name="name">Name.</param>
	public void LogIn (string name, string[] creds)
	{
		userName = name;
		userID = creds [0];
		sessionID = creds [1];
		sessionSalt = creds [2];
		loggedIn = true;
	}

	/// <summary>
	/// Logs the user out.
	/// </summary>
	public void LogOut ()
	{
		loggedIn = false;
	}

	/// <summary>
	/// Determines whether the user is logged in.
	/// </summary>
	/// <returns><c>true</c> if this instance is logged in; otherwise, <c>false</c>.</returns>
	public bool IsLoggedIn ()
	{
		return loggedIn;
	}

	/// <summary>
	/// Gets the name of the user.
	/// </summary>
	/// <returns>The user name.</returns>
	public string GetUserName ()
	{
		return userName;
	}

	/// <summary>
	/// Gets the user ID.
	/// </summary>
	/// <returns>The user I.</returns>
	public string GetUserID ()
	{
		return userID;
	}

	/// <summary>
	/// Gets the session ID.
	/// </summary>
	/// <returns>The session I.</returns>
	public string GetSessionID ()
	{
		return sessionID;
	}

	/// <summary>
	/// Gets the session salt.
	/// </summary>
	/// <returns>The session salt.</returns>
	public string GetSessionSalt ()
	{
		return sessionSalt;
	}

	/// <summary>
	/// Gets the high score.
	/// </summary>
	/// <returns>The high score.</returns>
	public int GetHighScore ()
	{
		return highScore;
	}
}
