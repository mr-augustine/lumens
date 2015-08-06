using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;

public class CreateAccount : MonoBehaviour
{
	public Text username, email, password;
	private User user;

	void Start ()
	{
		user = User.Instance;
	}
	/// <summary>
	/// Creates a new ParseUser
	/// </summary>
	public void CreateNewAccount ()
	{
		var newUser = new ParseUser (){
			Username = username.text,
			Password = password.text,
			Email = email.text
		};

		newUser.SignUpAsync ();

		user.LogIn (username.text);

		ChangeScene.ChangeToSceneProg (0);
	}
}
