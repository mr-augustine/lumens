using UnityEngine;
using System.Collections;

public class User : MonoBehaviour
{

	public static User Instance;
	private string userName;
	private int[] highScores;
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
	public void LogIn (string name)
	{
		this.name = name;
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
	/// Gets the high scores.
	/// </summary>
	/// <returns>The high scores.</returns>
	public int[] GetHighScores ()
	{
		return highScores;
	}

	/// <summary>
	/// Gets a high score at th given index.
	/// </summary>
	/// <returns>The high score.</returns>
	/// <param name="index">Index.</param>
	public int GetHighScore (int index)
	{
		if (highScores.Length == 0 || index < 0 || index > highScores.Length - 1) {
			return -1;
		}
		return highScores [index];
	}

}
