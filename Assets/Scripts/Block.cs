using UnityEngine;
using System.Collections;

/// <summary>
/// The behavior for block pieces.
/// </summary>
public class Block : MonoBehaviour
{
	private Rigidbody body;
	private enum BlockState
	{
		INIT,
		ACT,
		SET}
	;

	private BlockState state;
	private bool hit;
	public float timer = .5f;

	void Start ()
	{
		body = GetComponent<Rigidbody> ();
		state = BlockState.INIT;
	}

	void Update ()
	{
		switch (state) {
		case BlockState.INIT:
			break;
		case BlockState.ACT:
			if (hit == true && SetBlock (ref timer) == true) {
				state = BlockState.SET;
			}
			break;
		case BlockState.SET:
			break;
		}
	}

	void OnCollisionEnter ()
	{
		hit = true;
	}

	/// <summary>
	/// Set the state of the block to active so that it falls into gameplay.
	/// </summary>
	public void Drop ()
	{
		state = BlockState.ACT;
	}

	/// <summary>
	/// Makes use of a countdown timer in order to deactivate the block after making contact.
	/// </summary>
	/// <returns><c>true</c>, if time > 0, <c>false</c> otherwise.</returns>
	/// <param name="timer">Timer.</param>
	public bool SetBlock (ref float timer)
	{
		if (timer <= 0) {
			Debug.Log("Time Up");
			return true;
		}
		timer -= Time.deltaTime;
		return false;
	}

	public bool IsSet ()
	{
		if (state == BlockState.SET) {
			return true;
		}
		return false;
	}
}
