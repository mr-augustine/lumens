using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuHandler : MonoBehaviour
{

	public GameObject logIn, createAccount, userName, highScores;
	
	void Start ()
	{
		if (User.Instance.IsLoggedIn ()) {
			logIn.SetActive (false);
			createAccount.SetActive (false);
			userName.SetActive (true);
			userName.GetComponentInChildren<Text> ().text = User.Instance.GetUserName ();
			highScores.GetComponent<Button> ().interactable = true;
		} else {
			highScores.GetComponent<Button> ().interactable = false;
		}
	}
}
