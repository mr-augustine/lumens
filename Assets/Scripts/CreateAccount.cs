using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour
{
	public Text username, email, password;
	private User user;
	private string userName;
	private string passWord;
	private string url;
	private string checkSum;
	private WWW www;
	private readonly string appcode = "2Be3Jsb3bt";

	void Start ()
	{
		user = User.Instance;
	}
	/// <summary>
	/// Creates a new ParseUser
	/// </summary>
	public void CreateNewAccount ()
	{
		userName = username.text;
		passWord = password.text;
		string arguments = "[{\"action\":\"add_user\"},{\"login\":\"" + userName + "\"},{\"app_code\":\"" 
		+ appcode + "\"},{\"password\":\"" + passWord + "\"}]";
		checkSum = sha256Generator.getHashSha256(arguments);
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=[{\"action\":\"add_user\"},{\"login\":\"" + userName + "\"},{\"app_code\":\"" 
			+ appcode + "\"},{\"password\":\"" + passWord + "\"},{\"checksum\":\"" + checkSum + "\"}]";
		StartCoroutine(Connect());
	
	}
	
	IEnumerator Connect() {
		www = new WWW(url);
		yield return www;
		//Debug.Log(System.Text.Encoding.ASCII.GetString(www.bytes));
		user.LogIn (username.text);
	}
}
