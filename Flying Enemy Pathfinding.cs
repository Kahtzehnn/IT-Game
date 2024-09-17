using UnityEngine;

public class FEnemyPath : MonoBehaviour
{

    public bool chasing;
    private float speed = 8f;

    private Transform KnownTransform;
    public Transform Goal;

    private Rigidbody2D body;
    private EnemyStats stats;

    void Awake(){
        body = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();
    }

    void FixedUpdate(){
        if (stats.Health > 0){
            FollowPlayer();
        } else {
            chasing = false;
            body.gravityScale = 2f;
        }
    }

    void FollowPlayer(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (Goal.transform.position - transform.position).normalized, 50f, LayerMask.GetMask("Environment", "Player"));

        if (hit.collider.CompareTag("Player")){
            KnownTransform = hit.collider.transform;
            chasing = true;
        } else {
            chasing = false;
        }

        if (chasing == true){
            transform.position = Vector2.MoveTowards(transform.position, KnownTransform.position, speed * Time.deltaTime);
        }
    }
    
}
