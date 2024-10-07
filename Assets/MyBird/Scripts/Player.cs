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

        //버드 에니메이션
        Animator birdAnimtor;

        //게임 UI
        public GameObject readyUI;
        public GameObject gameoverUI;

        //게임 사운드
        private AudioSource audioSource;



        #endregion

        // Start is called before the first frame update
        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            birdAnimtor = GetComponent<Animator>();
            birdAnimtor.enabled = false;
            readyUI.SetActive(true);
            gameoverUI.SetActive(false);
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.isDeath == true)
            {
                return;
            }
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
            if (GameManager.isDeath)
                return;

#if UNITY_EDITOR
            //점프:  스페이스바 또는 마우스 왼클릭
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);
#else
            //터치 인풋 처리
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    keyJump |= true;
                }
            }
#endif


            if (GameManager.isStart == false  && keyJump)
            {
                MoveStartBird();
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
            if (GameManager.isStart == false || GameManager.isDeath == true)
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

        //버드 죽기
        void DeathBird()
        {
            if(GameManager.isDeath == true)
            {
                return ;
            }
            Debug.Log("죽음 처리");
            GameManager.isDeath = true;


            birdAnimtor.enabled = true;
            birdAnimtor.SetBool("IsDeath", true);

            gameoverUI.SetActive(true);


        }

        //점수 획득
        void GetPoint()
        {
            if (GameManager.isDeath == true)
            {
                return;
            }

            //Debug.Log("점수 획득 처리");
            GameManager.Score++;

            //점수 획득 시 사운드 플레이
            audioSource.Play();

            //기둥 10개를 통과할때마다 - 난이도 상승
            if(GameManager.Score % 10 == 0)
            {
                SpawnManager.levelTimer += 0.05f;
            }
        }

        //이동 시작
        void MoveStartBird()
        {
            GameManager.isStart = true;
            readyUI.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.tag == "Pipe")
            {
                DeathBird();
            }
            else if(collider.tag == "Point")
            {
                GetPoint();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                Debug.Log("그라운드 충돌 - 죽는다");
                DeathBird();
            }
        }
    }
}
