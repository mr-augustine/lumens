using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PopulateHighScores : MonoBehaviour
{
	public static PopulateHighScores Instance;
	private DatabaseHandler
		dbHandle;
	[SerializeField]
	private Text
		scoreBox;
	private Vector3 boxLoc;
	private RectTransform rec;

	void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		dbHandle = DatabaseHandler.Instance;
		rec = GetComponent<RectTransform> ();
		boxLoc = transform.position;
		dbHandle.PullHighScores ();
	}

	/// <summary>
	/// Fills the screen with high score values.
	/// </summary>
	/// <param name="scores">Scores.</param>
	public void Populate (string[] scores)
	{
		foreach (string s in scores) {
			Text temp = (Text)Instantiate (scoreBox, new Vector3 (711, boxLoc.y, 0), Quaternion.identity);
			temp.transform.SetParent (transform);
			temp.text = s;
			boxLoc += new Vector3 (0, -64, 0);
			rec.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, rec.rect.height + 64);
		}
	}

}
