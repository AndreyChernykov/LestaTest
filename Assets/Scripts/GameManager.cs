using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Game.BuildScene;
using UnityEngine.SceneManagement;

namespace Game.Play
{
    public class GameManager : MonoBehaviour
    {
        
        public bool OnTimer { get; set; }
        public bool OnCounter { get; set; }

        public bool isWin { get; private set; }
        public bool isPlay { get; set; }

        public int Time { get; private set; }
        public int Step { get; private set; }

        
        public static UnityEvent eventsGame = new UnityEvent();

        IEnumerator timer;

        public static void SendEvent()
        {
            eventsGame.Invoke();
        }

        public void StartTimer()
        {
            timer = Timer();
            StartCoroutine(timer);
        }

        public void StepCounter()
        {
            if(OnCounter)Step++;
            else Step = 0;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void CheckArray(List<int> conditions, List<GameObject> Boxses, GameObject[,] tileArray)
        {
            int points = 0;
            int maxPoints = (tileArray.GetLength(0) - 2) * conditions.Count;

            foreach (int c in conditions)
            {
                TileState conditionState = Boxses[c].GetComponent<Tile>().State;
                
                for (int i = 0; i < tileArray.GetLength(0); i++)
                {
                    TileState state = tileArray[i, c].GetComponent<Tile>().State;
                    if(state == conditionState) points++;
                }
               
            }
            
            if (points == maxPoints)
            {
                isWin = true;
                isPlay = false;
            }
                
        }

        private IEnumerator Timer()
        {
            Time = 0;
            while (OnTimer)
            {
                yield return new WaitForSeconds(1);
                if(isPlay)Time++;
                
            }
            StopCoroutine(timer);
        }
    }
}


