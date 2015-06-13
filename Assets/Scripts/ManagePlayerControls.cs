using UnityEngine;
using System.Collections;

public class ManagePlayerControls : MonoBehaviour
{

	private Block currentBlock;
	private float hInput;

	void Update ()
	{
		if (currentBlock != null && !currentBlock.IsSet ()) {
			Debug.Log("Current Block Good");
			hInput = Input.GetAxisRaw ("Horizontal");
			currentBlock.transform.Translate (hInput, 0, 0);
		}
	}

	public void SetCurrentBlock (Block currentBlock)
	{
		this.currentBlock = currentBlock;
	}
}
