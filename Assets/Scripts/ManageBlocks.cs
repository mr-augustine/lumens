using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManageBlocks : MonoBehaviour
{
	private Queue<Block> blocks;

	void Start ()
	{
		blocks = new Queue<Block> ();
		GenerateBlocks ();
	}

	void Update ()
	{
		
	}

	private void GenerateBlocks ()
	{

	}

	private void DropNextBlock ()
	{
		blocks.Dequeue ().Drop ();
	}
}
