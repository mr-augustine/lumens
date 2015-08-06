using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Parse;

public class Login : MonoBehaviour
{

	public Text username, password;
	private User user;

	void Start ()
	{
		user = User.Instance;
	}
	
	public void LogIn ()
	{

	}
}
