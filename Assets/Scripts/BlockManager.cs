using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[]
		blockPrefabs;
	private int length;
	private List<GameObject> blocks;

	void Start ()
	{
		blocks = new List<GameObject> ();
	}

	void Update ()
	{
	
	}

	/// <summary>
	/// Chooses a random block prefab, instantiates it, and adds it to the blocks list.
	/// </summary>
	private void GenerateBlock ()
	{

	}
}
