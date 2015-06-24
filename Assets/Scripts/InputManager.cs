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
