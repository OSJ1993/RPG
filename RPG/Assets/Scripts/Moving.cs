using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;

    //�浹 ���� �� � ���̾�� �浹���� �� �Ǵ�.
    public LayerMask layerMask;


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

    //�ִϸ��̼��� ���������� ����
    private Animator animator;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }


    IEnumerator MoveCoroutine()
    {
        //�ѹ߾��� �ȴ� �ִϸ��̼� ����.
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
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

            //vector.x=1�̸� ���������� �̵��Ѵ� �� �� vector.y = 0 ���� ����ڴ�.
            if (vector.x != 0)
                vector.y = 0;

            //DirX�� ���� �ߴ� ��� ��Ű(-1)�� ���� ������  vector.x�� ����.
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 star1 = transform.position; //A����, ĳ������ ���� ��ġ ��
            Vector2 end = star1 + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);   //B����, ĳ���Ͱ� �̵��ϰ��� �ϴ� ��ġ ��

            // boxCollider2D ���ִ� enabled
            boxCollider2D.enabled = false;
            hit = Physics2D.Linecast(star1, end, layerMask);
            boxCollider2D.enabled = true;

            //hit�� ��ȯ�Ǵ� ���� ������� ���� ��ɾ �����Ű�� �ʰڴ�.
            if (hit.transform != null)
                break;

            //��������
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
