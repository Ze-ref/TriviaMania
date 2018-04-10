using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace QuizGame{

	public class TimeAttack : MonoBehaviour {

		[Header("Question elements")]
		public RectTransform QuestionPanel;
		public Text QuestionText;

		public Text ButtonAText;
		public Text ButtonBText;
		public Text ButtonCText;
		public Text ButtonDText;

		[Header("Quit Elements")]
		public RectTransform VerifyPanel;
		public bool timerPaused;

		[Header("Game Over Element")]
		public RectTransform GameOverButton;
		public Text GameOverPoints;
		public Text HighScoreText;
		public Text HighScoretextUI;

		[Header("User elements")]
		public Text PointsText;


		public Text TimerText;

		[Header("Questions")]
		public List<QuestionSO> QuestionTimeAttack;

		private QuestionSO ActiveQuestion;
		private int score;

		private float RemainingTime = 10;

		private GameController GameController;

		/// <summary>
		/// Called once the object was created.
		/// </summary>
		private void Awake() {

			// Store the instance locally
			GameController = GameController.Instance;
		}

		/// <summary>
		/// Called when the object was instantiated and is reaching its first frame.
		/// </summary>
		void Start() {
			PickQuestion();
			PointsText.text = "Score: " + score;
			HighScoretextUI.text = "High Score: " + PlayerPrefs.GetInt ("HighScore");
		}

		/// <summary>
		/// Update is called once per frame
		/// </summary>
		void Update() {

			// Check if we need the timer
			// Basically, check if there is an active question
			if (ActiveQuestion != null) {
				DecreaseTimer();
			}

		}

		private void DecreaseTimer() {
			if(timerPaused == false){
				RemainingTime -= Time.deltaTime;

				UpdateTimer();

				// Check if time expired
				if (RemainingTime <= 0f) {
					GameOver ();
				}
			}
		}

		private void UpdateTimer() {
			TimerText.text = ((int) RemainingTime).ToString();
		}

		private void PickQuestion() {
			// Select an integer in the range of available questions
			int pick = Random.Range(0, QuestionTimeAttack.Count);
			// Pick it from the list
			ActiveQuestion = QuestionTimeAttack[pick];

			// Remove it so it won't get picked again during this game.
			QuestionTimeAttack.RemoveAt(pick);

			ShowQuestion();
		}

		// Answer button callbacks

		public void onAnswerA() {
			Evaluate(1);
		}

		public void onAnswerB() {
			Evaluate(2);
		}

		public void onAnswerC() {
			Evaluate(3);
		}

		public void onAnswerD() {
			Evaluate(4);
		}
			
		private void Evaluate(int clickedIndex) {
			if (ActiveQuestion.CorrectAnswerIndex == clickedIndex) {
				Correct();
			} else {
				False();
			}
		}

		/// <summary>
		/// Called if the clicked answer was correct
		/// </summary>
		private void Correct() {
			score += 10;
			PointsText.text = "Score: " + score;
			PlayerPrefs.SetInt ("GameScore", score);
			PlayerPrefs.GetInt ("HighScore");

			PickQuestion ();
		}

		/// <summary>
		/// Called when the user clicked a wrong answer.
		/// </summary>
		private void False() {
			PickQuestion ();
		}



		public void GameOver() {
			GameOverButton.gameObject.SetActive (true);
			GameOverPoints.text = score.ToString();
			if (PlayerPrefs.GetInt ("HighScore") < score) {
				PlayerPrefs.SetInt ("HighScore", score);
			} 
			HighScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString ();

		}

		public void Exiting(){
			VerifyPanel.gameObject.SetActive (true);
			timerPaused = true;
		}

		public void DontExit(){
			VerifyPanel.gameObject.SetActive (false);
			timerPaused = false;
		}


		/// <summary>
		/// Moves back to the main menu
		/// </summary>
		public void EndGame() {
			SceneManager.LoadScene("Play");
		}

		/// <summary>
		/// Displays the active question
		/// </summary>
		private void ShowQuestion() {
			QuestionText.text = ActiveQuestion.Question;

			ButtonAText.text = ActiveQuestion.AnswerA;
			ButtonBText.text = ActiveQuestion.AnswerB;
			ButtonCText.text = ActiveQuestion.AnswerC;
			ButtonDText.text = ActiveQuestion.AnswerD;

			QuestionPanel.gameObject.SetActive(true);

		}
	}

}