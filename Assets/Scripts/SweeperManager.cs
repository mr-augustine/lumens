using UnityEngine;
using System.Collections;

public class SweeperManager : MonoBehaviour
{

	private bool active;
	private Vector3 moveDirection;
	private Vector3 startingPosition;

	public enum Turn { Even, 
						Odd };
	private int iteration; //!< the Timeline's current sweep iteration
	private Turn current;  //!< shortcut that represents the current iteration
	private Turn next;	   //!< shortcut that represents the next iteration

	void Start ()
	{
		Invoke ("Begin", 1);
		moveDirection = Vector3.right;
		startingPosition = transform.position;

		iteration = 1;
		current = Turn.Odd;
		next = Turn.Even;
		PrintIteration();
		PrintPosition ();
	}

	void Update ()
	{
		if (!active)
			return;
		//transform.Translate (moveDirection * .05f);
		//aah I slowed down the Timeline sweep rate to make the pacing
		//less frantic
		transform.Translate (moveDirection * .02f);
		PrintPosition ();
	}

	void OnCollisionEnter (Collision col)
	{
		transform.position = startingPosition;
		IncrementIteration();
		PrintIteration ();
	}

	private void Begin ()
	{
		active = true;
	}

	private void IncrementIteration() {
		iteration += 1;

		next = current;
		current = (iteration % 2 == 0 ? Turn.Even : Turn.Odd);
	}

	private void PrintIteration() {
		Debug.Log ("Iteration #" + iteration + " started; Current Turn: " + current +
		           "; Next Turn: " + next);
	}

	private void PrintPosition() {
		Debug.Log ("3D Position.x: " + this.transform.position.x);

		// aah We use an offset of 8 (i.e. numColumns / 2) to convert between
		// x-position in 3D space and the corresponding grid column
		// TODO remove the magic number 8
		Debug.Log ("Grid Position.column: " + 
		           (Mathf.FloorToInt(this.transform.position.x) + 8));
	}
}
