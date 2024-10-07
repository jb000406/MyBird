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

        //���� Ÿ�̸�
        [SerializeField] private float spawnTimer = 1.0f;
        private float countdown = 1f;

        [SerializeField] private float maxspawnTimer = 1.05f;
        [SerializeField] private float minspawnTimer = 0.95f;
        public static float levelTimer = 0f;


        //���� ��ġ
        [SerializeField] private float maxSpawnY = 3.5f;
        [SerializeField] private float minSpawnY = -1.5f;

        #endregion
        // Start is called before the first frame update
        private void Start()
        {
            //�ʱ�ȭ
            countdown = 0f;
        }

        // Update is called once per frame
        private void Update()
        {
            //���� ���� üũ
            if (GameManager.isStart == false)
                return;

            //���� Ÿ�̸�
            if(countdown <= 0f) 
            {
                //����
                SpawnPipe();

                //Ÿ�̸� �ʱ�ȭ
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