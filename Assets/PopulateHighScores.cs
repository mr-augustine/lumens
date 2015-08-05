using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopulateHighScores : MonoBehaviour
{
	[SerializeField]
	private Text
		scoreBox;
	private Vector3 boxLoc;
	private RectTransform rec;

	void Start ()
	{
		rec = GetComponent<RectTransform> ();
		boxLoc = transform.position;
		Populate ();
	}

	private void Populate ()
	{
		foreach (string s in DatabaseHandler.PullHighScores ()) {
			Text temp = (Text)Instantiate (scoreBox, new Vector3(711, boxLoc.y, 0), Quaternion.identity);
			temp.transform.SetParent (transform);
			temp.text = s;

//			Vector3 tempVec = temp.GetComponent<RectTransform> ().
//			tempVec.x = 0;
//			temp.GetComponent<RectTransform> ().localPosition = tempVec;

			boxLoc += new Vector3 (0, -64, 0);

			rec.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, rec.rect.height + 64);
		}
	}

}
