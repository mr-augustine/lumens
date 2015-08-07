using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour
{
	public Text username, email, password;
	private User user;
	private string url;
	private WWW www;

	void Start ()
	{
		user = User.Instance;
	}

	public void CreateNewAccount ()
	{
		string userName = username.text;
		string secureWord = sha256Generator.getHashSha256 (password.text);
		string arguments = "[{\"action\":\"add_user\"},{\"login\":\"" + userName + "\"},{\"app_code\":\"2Be3Jsb3bt\"},{\"password\":\"" + secureWord + "\"}]";
		string checkSum = sha256Generator.getHashSha256 (arguments);
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=[{\"action\":\"add_user\"},{\"login\":\"" + userName + "\"},{\"app_code\":\"2Be3Jsb3bt\"},{\"password\":\"" + secureWord + "\"},{\"checksum\":\"" + checkSum + "\"}]";
		StartCoroutine (Connect ());
	}
	
	IEnumerator Connect ()
	{
		www = new WWW (url);
		yield return www;
		string response = System.Text.Encoding.ASCII.GetString (www.bytes);
		if (JSONParser.Success (response)) {
			ChangeScene.ChangeToSceneProg (0);
		} else {
			Debug.Log ("Account already exists");
		}
	}
}
