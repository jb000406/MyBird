using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{

    public class CameraController : MonoBehaviour
    {
        public Transform bird;

        [SerializeField] private float offset = 1.5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void LateUpdate()
        {
            FollowPlayer();
        }

        void FollowPlayer()
        {
            transform.position = new Vector3(bird.position.x + offset, transform.position.y, transform.position.z);
        }
    }
    
}