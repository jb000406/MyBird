using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{

    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //점프
        [SerializeField] private float jumpForce = 250f;
        private bool keyJump = false;                   //점프 키입력 체크

        public GameObject bird;

        private float maxRotateAnger = 30;
        private float minRotateAnger = -90;
        // 회전
        private Vector3 birdRotation;
        [SerializeField] private float rorateSpeed = 0.5f;

        //이동
        [SerializeField] private float moveSpeed = 5f;

        //대기 
        [SerializeField] private float readyForce = 1f;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            //버드 대기
            ReadyBird();
            

            //키입력
            InputBird();

            //버드 회전
            RotateBird();

            //버드 이동
            BirdMove();
        }

        private void FixedUpdate()
        {
            //점프
            if (keyJump)
            {
                Debug.Log("점프!");
                JumpBird();
                keyJump = false;
            }
        }

        //버드 점프
        void JumpBird()
        {


            //위로 힘을 주어 위로 이동
            //rb2D.AddForce(Vector3.up * jumpForce,ForceMode2D.Force);
            rb2D.velocity = Vector3.up * jumpForce;

        }

        //컨트롤 입력
        void InputBird()
        {
            //점프:  스페이스바 또는 마우스 왼클릭
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);

            if(GameManager.isStart == false  && keyJump)
            {
                GameManager.isStart = true;
            }
        }

        void RotateBird()
        {
            float degree = 0f;
            if(rb2D.velocity.y > 0f)
            {
                degree = rorateSpeed;
            }
            else
            {
                degree = -rorateSpeed;
            }

            float rotationZ = Mathf.Clamp(birdRotation.z + degree, minRotateAnger, maxRotateAnger);
            birdRotation = new Vector3(0f, 0f, rotationZ);
            transform.eulerAngles = birdRotation;

            /*float changeVelocity = Mathf.Clamp(rb2D.velocity.y, -10f, 5.8f);

            float birdRotation = Mathf.Lerp(-90f, 30f, Mathf.InverseLerp(-10f, 5.8f, changeVelocity));

            transform.rotation = Quaternion.Euler(0, 0, birdRotation);*/
        }

        void BirdMove()
        {
            if (GameManager.isStart == false)
            {
                return;
            }
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        }

        void ReadyBird()
        {

            if (GameManager.isStart)
                return;
                
            rb2D.velocity = Vector2.up * readyForce;
            
        }

    }
}
