using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {
	private LineRenderer sweeperBar;
	private float count;
	private float distance;
	
	public Transform SweeperSphereTop, SweeperSphereBottom;
	public float drawSpeed = 6f;

	// Use this for initialization
	void Start () {
		sweeperBar = GetComponent<LineRenderer> ();
		sweeperBar.SetPosition (0, SweeperSphereTop.position);
		sweeperBar.SetWidth (.45f, .45f);

		distance = Vector3.Distance (SweeperSphereTop.position, SweeperSphereBottom.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (count < distance) {
			count += .1f / drawSpeed;

			float x = Mathf.Lerp(0, distance, count);

			Vector3 startPoint = SweeperSphereTop.position;
			Vector3 endPoint = SweeperSphereBottom.position;
			Vector3 pointInTheLine = x * Vector3.Normalize(endPoint - startPoint) + startPoint;

			sweeperBar.SetPosition(1, pointInTheLine);
		}	
	}
}
