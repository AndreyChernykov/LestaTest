using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game.Play
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameManager gameManager;
        [SerializeField] Audio audio;
        [SerializeField] TextMeshProUGUI textTime;
        [SerializeField] TextMeshProUGUI textCounter;
        [SerializeField] Toggle toggleOnTimer;
        [SerializeField] Toggle toggleOnCounter;
        [SerializeField] Toggle toggleOnSound;
        [SerializeField] GameObject startPanel;
        [SerializeField] GameObject winPanel;

        string timer = "Time: ";
        string counter = "Steps: ";

        private void DisplayTimer()
        {
            textTime.text = timer + gameManager.Time;
        }

        private void DisplayCounter()
        {
            textCounter.text = counter + gameManager.Step;
        }

        public void StartTimer()
        {
            bool isOn = toggleOnTimer.isOn;
            gameManager.OnTimer = isOn;
            if (isOn)
            {
                GameManager.eventsGame.AddListener(DisplayTimer);
                gameManager.StartTimer();
            }
            else
            {
                GameManager.eventsGame.RemoveListener(DisplayTimer);
                textTime.text = "";
            }

        }

        public void StartStepCounter()
        {
            bool isOn = toggleOnCounter.isOn;
            gameManager.OnCounter = isOn;
            if (isOn)
            {
                GameManager.eventsGame.AddListener(DisplayCounter);
            }
            else
            {
                GameManager.eventsGame.RemoveListener(DisplayCounter);
                textCounter.text = "";
            }
        }

        public void OnSound()
        {
            audio.isPlay = toggleOnSound.isOn;
            audio.PlayMusic();
        }

        public void Play()
        {
            GameManager.eventsGame.AddListener(ToWin);
            startPanel.SetActive(false);
            gameManager.isPlay = true;
        }

        public void ToWin()
        {
            if (gameManager.isWin)
            {
                winPanel.SetActive(true);
            }
        }

        public void Reload()
        {
            gameManager.RestartGame();
        }

        public void Exit()
        {
            gameManager.ExitGame();
        }
    }
}

