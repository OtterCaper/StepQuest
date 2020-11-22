using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PedometerU.Tests {
    public class StepUtility : MonoBehaviour {
        private StepCounter stepCounter;
        PlayerController player;
        private int currentSteps;

        //public int goal = 20; // to be removed later used for testing. 
        public Text stepText;//, goalText;


        //private int totalStep; 

        // Use this for initialization
        void Start() {
            stepCounter = FindObjectOfType<StepCounter>();
            player = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update() {
            if (stepCounter != null) {
                int newSteps = stepCounter.GetSteps();

                if (currentSteps != newSteps) {
                    currentSteps = newSteps;
                    stepText.text = currentSteps.ToString();
                }

                if (player.currentPath != null) {
                    Node nextNode = player.currentPath[0];
                    if (nextNode != null && currentSteps >= nextNode.cost) {//next node
                        stepCounter.DecrementSteps(nextNode.cost);// decrement saved steps.
                        player.MovePlayer();
                    }
                }
                
            }
        }
        public void IncrementSteps() {
            stepCounter.AddSteps(10);
        }
        public void ResetSteps() {
            stepCounter.ResetSteps();
        }
    } 
}

