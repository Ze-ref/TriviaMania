using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour {

	public GameObject heart1, heart2, heart3;
	public static int health = 3;
	void Start () {
		health = 3;
		heart1.gameObject.SetActive (true);
		heart2.gameObject.SetActive (true);
		heart3.gameObject.SetActive (true);

	}
	
	// Update is called once per frame
	void Update () {
		if (health > 3) 
			health = 3;
		

			switch(health){
			case 3:
				heart1.gameObject.SetActive (true);
				heart2.gameObject.SetActive (true);
				heart3.gameObject.SetActive (true);
				break;
			case 2:
				heart1.gameObject.SetActive (true);
				heart2.gameObject.SetActive (true);
				heart3.gameObject.SetActive (false);
				break;
			case 1:
				heart1.gameObject.SetActive (true);
				heart2.gameObject.SetActive (false);
				heart3.gameObject.SetActive (false);
				break;
			case 0:
				heart1.gameObject.SetActive (false);
				heart1.gameObject.SetActive (false);
				heart1.gameObject.SetActive (false);
				break;
			}
		}
	}
