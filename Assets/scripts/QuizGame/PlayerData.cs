using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuizGame {

    /// <summary>
    /// Player container
    /// </summary>
    public class PlayerData {

        private int points;
        public int Points {
            get {
                return points;
            }

            private set {
                points = value;
            }
        }
			
        public void AddPoints(int points) {
            this.points += points;
        }

        public void Reset() {
            this.points = 0;
        }

    }

}
