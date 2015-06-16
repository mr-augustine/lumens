using UnityEngine;
using System.Collections;

/// <summary>
/// The behavior for block pieces.
/// </summary>
public class Block : MonoBehaviour
{
	private Rigidbody body;
	private bool active, landed;
	[SerializeField]
	private float
		time, reset;
	private Ray ray;
	private RaycastHit hit;

	void Start ()
	{
		body = GetComponent<Rigidbody> ();
	}

	void Update ()
	{
		if (active) {
			Debug.DrawLine (ray.origin, hit.point);
			if (!landed) {
				CountDown (ref time, ref reset);
			}
		}
	}

	/// <summary>
	/// Checks the distance to floor.
	/// </summary>
	private void CheckDistanceToFloor ()
	{
		ray = new Ray (transform.position, new Vector3 (0, -1, 0));
		if (Physics.Raycast (ray, out hit)) {
			if (Vector3.Distance (ray.origin, hit.point) <= 1.5f) {
				landed = true;
				return;
			}
		}
		Fall ();
	}

	/// <summary>
	/// Counts down until the next y-axis decrement.
	/// </summary>
	/// <param name="time">Time.</param>
	/// <param name="reset">Reset.</param>
	private void CountDown (ref float time, ref float reset)
	{
		if (time <= 0) {
			reset = reset * .9f;
			time = reset;
			CheckDistanceToFloor ();
		} else {
			time -= Time.deltaTime;
		}
	}

	/// <summary>
	/// Decrements the y-axis value.
	/// </summary>
	private void Fall ()
	{
		body.MovePosition (transform.position + Vector3.down);
	}

	/// <summary>
	/// Sets the block gameobject active state and active boolean values to true.
	/// </summary>
	public void Activate ()
	{
		gameObject.SetActive (true);
		active = true;
	}

	/// <summary>
	/// Returns whter the block has landed or not.
	/// </summary>
	public bool Finished ()
	{
		return landed && active;
	}
}
