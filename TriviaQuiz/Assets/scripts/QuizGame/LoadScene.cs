using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace QuizGame{
	public class LoadScene : MonoBehaviour {


		public void onPlayButton(string sceneName){
			SceneManager.LoadScene (sceneName);


		}

	}
}
