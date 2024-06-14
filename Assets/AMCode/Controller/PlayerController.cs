using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 0;//速度
    public Rigidbody2D PlayerRigidBody ;//刚体组件
    //生命值
    public int maxHealth = 5;//最大生命值
    public int currentHealth;//当前生命值
    public int Health { get { return currentHealth; } }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
