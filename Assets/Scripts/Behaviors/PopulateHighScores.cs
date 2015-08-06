using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopulateHighScores : MonoBehaviour
{
	[SerializeField]
	private DatabaseHandler
		dbHandle;
	private Text
		scoreBox;
	private Vector3 boxLoc;
	private RectTransform rec;

	void Start ()
	{
		dbHandle = DatabaseHandler.Instance;
		rec = GetComponent<RectTransform> ();
		boxLoc = transform.position;
		Populate ();
	}

	private void Populate ()
	{
		dbHandle.PullHighScores ();
//		foreach (string s in dbHandle.PullHighScores ()) {
//			Text temp = (Text)Instantiate (scoreBox, new Vector3 (711, boxLoc.y, 0), Quaternion.identity);
//			temp.transform.SetParent (transform);
//			temp.text = s;
//
//			boxLoc += new Vector3 (0, -64, 0);
//
//			rec.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, rec.rect.height + 64);
//		}
	}

}
