﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
	private static Block currentBlock;

	private bool holdingDown;
	private static bool newSpawn;

	void Awake ()
	{

	}

	void Update ()
	{	
		if (currentBlock == null)
			return;

		// Move Down
		if (Input.GetKey (KeyCode.DownArrow)) {
			if(holdingDown && newSpawn){
				
			} else {
				currentBlock.MoveDown ();
			}
			holdingDown = true;
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			holdingDown = false;
			newSpawn = false;
		}

		// Move Left
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			currentBlock.MoveLeft ();
		} else if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.LeftArrow)) {
			currentBlock.MoveLeft ();
		}

		// Move Right
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			currentBlock.MoveRight ();
		} else if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.RightArrow)) {
			currentBlock.MoveRight ();
		}

		// Rotate
		if (Input.GetKeyDown (KeyCode.R)) {
			currentBlock.RotateClockwise ();
		}

		float horizontalInput = Input.GetAxis ("Horizontal");
		float verticalInput = Input.GetAxis ("Vertical");

		// Fast Down
		if (verticalInput < 0) {
			currentBlock.MoveDown ();
		}

		// Move Left
		if (horizontalInput < 0) {
			currentBlock.MoveLeft ();
		}

		// Move Right
		if (horizontalInput > 0) {
			currentBlock.MoveRight ();
		}

		// Rotate Left
		if (Input.GetButtonUp ("Fire1")) {
			currentBlock.RotateCounterClockwise ();
		}

		// Rotate Right
		if (Input.GetButtonUp ("Fire2")) {
			currentBlock.RotateClockwise ();
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

	public static void SetNewBlock(){
		newSpawn = true;
	}
}
