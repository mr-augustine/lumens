using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class sha256Generator : MonoBehaviour
{
	void Start ()
	{
		Debug.Log (getHashSha256 ("hello"));
	}
	
	public static string getHashSha256 (string text)
	{
		byte[] bytes = Encoding.UTF8.GetBytes (text);
		SHA256Managed hashstring = new SHA256Managed ();
		byte[] hash = hashstring.ComputeHash (bytes);
		string hashString = string.Empty;
		foreach (byte x in hash) {
			hashString += string.Format ("{0:x2}", x);
		}
		return hashString;
	}
}
