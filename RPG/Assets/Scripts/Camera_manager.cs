using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_manager : MonoBehaviour
{
    //ī�޶� ���� ���.
    public GameObject target;

    //ī�޶� �󸶳� ���� �ӵ��� ����� ���� ����.
    public float moveSpeed;

    //��� ���� ��ġ ��.
    private Vector3 targetPosition;


    void Start()
    {

    }


    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            //lerp�� A���� B�� ������ ���� �������� �߰� ���� �����Ѵ�.
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        }
    }
}
