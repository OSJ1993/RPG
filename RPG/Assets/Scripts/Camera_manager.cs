using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_manager : MonoBehaviour
{
    //카메라가 따라갈 대상.
    public GameObject target;

    //카메라가 얼마나 빠른 속도로 대상을 쫓을 건지.
    public float moveSpeed;

    //대상에 현재 위치 값.
    private Vector3 targetPosition;


    void Start()
    {

    }


    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            //lerp는 A값과 B값 사이의 선형 보간으로 중간 값을 리턴한다.
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        }
    }
}
