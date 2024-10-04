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
            //���� ������ ����
            GameManager.BestScore = PlayerPrefs.GetInt("BestScore", 0); //����� ������ ��������

            if (GameManager.Score > GameManager.BestScore) //����� �����Ϳ� ���ϱ�
            {
                GameManager.BestScore = GameManager.Score;
                PlayerPrefs.SetInt("BestScore", GameManager.Score);
                NewText.text = "New";
            }
            else
            {
                NewText.text = null;
            }

            //UI ����
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