using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

namespace QuizGame {

    public class MenuController : MonoBehaviour {

        public InputField UsernameInput;

        private void Start() {
            Debug.Log("Start");

        }
			
		void Update(){

		}

        /// <summary>
        /// Start button callback
        /// </summary>
        public void onStartButton() {

			if (UsernameInput.text != "") {
				Debug.Log ("Username: " + UsernameInput.text);
				PlayerPrefs.SetString ("Username", UsernameInput.text);
				SceneManager.LoadScene ("GradeLevel");
			} else {
				UsernameInput.placeholder.GetComponent<Text>().text = "<color=red>Enter a Username!!!</color>";
			}
           

        }
    }

}
