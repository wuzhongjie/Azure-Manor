using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float Speed = 1;//速度

    public GameObject playerObject;

    private Vector2 playerPos;//玩家位置
    public Vector2 PlayerPos{ get{ return playerPos; } }
    private Vector2 zombiePos;//僵尸位置
    private Vector2 zom2Player;//僵尸与玩家相对位置向量
    private float distanceZ2P;//角色和怪物之间的距离
    private int zombieState;//怪物行为模式
    private Vector2 ZombieMove;
    //生命值
    public int maxHealth = 5;//最大生命值
    public int currentHealth;//当前生命值
    public int Health { get { return currentHealth; } }
    private Animator aniZombieBody;
    private Animator aniZombieHead;
    private Rigidbody2D zombieRigidBody;//刚体组件




    /* 僵尸行为模式
    0，故障：故障模式
    1，待机：周围没有玩家时待机，计算与玩家距离
    2，销毁：当玩家与僵尸距离太远时，销毁僵尸
    3，跟随：玩家与僵尸距离小于10米时，走向玩家。
    4，攻击：触碰到玩家时，对玩家和自身都进行一个短距离的击退，并进行伤害。
    5，受击：被玩家攻击时，掉血
    6，死亡：血量降低到0，改变碰撞状态，并播放死亡动画。
     */


    void Start()
    {
        GetComponents();
        currentHealth = maxHealth;
    }

    //获取组件
    private void GetComponents()
    {
        zombieRigidBody = GetComponent<Rigidbody2D>();
        aniZombieBody = this.transform.Find("Body").GetComponent<Animator>();
        aniZombieHead = this.transform.Find("Head").GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        //获取玩家位置和自身位置
        playerPos = playerObject.transform.position;
        zombiePos = this.transform.position;
        //获取相对位置向量
        zom2Player = playerPos - zombiePos;
        //计算怪物与玩家之间的距离
        distanceZ2P = zom2Player.sqrMagnitude;
        Debug.Log("距离" + distanceZ2P);
        //设置怪物方向
        ZombieMove = new Vector2(zom2Player.x,zom2Player.y).normalized;
        //设置行为模式
        if (distanceZ2P < 0)
        {
            //小于0，故障。
            zombieState = 0;
        }
        else if (distanceZ2P > 10 && distanceZ2P < 100)
        {
            //距离大于10米小于100米，待机
            zombieState = 1;
        }
        else if (distanceZ2P >= 100)
        {
            //超过100米，销毁
            zombieState = 2;
        }
        else if (distanceZ2P >= 0 && distanceZ2P <= 10)
        {
            //0-10米，跟随
            zombieState = 3;
        }
    }
    void Update()
    {
        //根据状态做出行为
        switch (zombieState)
        {
            case 0:
                break;
            case 1://待机
            Debug.Log("待机");
                break;
            case 2://销毁
            Debug.Log("销毁");
                Destroy(this.gameObject);
                break;
            case 3://跟随
            Debug.Log("跟随");
                Vector2 position = transform.position;
                position += zom2Player.normalized * Speed * Time.deltaTime;
                zombieRigidBody.MovePosition(position);
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            default:
                break;
        }
        //朝向设置（使用normalized可以对向量进行归一化，限制范围在1到-1）
        //mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
        //mouseDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position).normalized;
        //身体动画设置
        aniZombieBody.SetFloat("MoveX", ZombieMove.x);
        aniZombieBody.SetFloat("MoveY", ZombieMove.y);
        aniZombieBody.SetFloat("Speed", ZombieMove.magnitude);
        //头部动画设置
        aniZombieHead.SetFloat("MoveX", ZombieMove.x);
        aniZombieHead.SetFloat("MoveY", ZombieMove.y);

    }

}
