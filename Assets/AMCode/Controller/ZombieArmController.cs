using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieArmController : MonoBehaviour
{
    private Vector3 mousePos;//鼠标位置
    private SpriteRenderer armSR;//手臂贴图
    private Vector2 a2mDirection;//手臂到鼠标的向量。
    private float angA2M;//手臂和鼠标之间的夹角
    private Vector2 armPos;//手臂位置
    private ZombieController zombieController;




    void Start()
    {
        armSR = GetComponent<SpriteRenderer>();
        zombieController = this.transform.parent.GetComponent<ZombieController>();
    }


    void Update()
    {
        //玩家位置减去手臂位置，得出一个连接手臂与玩家的差值向量。
        armPos = this.transform.position;
        a2mDirection = (zombieController.PlayerPos - armPos).normalized;
        //计算鼠标与武器之间的向量的夹角,Atan2  Y/X  计算出弧度并转化为角度
        angA2M = Mathf.Atan2(a2mDirection.y, a2mDirection.x) * Mathf.Rad2Deg;

        //朝向是上方时，需要将手臂改变渲染层数到头部和身体下方。
        if (angA2M >= 45 && angA2M <= 135)
        {
            armSR.sortingOrder = 0;
        }
        else
        {
            armSR.sortingOrder = 3;
        }

        transform.localEulerAngles = new Vector3(transform.position.x, transform.position.y, angA2M);



    }
}
