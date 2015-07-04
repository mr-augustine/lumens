using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour
{
	private LineRenderer sweeperBar;
	[SerializeField]
	private Transform
		SweeperSphereTop, SweeperSphereBottom;
	
	void Start ()
	{
		sweeperBar = GetComponent<LineRenderer> ();
		sweeperBar.SetWidth (.45f, .45f);
	}

	void Update ()
	{
		sweeperBar.SetPosition (0, SweeperSphereTop.position);
		sweeperBar.SetPosition (1, SweeperSphereBottom.position);	
	}
}
