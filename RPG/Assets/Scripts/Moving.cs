using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;

    //충돌 했을 때 어떤 레이어와 충돌했을 때 판단.
    public LayerMask layerMask;


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

    //애니메이션을 직접적으로 제어
    private Animator animator;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }


    IEnumerator MoveCoroutine()
    {
        //한발씩만 걷는 애니메이션 제어.
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
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

            //vector.x=1이면 오른쪽으로 이동한다 그 때 vector.y = 0 으로 만들겠다.
            if (vector.x != 0)
                vector.y = 0;

            //DirX에 선언 했던 경우 좌키(-1)를 전달 받은걸  vector.x로 실행.
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 star1 = transform.position; //A지점, 캐릭터의 현재 위치 값
            Vector2 end = star1 + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);   //B지점, 캐릭터가 이동하고자 하는 위치 값

            // boxCollider2D 꺼주는 enabled
            boxCollider2D.enabled = false;
            hit = Physics2D.Linecast(star1, end, layerMask);
            boxCollider2D.enabled = true;

            //hit에 반환되는 값이 있을경우 이후 명령어를 실행시키지 않겠다.
            if (hit.transform != null)
                break;

            //상태전이
            animator.SetBool("Walking", true);

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


        }
        animator.SetBool("Walking", false);
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
