using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{

    public class TitleUI : MonoBehaviour
    {
        #region Variables
        private string loadScene = "PlayScene";
        #endregion

        private void Update()
        {
            //ġƮ - P key
            if(Input.GetKeyDown(KeyCode.P))
            {
                ResetGameData();
            }
        }

        public void Play()
        {
            SceneManager.LoadScene(loadScene);
        }

        public void AllDelete()
        {
            PlayerPrefs.DeleteAll();
        }

        void ResetGameData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}