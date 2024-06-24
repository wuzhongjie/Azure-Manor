using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Boolean Hitting;//攻击中
    private Animator animatorWeapon;
    private Vector3 mousePos;//鼠标位置
    private SpriteRenderer weaponSR;//武器贴图
    private Vector2 w2mDirection;//武器到鼠标的向量。

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        Hitting = false;
        playerController = this.transform.parent.GetComponent<PlayerController>();
        animatorWeapon = GetComponent<Animator>();
        weaponSR = GetComponent<SpriteRenderer>();
    }

    public void hitOver() { Hitting = false; }


    // Update is called once per frame
    void Update()
    {
        //获取鼠标屏幕位置,并转化为游戏坐标
        mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //鼠标位置减去武器位置，得出一个连接武器与鼠标的差值向量。
        w2mDirection = (worldPos - transform.position).normalized;      
        //计算鼠标与武器之间的向量的夹角,Atan2  Y/X  计算出弧度并转化为角度
        float angW2M = Mathf.Atan2(w2mDirection.y, w2mDirection.x) * Mathf.Rad2Deg;

        //朝向是上方时，需要将武器改变渲染层数到头部和身体下方。
        if (angW2M >= 45 && angW2M <= 135)
        {
            weaponSR.sortingOrder = 0;
        }
        else
        {
            weaponSR.sortingOrder = 3;
        }
        //武器朝向鼠标指针
        if (!Hitting)
        {            
            //鼠标在右边时是0度，素材角度是135度。
            transform.localEulerAngles = new Vector3(transform.position.x, transform.position.y, angW2M - 45);
            //Quaternion rotation = Quaternion.AngleAxis(ang, Vector3.forward);
            //this.transform.Find("Body").transform.Find("Weapon").rotation = Quaternion.Slerp(this.transform.Find("Body").transform.Find("Weapon").rotation, rotation,0.001f);
            //this.transform.Find("Body").transform.Find("Weapon").rotation = Quaternion.LookRotation(direction,Vector3.up);
        }

        //武器动画设置
        animatorWeapon.SetFloat("MoveX", playerController.LookDirection.x);
        animatorWeapon.SetFloat("MoveY", playerController.LookDirection.y);
        //按键检测
        if (Input.GetMouseButtonDown(0))
        {
            if (!Hitting)
            {
                Hitting = true;
                //点击鼠标时，快速进行一个武器旋转并归位。
                animatorWeapon.SetTrigger("ClickHit");
            }

        }

    }
}
