using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{

    public class GroundMove : MonoBehaviour
    {
        #region Variables

        //∂• ¿Ãµø
        [SerializeField] private float moveSpeed = 5f;
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(this.transform.localPosition.x <= -8.4f)
            {
                Debug.Log("¿Ãµø");
                this.transform.localPosition = new Vector3(0f, 0f, 0f);
            }

            MoveGround();
        }

        void MoveGround()
        {
            this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}