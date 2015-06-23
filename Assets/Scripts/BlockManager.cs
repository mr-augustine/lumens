﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	void Update ()
	{
		DropNextBlock ();
	}

	private void BuildBlocks ()
	{
		for (int i = 0; i < length; i ++) {
			GenerateBlock ();
		}
	}
	
	private void GenerateBlock ()
	{
		int i = Random.Range (0, blockPrefabs.Length - 1);
		blocks.Enqueue ((GameObject)Instantiate (blockPrefabs [i], startLocation, Quaternion.identity));
	}

	private void DropNextBlock ()
	{
		if (currentBlock == null || currentBlock.GetComponent<Block> ().AllDone ()) {
			currentBlock = blocks.Dequeue ();
			currentBlock.GetComponent<Block> ().Begin ();
		}
	}

	public float GetInterval ()
	{
		return interval;
	}
}
