using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{

    public class TitleUI : MonoBehaviour
    {
        #region Variables
        private string loadScene = "MyPlayScene";

        public GameObject optionUI;
        #endregion

        private void Update()
        {
            
        }

        public void Play()
        {
            SceneManager.LoadScene(loadScene);
        }

        public void ShowOptionUI()
        {
            optionUI.SetActive(true);
        }

    }
}