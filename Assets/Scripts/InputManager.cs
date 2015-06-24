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

		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");

		// Fast Down
		if (verticalInput < 0) {
			currentBlock.MoveDown ();
		}

		// Move Left
		if (horizontalInput < 0) {
			currentBlock.MoveLeft();
		}

		// Move Right
		if (horizontalInput > 0) {
			currentBlock.MoveRight();
		}

		// Rotate Left
		if (Input.GetButtonUp("Fire1")) {
			currentBlock.RotateCounterClockwise();
		}

		// Rotate Right
		if (Input.GetButtonUp("Fire2")) {
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
