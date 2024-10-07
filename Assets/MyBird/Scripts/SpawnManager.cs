using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace MyBird
{

    public class SpawnManager : MonoBehaviour
    {
        #region Variables
        public GameObject pipePref;

        Vector3 point;

        //스폰 타이머
        [SerializeField] private float spawnTimer = 1.0f;
        private float countdown = 1f;

        [SerializeField] private float maxspawnTimer = 1.05f;
        [SerializeField] private float minspawnTimer = 0.95f;
        public static float levelTimer = 0f;


        //스폰 위치
        [SerializeField] private float maxSpawnY = 3.5f;
        [SerializeField] private float minSpawnY = -1.5f;

        #endregion
        // Start is called before the first frame update
        private void Start()
        {
            //초기화
            countdown = 0f;
        }

        // Update is called once per frame
        private void Update()
        {
            //게임 진행 체크
            if (GameManager.isStart == false)
                return;

            //스폰 타이머
            if(countdown <= 0f) 
            {
                //스폰
                SpawnPipe();

                //타이머 초기화
                countdown = Random.Range(minspawnTimer, maxspawnTimer - levelTimer);
            }

            countdown -= Time.deltaTime;
        }

        void SpawnPipe()
        {
            float spawnY = transform.position.y + Random.Range(minSpawnY, maxSpawnY);
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0f);
            Instantiate(pipePref, spawnPosition, Quaternion.identity);
        }
    }
}