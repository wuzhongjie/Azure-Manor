using System.Collections;
using System.Collections.Generic;
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
    private Animator animatorWeapon;
    private Vector2 lookDirection = new Vector2(1, 0);//朝向
    private Vector3 respawnPosition;//重生位置

    /* 待实现功能
    1，武器跟随鼠标指针
    2，左键攻击
    3，右键防御
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
        //animatorWeapon = this.transform.Find("Weapon").GetComponent<Animator>();

    }

    private void FixedUpdate() {
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
    void Update(){
       
        //朝向设置
        if (!Mathf.Approximately(playerMove.x, 0) || !Mathf.Approximately(playerMove.y, 0))
        {
            lookDirection.Set(playerMove.x, playerMove.y);
            lookDirection.Normalize();
        }
        else
        {

        }
        //身体动画设置
        animatorBody.SetFloat("MoveX", lookDirection.x);
        animatorBody.SetFloat("MoveY", lookDirection.y);
        animatorBody.SetFloat("Speed", playerMove.magnitude);
        //头部动画设置
        animatorHead.SetFloat("MoveX", lookDirection.x);
        animatorHead.SetFloat("MoveY", lookDirection.y);
        //武器跟随鼠标指针移动
        
    }

    

    private void Respawn()
    {
        transform.position = respawnPosition;
    }
}
