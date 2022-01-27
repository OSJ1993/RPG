using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    //ĳ���� �ӵ�
    public float speed;

    private Vector3 vector;

    //����Ʈ ������ �� ��ĭ�� �̵��ϴ� �κ� ����
    private bool applyRunFlag = false;

    //����Ʈ Ű ���� ���� ������.
    public float runSpeed;

    //����Ʈ Ű �� ������ �� ��� �ӵ�.
    private float applyRunSpeed;

    //��ĭ�� �����̰� �ϱ�.
    public int walkCount;
    private int currentWalkCount;


    //�ڷ�ƾ �ݺ� ���� ����
    private bool canMove = true;

    void Start()
    {

    }


    IEnumerator MoveCoroutine()
    {
        //����Ʈ ų ������ �� �ӵ� ������ ����ƮŰ�� ���� �ӵ��� ���� �ӵ�.
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

        // canMove�� true�� ���� ����.
        if (canMove)
        {
            //0�� �ƴ� ��� ����Ű�� ������ �� ����.
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }

        }
    }


}
