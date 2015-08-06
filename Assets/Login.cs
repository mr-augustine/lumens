using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Parse;

public class Login : MonoBehaviour
{

	public Text username, password;
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
	public void LogIn ()
	{
		userName = username.text;
		passWord = password.text;
		Debug.Log (passWord);
		string secureWord = sha256Generator.getHashSha256 (passWord);
		Debug.Log (secureWord);
		string arguments = "[{\"action\":\"login\"},{\"login\":\"" + userName + "\"},{\"password\":\"" + secureWord + "\"},{\"app_code\":\"2Be3Jsb3bt\"}]";
		checkSum = sha256Generator.getHashSha256(arguments);
		url = "https://devcloud.fulgentcorp.com/bifrost/ws.php?json=[{\"action\":\"login\"},{\"login\":\"" + userName + "\"},{\"password\":\"" + secureWord + "\"},{\"app_code\":\"2Be3Jsb3bt\"},{\"checksum\":\"" + checkSum + "\"}]";
		StartCoroutine(Connect());
		
	}
	
	IEnumerator Connect() {
		www = new WWW(url);
		yield return www;
		Debug.Log(System.Text.Encoding.ASCII.GetString(www.bytes));
		user.LogIn (username.text);
	}
}
