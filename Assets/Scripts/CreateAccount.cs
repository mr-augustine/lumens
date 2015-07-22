using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;

public class CreateAccount : MonoBehaviour
{
	public Text username, email, password;
	private User use;

	void Start ()
	{
		use = User.Instance;
	}

	public void CreateNewAccount ()
	{
		var newUser = new ParseUser (){
			Username = username.text,
			Password = password.text,
			Email = email.text
		};

		newUser.SignUpAsync ();

		use.LogIn (user, null);
	}
}
