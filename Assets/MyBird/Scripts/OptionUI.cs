using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{

    public class OptionUI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AllDelete()
        {
            PlayerPrefs.DeleteAll();
        }

        public void OutOptionUI()
        {
            gameObject.SetActive(false);
        }
    }
}