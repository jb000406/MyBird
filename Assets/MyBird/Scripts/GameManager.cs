using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MyBird
{

    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static bool isStart { get; set; }


        public static bool isDeath { get; set; }

        public static int Score { get; set; }

        public static int BestScore { get; set; }

        //���� UI
        public TextMeshProUGUI scoreText;
        #endregion

        // Start is called before the first frame update
        private void Start()
        {
            //�ʱ�ȭ
            isStart = false;
            isDeath = false;
            Score = 0;
            
        }

        private void Update()
        {
            //score UI
            scoreText.text = Score.ToString();
        }


    }
}