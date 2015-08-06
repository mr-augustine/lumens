using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Parse;

public class Login : MonoBehaviour
{

	public Text userField, passField;
	public GameObject failure;
	private User user;
	private string url;
	private WWW www;
	
	void Start ()
	{
		user = User.Instance;
	}

	public void LogIn ()
	{
		string userName = userField.text;
		string secureWord = sha256Generator.getHashSha256 (passField.text);
		string args = "[{\"action\":\"login\"},{\"login\":\"" + userName + "\"}," +
			"{\"password\":\"" + secureWord + "\"},{\"app_code\":\"2Be3Jsb3bt\"}]";
		string checkSum = sha256Generator.getHashSha256 (args);
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=" +
			"[{\"action\":\"login\"},{\"login\":\"" + userName + "\"}," +
			"{\"password\":\"" + secureWord + "\"},{\"app_code\":\"2Be3Jsb3bt\"}," +
			"{\"checksum\":\"" + checkSum + "\"}]";
		Debug.Log (url);
		StartCoroutine (Connect ());
	}
	
	IEnumerator Connect ()
	{
		www = new WWW (url);
		yield return www;
		string response = System.Text.Encoding.ASCII.GetString (www.bytes);
		if (JSONParser.Success (response)) {
			user.LogIn (userField.text, JSONParser.ParseLoginCredentials (response));
			Debug.Log("LOGIN SUCCESS");
			ChangeScene.ChangeToSceneProg(0);
		} else {
			failure.SetActive(true);
		}
	}
}
