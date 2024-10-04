using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{

    public class PipeKiller : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Point")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}