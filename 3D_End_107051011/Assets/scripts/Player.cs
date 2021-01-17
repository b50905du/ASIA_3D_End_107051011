using UnityEngine;
using Invector.vCharacterController;

public class Player : MonoBehaviour
{
    private float hp = 100;
    private Animator ani;
    private int atkcount;

    private float timer;

    [Header("連擊間隔時間"), Range(0, 3)]
    public float interval = 2;
    [Header("攻擊中心點")]
    public Transform atkpoint;
    [Header("攻擊距離"), Range(0f, 5f)]
    public float atklength;
    [Header("攻擊力"), Range(0, 500)]
    public float atk = 30;


    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(atkpoint.position, atkpoint.forward * atklength);
    }

    private RaycastHit hit;

    private void Attack()
    {
        if (atkcount < 3)
        {
            if (timer < interval)
            {
                timer += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    atkcount++;
                    timer = 0;

                    if(Physics.Raycast(atkpoint.position,atkpoint.forward,out hit, atklength, 1 << 9)) 
                    {
                        hit.collider.GetComponent<Enemy>().hurt(atk);
                    }
                }
                
            }
            else
            {
                atkcount = 0;
                timer = 0;
            }
        }
        if (atkcount == 3) atkcount = 0;
        ani.SetInteger("attack", atkcount);
    }
    public void hurt(float hurt) 
    {
        hp -= hurt;
        ani.SetTrigger("hit");

        if (hp <= 0) dead();
    }
    private void dead() 
    {
        ani.SetTrigger("die");
        vThirdPersonController vt = GetComponent<vThirdPersonController>();
        vt.lockMovement = true;
        vt.lockRotation = true;
    }

}
