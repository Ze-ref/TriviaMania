using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace QuizGame {

	public class quiz : MonoBehaviour{

		public Text QuestionUI;
		public InputField AnswerUI;
		public Text ResultUI;
		public Text TriviaUI;
		public Text ScoreUI;
		public Text GameOverScoreText;

		public RectTransform GameOverUI;
		public RectTransform VerifyPanel;

		private int Score;

		[Header("Timer")]
		public Text TimerText;
		public bool timerPaused;
		public Text UsernameText;

		private float RemainingTime = 120;

		public List<string> Question = new List<string>();
		public List<string> Answer = new List<string>();
		public List<string> Trivia = new List<string>();

		private GameController GameController;

		private int ActiveQuestion;
		//private int LastActiveQuestion;
		private bool CheckedTheAnswer;

		void Start() {
			UsernameText.text = PlayerPrefs.GetString ("Username");
			MakeQuestion();
			Score = 0;
		}

		void Update(){
			if (ActiveQuestion != null) {
				DecreaseTimer();
			}
		}

		private void DecreaseTimer() {
			if (timerPaused == false) {

			RemainingTime -= Time.deltaTime;

			UpdateTimer();

			// Check if time expired
			if (RemainingTime <= 0f) {
				GameOver();
				RemainingTime = 0f;
			}
			}
		}

		private void UpdateTimer() {
			TimerText.text = ((int) RemainingTime).ToString();
		}

		void FixedUpdate() {
			if (Input.GetKeyUp(KeyCode.Return) && CheckedTheAnswer == false){// if completed typing the answer
				CheckAnswer();

			}else if (Input.GetKeyUp(KeyCode.Return) && CheckedTheAnswer){
				MakeQuestion();

			}
		}

		public void Exiting(){
			VerifyPanel.gameObject.SetActive (true);
			timerPaused = true;
		}

		public void DontExit(){
			VerifyPanel.gameObject.SetActive (false);
			timerPaused = false;
		}

		void MakeQuestion() {
			GetNewRandomNumber();


			QuestionUI.text = Question[ActiveQuestion]; // show random question



			AnswerUI.text = ("").ToUpper();
			ResultUI.text = "";
			TriviaUI.text = "Trivia will show here";

			CheckedTheAnswer = false;// prepare for next repose

		}


		void CheckAnswer() {
			// compare
			if (AnswerUI.text == Answer[ActiveQuestion]){
				ResultUI.text = "Correct";
				Score += 10;
				Debug.Log (AnswerUI.text);
				ScoreUI.text = "Score:" + Score; 
				TriviaUI.text = Trivia[ActiveQuestion];
				CheckedTheAnswer = true;
				Question.RemoveAt (ActiveQuestion);
				Trivia.RemoveAt (ActiveQuestion);
				Answer.RemoveAt (ActiveQuestion);
			}   else{
				ResultUI.text = "Wrong";
				GameControlScript.health -= 1;
				Debug.Log (AnswerUI.text);
				TriviaUI.text = Trivia[ActiveQuestion];
				CheckedTheAnswer = true;
				Question.RemoveAt (ActiveQuestion);
				Trivia.RemoveAt (ActiveQuestion);
				Answer.RemoveAt (ActiveQuestion);

			}

		}

		public void GameOver() {
			GameOverUI.gameObject.SetActive (true);
			ScoreUI.text = "Score: " + Score;
			ResetScore ();


		}

		public void ResetScore(){
			Score = 0;
		}
		void GetNewRandomNumber(){
			ActiveQuestion = Mathf.FloorToInt(UnityEngine.Random.Range(0, Question.Count -1));

			if((Question.Count) < 1  || (Question.Count) < 6){
				GameOver ();
			}

		}


	}

}

