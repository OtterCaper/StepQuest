/* 
*   Pedometer
*   Copyright (c) 2017 Yusuf Olokoba
*/

namespace PedometerU.Tests {

    using UnityEngine;
    using UnityEngine.UI;

    public class StepCounter : MonoBehaviour {

        //public Text stepText, distanceText;
        private Pedometer pedometer;
        private PlayerInfo player;
        private int currentSteps;
        private int savedSteps;

        private void Awake() {
            GameObject.DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            // Create a new pedometer
            pedometer = new Pedometer(OnStep);
            player = FindObjectOfType<PlayerInfo>();
            savedSteps = player.steps;
            
            OnStep(0, 0);
        }

       


        private void OnStep(int steps, double distance) {
            currentSteps = savedSteps + steps;
            player.steps = currentSteps;
        }

        private void OnDisable() {
            // Release the pedometer
            pedometer.Dispose();
            pedometer = null;  
        }

        public void DecrementSteps(int cost) {
            if (currentSteps >= cost) {
                currentSteps -= cost;
                player.steps = currentSteps;
            }
            else {
                Debug.LogError("Cannot decrease steps more than currently owned.");
            }
            
        }
       public int GetSteps() {
            return currentSteps;
        }
        public void ResetSteps() {
            savedSteps = 0;
            currentSteps = 0;
            player.steps = 0;
        }
        public void AddSteps(int x) {
            currentSteps += x;
            player.steps = currentSteps;
        }
    }
}