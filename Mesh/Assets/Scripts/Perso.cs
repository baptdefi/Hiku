using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Perso : MonoBehaviour {
	int choix;
	int score1;
	int score2;
	int score3;
	int score4;
	public GameObject rank1;
	public GameObject rank2;
	public GameObject rank3;
	public GameObject rank4;
	public Sprite eskimo;
	public Sprite ours;
	public Sprite phoque;
	public Sprite pingouin;
	public GameObject tetej1;
	public GameObject tetej2;
	public GameObject tetej3;
	public GameObject tetej4;
	public List<GameObject> j1Score;
	public List<GameObject> j2Score;
	public List<GameObject> j3Score;
	public List<GameObject> j4Score;
	public Sprite snowball;
	public Sprite premier;
	public Sprite deuxieme;
	public Sprite troisieme;
	public Sprite quatrieme;

	Image j1s;
	Image rank;
	Image img;

	// Use this for initialization
	void Start ()
    {
		// Sprite tete
		img = this.GetComponent<Image> ();
		if (this.name == "Tete1")
        {		
			choix = PlayerPrefs.GetInt ("PreferedModel1");
		}
        else if (this.name == "Tete2")
        {		
			choix = PlayerPrefs.GetInt ("PreferedModel2");
		}
		else if (this.name == "Tete3" && PlayerPrefs.GetInt("nbplayer")>=3)
        {
			choix = PlayerPrefs.GetInt ("PreferedModel3");
		}
		else if (this.name == "Tete4" && PlayerPrefs.GetInt("nbplayer")>=4)
        {
			choix = PlayerPrefs.GetInt ("PreferedModel4");
		}

		if (choix == 0)
			img.sprite = eskimo;
		else if (choix == 1)
			img.sprite = ours;
		else if (choix == 2)
			img.sprite = phoque;
		else if (choix == 3)
			img.sprite = pingouin;

		// Scores
		score1 = (PlayerPrefs.GetInt ("Score1"));
		score2 = (PlayerPrefs.GetInt ("Score2"));
		score3 = (PlayerPrefs.GetInt ("Score3"));
		score4 = (PlayerPrefs.GetInt ("Score4"));

		for (int i = 0; i < 16; i++)
        {
			if (score1 >= (i+1))
            {
				j1s = j1Score [i].GetComponent<Image> ();
				j1s.sprite = snowball;
			}
		}
		for (int i = 0; i < 16; i++)
        {
			if (score2 >= (i+1))
            {
				j1s = j2Score [i].GetComponent<Image> ();
				j1s.sprite = snowball;
			}
		}
		for (int i = 0; i < 16; i++)
        {
			if (score3 >= (i+1))
            {
				j1s = j3Score [i].GetComponent<Image> ();
				j1s.sprite = snowball;
			}
		}
		for (int i = 0; i < 16; i++)
        {
			if (score4 >= (i+1))
            {
				j1s = j4Score [i].GetComponent<Image> ();
				j1s.sprite = snowball;
			}
		}

		// Ranks
		if (PlayerPrefs.GetInt ("nbplayer") == 2)
        {
			if (score1 > score2)
            {
				rank = rank1.GetComponent<Image> ();
				rank.sprite = premier;
				rank = rank2.GetComponent<Image> ();
				rank.sprite = deuxieme;
			}
			if (score1 < score2)
            {
				rank = rank2.GetComponent<Image> ();
				rank.sprite = premier;
				rank = rank1.GetComponent<Image> ();
				rank.sprite = deuxieme;
			}
			if (score1 == score2)
            {
				rank = rank2.GetComponent<Image> ();
				rank.sprite = premier;
				rank = rank1.GetComponent<Image> ();
				rank.sprite = premier;
			}
		}
		if (PlayerPrefs.GetInt ("nbplayer") == 3)
        {
			if (score1 >= score2 && score1> score3)
            {
				rank = rank1.GetComponent<Image> ();
				rank.sprite = premier;
				if((score3 >= score2))
                {
				    rank = rank3.GetComponent<Image> ();
				    rank.sprite = deuxieme;
				    rank = rank2.GetComponent<Image> ();
				    rank.sprite = troisieme;
			    }
				if((score2 >= score3))
                {
					rank = rank2.GetComponent<Image> ();
					rank.sprite = deuxieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = troisieme;
				}
				if (score2 == score3)
                {
					rank = rank2.GetComponent<Image> ();
					rank.sprite = deuxieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = deuxieme;
				}
			}
			if (score2 >= score3 && score2> score1)
            {
				rank = rank2.GetComponent<Image> ();
				rank.sprite = premier;
				if((score3 >= score1))
                {
					rank = rank3.GetComponent<Image> ();
					rank.sprite = deuxieme;
					rank = rank1.GetComponent<Image> ();
					rank.sprite = troisieme;
				}
				if((score1 >= score3))
                {
					rank = rank1.GetComponent<Image> ();
					rank.sprite = deuxieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = troisieme;
				}
				if (score1 == score3)
                {
					rank = rank1.GetComponent<Image> ();
					rank.sprite = deuxieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = deuxieme;
				}
			}
			if (score3 >= score2 && score3> score1)
            {
				rank = rank3.GetComponent<Image> ();
				rank.sprite = premier;
				if((score2 >= score1))
                {
					rank = rank2.GetComponent<Image> ();
					rank.sprite = deuxieme;
					rank = rank1.GetComponent<Image> ();
					rank.sprite = troisieme;
				}
				if((score1 >= score2))
                {
					rank = rank1.GetComponent<Image> ();
					rank.sprite = deuxieme;
					rank = rank2.GetComponent<Image> ();
					rank.sprite = troisieme;
				}
				if (score1 == score2)
                {
					rank = rank1.GetComponent<Image> ();
					rank.sprite = deuxieme;
					rank = rank2.GetComponent<Image> ();
					rank.sprite = deuxieme;
				}
			}
			if (score1 == score2 && score1 == score3)
            {
				rank = rank1.GetComponent<Image> ();
				rank.sprite = premier;
				rank = rank2.GetComponent<Image> ();
				rank.sprite = premier;
				rank = rank3.GetComponent<Image> ();
				rank.sprite = premier;
			}
		}	
		if (PlayerPrefs.GetInt ("nbplayer") == 4)
        {
			if (score1 >= score2 && score1> score3 && score1 >= score4)
            {
				rank = rank1.GetComponent<Image> ();
				rank.sprite = premier;
				if((score3 >= score2)&& (score3 >= score4))
                {
					rank = rank3.GetComponent<Image> ();
					rank.sprite = deuxieme;
					if ((score2 >= score4))
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = troisieme;
						rank = rank4.GetComponent<Image> ();
						rank.sprite = quatrieme;
					}
					else
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = quatrieme;
						rank = rank4.GetComponent<Image> ();
						rank.sprite = troisieme;
					}
				}
				if ((score3 == score2) && (score3 >= score4))
                {
					rank = rank2.GetComponent<Image> ();
					rank.sprite = troisieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = troisieme;
					rank = rank4.GetComponent<Image> ();
					rank.sprite = quatrieme;
				}
				if ((score3 == score4) && (score4 >= score2))
                {
					rank = rank4.GetComponent<Image> ();
					rank.sprite = troisieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = troisieme;
					rank = rank2.GetComponent<Image> ();
					rank.sprite = quatrieme;
				}
				if ((score3 == score4) && (score4 < score2))
                {
					rank = rank4.GetComponent<Image> ();
					rank.sprite = quatrieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = quatrieme;
					rank = rank2.GetComponent<Image> ();
					rank.sprite = deuxieme;
				}
				if ((score2 == score4) && (score4 < score3))
                {
					rank = rank4.GetComponent<Image> ();
					rank.sprite = quatrieme;
					rank = rank2.GetComponent<Image> ();
					rank.sprite = quatrieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = deuxieme;
				}
				if ((score2 == score4) && (score4 >= score3))
                {
					rank = rank4.GetComponent<Image> ();
					rank.sprite = troisieme;
					rank = rank2.GetComponent<Image> ();
					rank.sprite = troisieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = quatrieme;
				}
				if ((score2 == score4) && (score4 == score3))
                {
					rank = rank4.GetComponent<Image> ();
					rank.sprite = quatrieme;
					rank = rank2.GetComponent<Image> ();
					rank.sprite = quatrieme;
					rank = rank3.GetComponent<Image> ();
					rank.sprite = quatrieme;
				}
				if((score2 >= score3)&& (score2 >= score4))
                {
					rank = rank2.GetComponent<Image> ();
					rank.sprite = deuxieme;
					if ((score3 >= score4))
                    {
						rank = rank3.GetComponent<Image> ();
						rank.sprite = troisieme;
						rank = rank4.GetComponent<Image> ();
						rank.sprite = quatrieme;
					}
					else
                    {
						rank = rank3.GetComponent<Image> ();
						rank.sprite = quatrieme;
						rank = rank4.GetComponent<Image> ();
						rank.sprite = troisieme;
					}
				}
				if((score4 >= score3)&& (score4 >= score2))
                {
					rank = rank4.GetComponent<Image> ();
					rank.sprite = deuxieme;
					if ((score3 >= score2))
                    {
						rank = rank3.GetComponent<Image> ();
						rank.sprite = troisieme;
						rank = rank2.GetComponent<Image> ();
						rank.sprite = quatrieme;
					}
					else
                    {
						rank = rank3.GetComponent<Image> ();
						rank.sprite = quatrieme;
						rank = rank2.GetComponent<Image> ();
						rank.sprite = troisieme;
					}
				}
			}
		    if (score2 >= score1 && score2> score3 && score2 >= score4)
            {
			    rank = rank2.GetComponent<Image> ();
			    rank.sprite = premier;
			    if((score3 >= score1)&& (score3 >= score4))
                {
				    rank = rank3.GetComponent<Image> ();
				    rank.sprite = deuxieme;
				    if ((score1 >= score4))
                    {
					    rank = rank1.GetComponent<Image> ();
					    rank.sprite = troisieme;
					    rank = rank4.GetComponent<Image> ();
					    rank.sprite = quatrieme;
				    }
				    else
                    {
					    rank = rank1.GetComponent<Image> ();
					    rank.sprite = quatrieme;
					    rank = rank4.GetComponent<Image> ();
					    rank.sprite = troisieme;
				    }
			    }
			    if((score1 >= score3)&& (score1 >= score4))
                {
				    rank = rank1.GetComponent<Image> ();
				    rank.sprite = deuxieme;
				    if ((score3 >= score4))
                    {
					    rank = rank3.GetComponent<Image> ();
					    rank.sprite = troisieme;
					    rank = rank4.GetComponent<Image> ();
					    rank.sprite = quatrieme;
				    }
				    else
                    {
					    rank = rank3.GetComponent<Image> ();
					    rank.sprite = quatrieme;
					    rank = rank4.GetComponent<Image> ();
					    rank.sprite = troisieme;
				    }
			    }
			    if((score4 >= score3)&& (score4 >= score1))
                {
				    rank = rank4.GetComponent<Image> ();
				    rank.sprite = deuxieme;
				    if ((score3 >= score1))
                    {
					    rank = rank3.GetComponent<Image> ();
					    rank.sprite = troisieme;
					    rank = rank1.GetComponent<Image> ();
					    rank.sprite = quatrieme;
				    }
				    else
                    {
					    rank = rank3.GetComponent<Image> ();
					    rank.sprite = quatrieme;
					    rank = rank1.GetComponent<Image> ();
					    rank.sprite = troisieme;
				    }
			    }
		    }
		    if (score3 >= score1 && score3> score2 && score3 >= score4)
            {
		        rank = rank3.GetComponent<Image> ();
		        rank.sprite = premier;
		        if((score2 >= score1)&& (score2 >= score4))
                {
			        rank = rank2.GetComponent<Image> ();
			        rank.sprite = deuxieme;
			        if ((score1 >= score4))
                    {
				        rank = rank1.GetComponent<Image> ();
				        rank.sprite = troisieme;
				        rank = rank4.GetComponent<Image> ();
				        rank.sprite = quatrieme;
			        }
			        else
                    {
				        rank = rank1.GetComponent<Image> ();
				        rank.sprite = quatrieme;
				        rank = rank4.GetComponent<Image> ();
				        rank.sprite = troisieme;
			        }
		        }
				if((score1 >= score2)&& (score1 >= score4))
                {
					rank = rank1.GetComponent<Image> ();
					rank.sprite = deuxieme;
					if ((score2 >= score4))
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = troisieme;
						rank = rank4.GetComponent<Image> ();
						rank.sprite = quatrieme;
					}
					else
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = quatrieme;
						rank = rank4.GetComponent<Image> ();
						rank.sprite = troisieme;
					}
				}
				if((score4 >= score2)&& (score4 >= score1))
                {
					rank = rank4.GetComponent<Image> ();
					rank.sprite = deuxieme;
					if ((score2 >= score1))
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = troisieme;
						rank = rank1.GetComponent<Image> ();
						rank.sprite = quatrieme;
					}
					else
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = quatrieme;
						rank = rank1.GetComponent<Image> ();
						rank.sprite = troisieme;
					}
				}
			}
			if (score4 >= score1 && score4> score2 && score4 >= score3)
            {
				rank = rank4.GetComponent<Image> ();
				rank.sprite = premier;
				if((score2 >= score1)&& (score2 >= score3))
                {
					rank = rank2.GetComponent<Image> ();
					rank.sprite = deuxieme;
					if ((score1 >= score3))
                    {
						rank = rank1.GetComponent<Image> ();
						rank.sprite = troisieme;
						rank = rank3.GetComponent<Image> ();
						rank.sprite = quatrieme;
					}
					else
                    {
						rank = rank1.GetComponent<Image> ();
						rank.sprite = quatrieme;
						rank = rank3.GetComponent<Image> ();
						rank.sprite = troisieme;
					}
				}
				if((score1 >= score2)&& (score1 >= score3))
                {
					rank = rank1.GetComponent<Image> ();
					rank.sprite = deuxieme;
					if ((score2 >= score3))
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = troisieme;
						rank = rank3.GetComponent<Image> ();
						rank.sprite = quatrieme;
					}
					else
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = quatrieme;
						rank = rank3.GetComponent<Image> ();
						rank.sprite = troisieme;
					}
				}
				if((score3 >= score2)&& (score3 >= score1))
                {
					rank = rank3.GetComponent<Image> ();
					rank.sprite = deuxieme;
					if ((score2 >= score1))
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = troisieme;
						rank = rank1.GetComponent<Image> ();
						rank.sprite = quatrieme;
					}
					else
                    {
						rank = rank2.GetComponent<Image> ();
						rank.sprite = quatrieme;
						rank = rank1.GetComponent<Image> ();
						rank.sprite = troisieme;
					}
				}
			}
		}	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
