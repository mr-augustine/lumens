using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages the flow of gameplay for single player mode.
/// </summary>
public class ManageBlocks : MonoBehaviour
{
	[SerializeField]
	private GameObject[]
		blocks;
	[SerializeField]
	private int
		queueLength;
	private Queue<GameObject> nextBlocks;
	private GameObject currentBlock;

	void Start ()
	{
		nextBlocks = new Queue<GameObject> ();
		StartGame ();
	}

	void Update ()
	{
		if (currentBlock != null) {
			TakeInput ();
			if (currentBlock.GetComponent<Block> ().Finished ()) {
				DropNextBlock ();
			}
		}
	}

	/// <summary>
	/// Fills the queue of blocks and then begins the game.
	/// </summary>
	private void StartGame ()
	{
		int i;
		for (i = 0; i < queueLength; i++) {
			nextBlocks.Enqueue (ChooseBlock ());
		}

		Invoke ("DropNextBlock", 3);
	}

	/// <summary>
	/// Dequeues the next block and calls the block's activate method.
	/// </summary>
	private void DropNextBlock ()
	{
		currentBlock = nextBlocks.Dequeue ();
		currentBlock.GetComponent<Block> ().Activate ();
	}

	/// <summary>
	/// Instantiates a random block from the given list and adds it to the queue.
	/// </summary>
	/// <returns>The block.</returns>
	private GameObject ChooseBlock ()
	{
		int i = Random.Range (0, blocks.Length);
		return (GameObject)Instantiate (blocks [i], new Vector3 (0, 9f, 0), Quaternion.identity);
	}

	/// <summary>
	/// Responds to user input.
	/// </summary>
	private void TakeInput ()
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (!Physics.Raycast (currentBlock.transform.position, Vector3.left, 1)) {
				currentBlock.GetComponent<Rigidbody> ().MovePosition (currentBlock.transform.position + Vector3.left);
			}
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (!Physics.Raycast (currentBlock.transform.position, Vector3.right, 1)) {
				currentBlock.GetComponent<Rigidbody> ().MovePosition (currentBlock.transform.position + Vector3.right);
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			// Reserved for instant placement.
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			// Reserved for rotate.
		}
	}
}
