using UnityEngine;
using System.Collections;

public class SweeperManager : MonoBehaviour
{

	private bool active;
	private Vector3 moveDirection;

	void Start ()
	{
		Invoke ("Begin", 1);
		moveDirection = Vector3.left;
	}

	void Update ()
	{
		if (!active)
			return;
		transform.Translate (moveDirection * .1f);
	}

	void OnCollisionEnter (Collision col)
	{
		moveDirection *= -1;
	}

	private void Begin ()
	{
		active = true;
	}
}
