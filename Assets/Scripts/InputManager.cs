using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	private static Block currentBlock;

	void Awake ()
	{

	}

	void Update ()
	{	
		if (currentBlock == null)
			return;

		// Fast Down
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			currentBlock.MoveDown();
		}

		// Move Left
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			currentBlock.MoveLeft();
		}

		// Move Right
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			currentBlock.MoveRight();
		}

		// Rotate Left
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			currentBlock.RotateCounterClockwise();
		}

		// Rotate Right
		if (Input.GetKeyDown (KeyCode.RightShift)) {
			currentBlock.RotateClockwise();
		}

		// Hold Move Down
		if (Input.GetKey (KeyCode.DownArrow)) {
			currentBlock.MoveDown ();
		}

		// Hold Move Left
		if (Input.GetKey (KeyCode.LeftArrow)) {
			currentBlock.MoveLeft ();
		}

		// Hold Move Right
		if (Input.GetKey (KeyCode.RightArrow)) {
			currentBlock.MoveRight ();
		}
	}

	/// <summary>
	/// Sets the current block.
	/// </summary>
	/// <param name="cB">Current block.</param>
	public static void SetCurrentBlock (GameObject cB)
	{
		currentBlock = cB.GetComponent<Block> ();
	}
}
