﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score4 : MonoBehaviour
{
	public static int score;        // The player's score.
	public static Score4 Instance;

	Text text;                      // Reference to the Text component.

	void Start()
	{

	}
	void Awake ()
	{ 	
		// Set up the reference.
		text = GetComponent <Text> ();

		// Reset the score.

	}


	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		text.text = "Player 4 : " + score;
	}
	public void reset()
	{
		score = 0;
	}
}