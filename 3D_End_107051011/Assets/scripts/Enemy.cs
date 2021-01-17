using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 30)]
    public float speed = 3;
    [Header("停止距離"), Range(0, 10)]
    public float stopDistance = 2.5f;
    [Header("攻擊間隔"), Range(0, 10)]
    public float cd = 3f;
    [Header("攻擊中心點")]
    public Transform atkpoint;
    [Header("攻擊距離"), Range(0f, 5f)]
    public float atklength;
    [Header("攻擊力"), Range(0, 500)]
    public float atk = 30;


    private Transform Player;
    private NavMeshAgent nav;
    private Animator ani;
    private float timer;

    private void Awake()
    {
        Player = GameObject.Find("Wallen").transform;
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        nav.speed = speed;
        nav.stoppingDistance = stopDistance;
    }
    private void Update()
    {
        Track();
        attack1();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(atkpoint.position, atkpoint.forward * atklength);
    }

    private RaycastHit hit;

    public float hp = 100;

    private void attack1() 
    {
        if (nav.remainingDistance<stopDistance)
        {
            timer+=Time.deltaTime;

            Vector3 pos = Player.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);

            if (timer>=cd)
            {
                ani.SetTrigger("attack");
                timer = 0;

                if(Physics.Raycast(atkpoint.position, atkpoint.forward,out hit, atklength, 1 << 8)) 
                {
                    hit.collider.GetComponent<Player>().hurt(atk);
                }
            }
        }

        
    }
    public void hurt(float hurt)
    {
        hp -= hurt;
        ani.SetTrigger("hit");

        if (hp <= 0) dead();
    }
    private void dead()
    {
        nav.isStopped = true;
        enabled = false;
        ani.SetBool("die",true);
      
    }
    private void Track() 
    {
        nav.SetDestination(Player.position);
        ani.SetBool("run", nav.remainingDistance > stopDistance);
    }

}
