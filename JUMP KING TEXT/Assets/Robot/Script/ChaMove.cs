using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaMove : MonoBehaviour
{
    public float speed = 3;
    public float turnSpeed = 10;

    Animator anim;
    Rigidbody rigid;

    Vector3 move;

    float forwardAmount;// 前进量
    float turnAmount;//转向量
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //世界坐标系Move向量
        move = new Vector3(x, 0, z);
        Vector3 localMove = transform.InverseTransformVector(move);

        forwardAmount = localMove.z;
        turnAmount = Mathf.Atan2(localMove.x, localMove.z);

        if (Input.GetButton("Fire3"))
        {
            forwardAmount *= 0.3f;
        }


         
        UpdateAnim();
    }
     
    private void FixedUpdate()
    {
        rigid.velocity = forwardAmount * transform.forward * speed;//刚体前进
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(0, turnAmount * turnSpeed, 0));
    }
    void UpdateAnim()
    {
        //anim.SetFloat("Speed", move.magnitude);
        anim.SetFloat("Forward", forwardAmount);//动画状态机
        anim.SetFloat("Turn", turnAmount);
    }

}
