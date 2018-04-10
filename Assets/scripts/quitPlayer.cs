using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuizGame {
	public class quitPlayer : MonoBehaviour {

		public void doQuit(){
			Application.Quit ();
			Debug.Log ("quitt");

		}
	}
}
