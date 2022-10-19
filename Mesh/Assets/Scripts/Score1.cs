using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score1 : MonoBehaviour
{
    // The player's score.
    public static int score;        
	public static Score1 Instance;

    // Reference to the Text component.
    Text text;                      

	void Start()
	{
		
	}

	void Awake ()
	{
		// Set up the reference.
		text = GetComponent <Text> ();
	}

	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		text.text = "Player 1 : " + score;
	}

	public void reset()
	{
		score = 0;
	}
}