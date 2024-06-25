using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 0;//速度

    private Vector2 playerMove;
    private Rigidbody2D playerRigidBody;//刚体组件
    //生命值
    public int maxHealth = 5;//最大生命值
    public int currentHealth;//当前生命值
    public int Health { get { return currentHealth; } }
    private Animator animatorBody;
    private Animator animatorHead;
    private Vector2 lookDirection;//键盘朝向
    private Vector2 mouseDirection;//鼠标朝向
    public Vector2 LookDirection { get { return lookDirection; } }
    public Vector2 MouseDirection { get { return mouseDirection; }}
    private Vector3 respawnPosition;//重生位置




    /* 待实现功能
    1，普通攻击剑气
    2，右键防御
    3，敌人
     */


    void Start()
    {
        GetComponents();
        currentHealth = maxHealth;
        respawnPosition = transform.position;
    }

    //获取组件
    private void GetComponents()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animatorBody = this.transform.Find("Body").GetComponent<Animator>();
        animatorHead = this.transform.Find("Head").GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        //获取玩家操作
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //移动向量
        playerMove = new Vector2(horizontal, vertical);
        //角色移动
        Vector2 position = transform.position;
        position += playerMove * Speed * Time.deltaTime;
        playerRigidBody.MovePosition(position);
    }
    void Update()
    {
        //朝向设置（使用normalized可以对向量进行归一化，限制范围在1到-1）
        //mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        mouseDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position).normalized;
        //身体动画设置
        animatorBody.SetFloat("MoveX", mouseDirection.x);
        animatorBody.SetFloat("MoveY", mouseDirection.y);
        animatorBody.SetFloat("Speed", playerMove.magnitude);
        //头部动画设置
        animatorHead.SetFloat("MoveX", mouseDirection.x);
        animatorHead.SetFloat("MoveY", mouseDirection.y);
        
    }

    private void Respawn()
    {
        transform.position = respawnPosition;
    }
}
