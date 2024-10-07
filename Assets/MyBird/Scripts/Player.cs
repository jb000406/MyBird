using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{

    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //����
        [SerializeField] private float jumpForce = 250f;
        private bool keyJump = false;                   //���� Ű�Է� üũ

        public GameObject bird;

        private float maxRotateAnger = 30;
        private float minRotateAnger = -90;
        // ȸ��
        private Vector3 birdRotation;
        [SerializeField] private float rorateSpeed = 0.5f;

        //�̵�
        [SerializeField] private float moveSpeed = 5f;

        //��� 
        [SerializeField] private float readyForce = 1f;

        //���� ���ϸ��̼�
        Animator birdAnimtor;

        //���� UI
        public GameObject readyUI;
        public GameObject gameoverUI;

        //���� ����
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
            //���� ���
            ReadyBird();
            

            //Ű�Է�
            InputBird();

            //���� ȸ��
            RotateBird();

            //���� �̵�
            BirdMove();
        }

        private void FixedUpdate()
        {
            //����
            if (keyJump)
            {
                Debug.Log("����!");
                JumpBird();
                keyJump = false;
            }
        }

        //���� ����
        void JumpBird()
        {


            //���� ���� �־� ���� �̵�
            //rb2D.AddForce(Vector3.up * jumpForce,ForceMode2D.Force);
            rb2D.velocity = Vector3.up * jumpForce;

        }

        //��Ʈ�� �Է�
        void InputBird()
        {
            if (GameManager.isDeath)
                return;

#if UNITY_EDITOR
            //����:  �����̽��� �Ǵ� ���콺 ��Ŭ��
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);
#else
            //��ġ ��ǲ ó��
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

        //���� �ױ�
        void DeathBird()
        {
            if(GameManager.isDeath == true)
            {
                return ;
            }
            Debug.Log("���� ó��");
            GameManager.isDeath = true;


            birdAnimtor.enabled = true;
            birdAnimtor.SetBool("IsDeath", true);

            gameoverUI.SetActive(true);


        }

        //���� ȹ��
        void GetPoint()
        {
            if (GameManager.isDeath == true)
            {
                return;
            }

            //Debug.Log("���� ȹ�� ó��");
            GameManager.Score++;

            //���� ȹ�� �� ���� �÷���
            audioSource.Play();

            //��� 10���� ����Ҷ����� - ���̵� ���
            if(GameManager.Score % 10 == 0)
            {
                SpawnManager.levelTimer += 0.05f;
            }
        }

        //�̵� ����
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
                Debug.Log("�׶��� �浹 - �״´�");
                DeathBird();
            }
        }
    }
}
