using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace MyBird
{

    public class GameOverUI : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI BestScore;
        public TextMeshProUGUI Score;
        public TextMeshProUGUI NewText;

        private string loadScene = "TitleScene";
        #endregion


        private void OnEnable()
        {
            //게임 데이터 저장
            GameManager.BestScore = PlayerPrefs.GetInt("BestScore", 0); //저장된 데이터 가져오기

            if (GameManager.Score > GameManager.BestScore) //저장된 데이터와 비교하기
            {
                GameManager.BestScore = GameManager.Score;
                PlayerPrefs.SetInt("BestScore", GameManager.Score);
                NewText.text = "New";
            }
            else
            {
                NewText.text = null;
            }

            //UI 연결
            BestScore.text = GameManager.BestScore.ToString();
            Score.text = GameManager.Score.ToString();
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            GameManager.isDeath = false;
            GameManager.isStart = false;
            GameManager.Score = 0;
        }

        public void Menu()
        {
            SceneManager.LoadScene(loadScene);
        }


    }
}