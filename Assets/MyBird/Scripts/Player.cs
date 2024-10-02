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
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
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
            //����:  �����̽��� �Ǵ� ���콺 ��Ŭ��
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
