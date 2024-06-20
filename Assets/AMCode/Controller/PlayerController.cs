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
    private Animator animatorWeapon;
    private Vector2 lookDirection;//朝向
    private Vector3 mousePos;//鼠标位置
    private Vector3 weaponPos;//武器位置
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
        //武器朝向鼠标指针
        mousePos = Input.mousePosition;//获取鼠标屏幕位置
        weaponPos = this.transform.Find("Body").transform.Find("Weapon").transform.position;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);//转化为游戏坐标
        Vector2 direction = (worldPos - weaponPos).normalized;//计算转向角度,鼠标位置减去武器位置，得出一个差值向量。        
        float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Atan2  Y/X  计算出弧度并转化为角度
        this.transform.Find("Body").transform.Find("Weapon").transform.localEulerAngles = new Vector3(weaponPos.x, weaponPos.y, ang - 135);//鼠标在右边时是0度，素材角度是135度。
        //Quaternion rotation = Quaternion.AngleAxis(ang, Vector3.forward);
        //this.transform.Find("Body").transform.Find("Weapon").rotation = Quaternion.Slerp(this.transform.Find("Body").transform.Find("Weapon").rotation, rotation,0.001f);
        //this.transform.Find("Body").transform.Find("Weapon").rotation = Quaternion.LookRotation(direction,Vector3.up);
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
