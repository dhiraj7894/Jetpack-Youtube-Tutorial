using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obs : MonoBehaviour
{
    
    public float speed = -3;

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        if (transform.position.x <= -15)
        {
            Destroy(this.gameObject);
        }
    }    
}
