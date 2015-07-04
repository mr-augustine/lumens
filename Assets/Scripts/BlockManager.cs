using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages block creation and deletion.
/// </summary>
public class BlockManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[]
		blockPrefabs;
	[SerializeField]
	private float
		interval;
	[SerializeField]
	private int
		length;
	[SerializeField]
	private Vector3
		startLocation;
	private Queue<GameObject> blocks;
	private GameObject currentBlock;

	void Start ()
	{
		blocks = new Queue<GameObject> ();
		BuildBlocks ();
	}

	void FixedUpdate ()
	{
		DropNextBlock ();
	}

	/// <summary>
	/// Instantiates a randomly chosen block prefab and adds it to the block queue.
	/// </summary>
	private void GenerateBlock ()
	{
		int i = Random.Range (0, blockPrefabs.Length - 1);
		blocks.Enqueue ((GameObject)Instantiate (blockPrefabs [i], startLocation, Quaternion.identity));
	}

	/// <summary>
	/// Fills the block queue with "length" number of blocks.
	/// </summary>
	private void BuildBlocks ()
	{
		for (int i = 0; i < length; i ++) {
			GenerateBlock ();
		}
	}

	/// <summary>
	/// Sets the current block to the next block in the queue and moves the block into play. Determines if
	/// game has ended.
	/// </summary>
	private void DropNextBlock ()
	{
		if (blocks.Count > 0) {
			if (currentBlock == null) {
				currentBlock = blocks.Dequeue ();
				InputManager.SetCurrentBlock (currentBlock);
				currentBlock.GetComponent<Block> ().Begin ();
			} else if (currentBlock.GetComponent<Block> ().AllDone ()) {
				if (currentBlock.GetComponent<Block> ().InDeadZone ()) {
					//Game Over
					Time.timeScale = 0;
				} else {
					currentBlock = blocks.Dequeue ();
					InputManager.SetCurrentBlock (currentBlock);
					currentBlock.GetComponent<Block> ().Begin ();
				}
			}
		}
	}

	/// <summary>
	/// Gets the current block.
	/// </summary>
	/// <returns>The current block.</returns>
	public GameObject GetCurrentBlock ()
	{
		return currentBlock;
	}

	/// <summary>
	/// Gets the interval.
	/// </summary>
	/// <returns>The interval.</returns>
	public float GetInterval ()
	{
		return interval;
	}
}
