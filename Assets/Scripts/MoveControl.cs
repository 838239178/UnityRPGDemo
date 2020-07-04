using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

/// <summary>
/// 玩家移动控制器，按格子移动，采用点碰撞检测
/// </summary>
public class MoveControl : MonoBehaviour
{
    [SerializeField]
    private bool onControl = true;

    public float speed;
    public float step;
    public Animator anim;
    public LayerMask ColliderCheckLayer;

    private SpriteRenderer spRender;
    private float axisRaw = 0f;
    private Vector2 movePoint;

    [SerializeField]
    private PlayerStat playerStat;    
  

    public void SetControl(bool set)
    {
        onControl = set;
    }

    private void Awake()
    {
        playerStat = GameObject.Find("PlayerParty").GetComponent<PlayerStat>();
        this.transform.position = playerStat.scenePosition;
    }

    private void Start()
    {
        spRender = this.GetComponent<SpriteRenderer>();        
        movePoint = this.transform.position;
    }

    private void OnDestroy()
    {
        playerStat.scenePosition = this.transform.position;
        playerStat.sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        axisRaw = 0;
        if (onControl)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, movePoint, speed*Time.deltaTime);
            if (Vector2.Distance(this.transform.position, movePoint) <= .05f)
            {
                anim.SetBool("walk", true);
                if ((axisRaw = Input.GetAxisRaw("Horizontal")) != 0f)
                {
                    movePoint += new Vector2(step * axisRaw, 0);
                    if (axisRaw < 0)
                        spRender.flipX = true;
                    else
                        spRender.flipX = false;
                }
                else if ((axisRaw = Input.GetAxisRaw("Vertical")) != 0f)
                {
                    movePoint += new Vector2(0, step * axisRaw);
                }
                else
                    anim.SetBool("walk", false);
            }
            Collider2D[] temp = new Collider2D[10];
            if(Physics2D.OverlapPointNonAlloc(movePoint, temp, ColliderCheckLayer) > 0)
            {
                movePoint = transform.position;
            }
        }
    }
}
