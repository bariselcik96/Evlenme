using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints; // Waypoint'ler için array
    private Transform player; // Oyuncu referansı
    public float patrolSpeed = 2f; // Devriye hız
    public float chaseSpeed = 4f; // Takip hızı
    public float detectionRadius = 5f; // Oyuncuyu algılama yarıçapı
    public float attackDistance = 1f; // Saldırı mesafesi
    public float attackRate = 1f; // Saldırı sıklığı

    private int currentWaypointIndex;
    private bool isChasing;
    private float attackCooldown;
    private Animator anim;
    private static readonly int CanWalk = Animator.StringToHash("canWalk");
    private static readonly int Attack1= Animator.StringToHash("Attack");

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!player)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        // Saldırı işlevi
        if (Vector3.Distance(transform.position, player.position) <= attackDistance)
        {
            isChasing = false;
            anim.SetBool(CanWalk, false);
            if (attackCooldown <= 0f)
            {
                StartCoroutine(Attack());
                attackCooldown = attackRate;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
            }
        }
        else if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            isChasing = true;
        }
        

        if (isChasing)
        {
            // Oyuncunun üzerine doğru hareket et
            ChasePlayer();
        }
        else if (Vector3.Distance(transform.position, player.position) > detectionRadius)
        {
            // Waypoint'ler arasında hareket et
            PatrolWaypoints();
        }

    }

    void PatrolWaypoints()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.LookAt(targetWaypoint.position);
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, patrolSpeed * Time.deltaTime);
        anim.SetBool(CanWalk,true);
        anim.SetBool(Attack1,false);

        
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.3f)
        {
            currentWaypointIndex = Random.Range(0,waypoints.Length - 1);
        }
    }

    private void ChasePlayer()
    {
        transform.LookAt(player.position);
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        anim.SetBool(CanWalk,true);
        anim.SetBool(Attack1,false);
    }
    private IEnumerator Attack()
    {
        transform.LookAt(player.position);
        anim.SetBool(Attack1,true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool(Attack1,false);
    }
}
