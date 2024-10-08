using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    public float speed = 10.0f;

    //���� �޼ҵ� : �̵�
    public virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    //�߻� �޼ҵ� :����
    public abstract void Horn();
    

         
}
