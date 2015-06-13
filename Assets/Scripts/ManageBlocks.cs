using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManageBlocks : MonoBehaviour
{
	private ManagePlayerControls MPC;
	[SerializeField]
	private GameObject
		block;
	private Block currentBlock;

	void Start ()
	{
		MPC = GameObject.Find ("GameManager").GetComponent<ManagePlayerControls> ();
		Invoke ("GenerateBlock", 3);
	}

	void Update ()
	{
		if (currentBlock != null && currentBlock.IsSet ()) {
			GenerateBlock ();
		}
	}

	/// <summary>
	/// Instantiates a block and then drops it into gameplay.
	/// </summary>
	private void GenerateBlock ()
	{
		GameObject temp = (GameObject)Instantiate (block, new Vector3 (0, 15, 0), Quaternion.identity);
		currentBlock = temp.GetComponent<Block> ();
		currentBlock.Drop ();
		MPC.SetCurrentBlock (currentBlock);
	}
}
