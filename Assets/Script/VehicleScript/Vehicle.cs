using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    public float speed = 10.0f;

    //가상 메소드 : 이동
    public virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    //추상 메소드 :경적
    public abstract void Horn();
    

         
}
