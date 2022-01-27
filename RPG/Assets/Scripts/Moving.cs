using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    //캐릭터 속도
    public float speed;

    private Vector3 vector;

    //쉬프트 눌렀을 때 두칸씩 이동하는 부분 제어
    private bool applyRunFlag = false;

    //쉬프트 키 누를 때만 빠르게.
    public float runSpeed;

    //쉬프트 키 안 눌렀을 때 평소 속도.
    private float applyRunSpeed;

    //한칸씩 움직이게 하기.
    public int walkCount;
    private int currentWalkCount;


    //코루틴 반복 실행 방지
    private bool canMove = true;

    void Start()
    {

    }


    IEnumerator MoveCoroutine()
    {
        //쉬프트 킬 눌렀을 때 속도 빠르게 쉬프트키를 떼면 속도는 원래 속도.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            applyRunSpeed = runSpeed;
            applyRunFlag = true;
        }
        else
        {
            applyRunSpeed = 0;
            applyRunFlag = false;
        }


        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);



        while (currentWalkCount < walkCount)
        {
            if (vector.x != 0)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0); ;
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
            }
            if (applyRunFlag)
            {
                currentWalkCount++;
            }
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }

        currentWalkCount = 0;


        canMove = true;
    }

    void Update()
    {

        // canMove가 true일 때만 실행.
        if (canMove)
        {
            //0이 아닐 경우 방향키를 눌렀을 때 실행.
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }

        }
    }


}
