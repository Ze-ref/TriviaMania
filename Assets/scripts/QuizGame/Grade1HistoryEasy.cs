using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace QuizGame {
public class Grade1HistoryEasy :  MonoBehaviour {

	[Header("Question elements")]
	public RectTransform QuestionPanel;
	public Text QuestionText;

	public Text ButtonAText;
	public Text ButtonBText;
	public Text ButtonCText;
	public Text ButtonDText;

	[Header("Trivia elements")]
	public RectTransform TriviaPanel;
	public Text TriviaText;
	
	[Header("Quit Elements")]
	public RectTransform VerifyPanel;
	

	public RectTransform NextQuestionButton;
	public RectTransform EndGameButton;

	[Header("Game Over Element")]
	public RectTransform GameOverButton;
	public Text GameOverPoints;
	public bool timerPaused;

	[Header("User elements")]
	public Text UsernameText;
	public Text PointsText;


	public Text TimerText;

	[Header("Questions")]
	public List<QuestionSO> questionsGrade1HistoryEasy;

	private QuestionSO ActiveQuestion;
	private int score;

	[Range(0, 11)]
	public int QuestionTime = 11;

	private float RemainingTime = 0;

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
		UsernameText.text = PlayerPrefs.GetString ("Username");
		PickQuestion();
		PointsText.text = "Score: " + score;
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
					EndQuestion();
				}
			}
		}

	private void UpdateTimer() {
		TimerText.text = ((int) RemainingTime).ToString();
	}

	private void PickQuestion() {
		// Select an integer in the range of available questions
		int pick = Random.Range(0, questionsGrade1HistoryEasy.Count);
		// Pick it from the list
		ActiveQuestion = questionsGrade1HistoryEasy[pick];

		// Remove it so it won't get picked again during this game.
		questionsGrade1HistoryEasy.RemoveAt(pick);

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

	// Evaluation

	/// <summary>
	/// Evaluates the answer clicked by its index
	/// </summary>
	/// <param name="clickedIndex"></param>
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
		NextQuestionButton.gameObject.SetActive(true);
		EndGameButton.gameObject.SetActive(false);

		// Add points to player
		score += 10;
		PointsText.text = "Score: " + score;

		// Update points

		ShowTrivia();

		ActiveQuestion = null;
	}

	/// <summary>
	/// Called when the user clicked a wrong answer.
	/// </summary>
	private void False() {
		NextQuestionButton.gameObject.SetActive(true);
		EndGameButton.gameObject.SetActive(false);

		ShowTrivia();

		EndQuestion();
	}

	// General methods
	/// <summary>
	/// Used to show the trivia of the current question.
	/// </summary>
	private void ShowTrivia() {
		QuestionPanel.gameObject.SetActive(false);

		TriviaText.text = ActiveQuestion.trivia;
		TriviaPanel.gameObject.SetActive(true);
	}

	/// <summary>
	/// Shows
	/// </summary>
	private void EndQuestion() {
		// End the round for this question
		ShowTrivia();

		ActiveQuestion = null;
	}

	public void GameOver() {
			GameOverButton.gameObject.SetActive (true);
			GameOverPoints.text = score.ToString();

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
	/// Used by the trivia to navigate to the next question.
	/// </summary>
	public void Next() {
		TriviaPanel.gameObject.SetActive(false);

		if (questionsGrade1HistoryEasy.Count > 0 && questionsGrade1HistoryEasy.Count > 5) {
			PickQuestion ();
		} else {
			GameOver ();
		}

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

		RemainingTime = QuestionTime;
	}

}

}