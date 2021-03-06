﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SweeperManager : MonoBehaviour
{
	public Text scoreText;
	public Text deletedText;
	public Text levelText;
	public Text timerText;
	public Text hiScoreText;
	private float timer = 0.0f;
	private int minutes;
	private int seconds;
	private int level = 1;
	private bool active;
	private Vector3 moveDirection;
	private Vector3 startingPosition;

	public enum Turn
	{
		Even, 
		Odd }
	;
	private int iteration; //!< the Timeline's current sweep iteration
	private Turn current;  //!< shortcut that represents the current iteration
	private Turn next;	   //!< shortcut that represents the next iteration
	private int gridColumn; //!< column position within the grid matrix
	private Grid grid;
	private Deleter deleter;
	private int deleteCount;

	void Start ()
	{
		Invoke ("Begin", 1);
		moveDirection = Vector3.right;
		startingPosition = transform.position;

		iteration = 1;
		current = Turn.Odd;
		next = Turn.Even;
		PrintIteration ();
		UpdateGridColumn ();
		PrintColumnPosition ();
		grid = GameObject.FindGameObjectWithTag ("SinglePlayerScene").GetComponent<Grid> ();
		deleter = new Deleter (grid, this);
		deleteCount = 0;
		scoreText.text = "" + deleter.GetScore ();
		deletedText.text = "" + deleter.GetDeleted ();
		if (User.Instance.IsLoggedIn ()) {
			hiScoreText.text = User.Instance.GetHighScore ().ToString ();
		}
	}

	void Update ()
	{
		if (!active)
			return;
		//transform.Translate (moveDirection * .05f);
		//aah I slowed down the Timeline sweep rate to make the pacing
		//less frantic
		transform.Translate (moveDirection * .01f);
		UpdateGridColumn ();
		//PrintColumnPosition ();
		
		//Putting up the Score!
		scoreText.text = "" + deleter.GetScore ();
		
		//Putting up how many squares were deleted
		deletedText.text = "" + deleter.GetDeleted ();
		
		//Changing level by score of 500
		if ((deleter.GetScore () / 500) == 0) {
			level = 1;
		} else {
			level = (deleter.GetScore () / 500) + 1;
		}
		levelText.text = "" + level;
		
		//Timer stuff
		timer += Time.deltaTime;
		minutes = Mathf.FloorToInt (timer / 60f);
		seconds = Mathf.FloorToInt (timer - minutes * 60);
		timerText.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
	}

	void OnCollisionEnter (Collision col)
	{
		transform.position = startingPosition;
		IncrementIteration ();
		PrintIteration ();
		grid.Notify (this);
	}

	private void Begin ()
	{
		active = true;
	}

	private void IncrementIteration ()
	{
		iteration += 1;
		deleteCount = 0;
		next = current;
		current = (iteration % 2 == 0 ? Turn.Even : Turn.Odd);
	}

	private void UpdateGridColumn ()
	{
		if (gridColumn != Grid.toCol (this.transform.position.x)) {
			gridColumn = Grid.toCol (this.transform.position.x);
			if (gridColumn == 0)
				deleter.Delete (Grid.MAXCOLUMN);
			else
				deleter.Delete (gridColumn - 1);
		}
	}

	public int GetGridColumn ()
	{
		return gridColumn;
	}

	private void PrintIteration ()
	{
		Debug.Log ("Iteration #" + iteration + " started; Current Turn: " + current +
			"; Next Turn: " + next);
	}

	private void PrintColumnPosition ()
	{
		Debug.Log ("Timeline 3D Position.x: " + this.transform.position.x);

		// aah We use an offset of 8 (i.e. numColumns / 2) to convert between
		// x-position in 3D space and the corresponding grid column
		// TODO remove the magic number 8
		Debug.Log ("Timeline Grid Position.column: " + gridColumn);
	}

	public void SetActive (bool boola)
	{
		active = boola;
	}
}
