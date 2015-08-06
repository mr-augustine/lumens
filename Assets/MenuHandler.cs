using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuHandler : MonoBehaviour
{

	public GameObject logIn, createAccount, userName, logOut;
	
	void Start ()
	{
		if (User.Instance.IsLoggedIn ()) {
			logIn.SetActive (false);
			createAccount.SetActive (false);
			userName.SetActive (true);
			userName.GetComponentInChildren<Text> ().text = User.Instance.GetUserName ();
			logOut.SetActive (true);
		}
	}
	
	public void OnLogOut ()
	{
		logIn.SetActive (true);
		createAccount.SetActive (true);
		userName.SetActive (false);
		logOut.SetActive (false);
	}
}
