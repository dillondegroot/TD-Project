using UnityEngine;

public class SlowBullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 3f;  
    public float slowMultiplier = 0.5f;  
    public float slowDuration = 5f;  

    private Transform target;

    public void SetTarget(Transform enemyTarget)
    {
        target = enemyTarget;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); 
            return;
        }

       
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        EnemyHP enemyHP = target.GetComponent<EnemyHP>();
        EnemyMovement enemyMovement = target.GetComponent<EnemyMovement>(); 

        if (enemyHP != null)
        {
            enemyHP.TakeDamage(Mathf.RoundToInt(damage));  
        }

        if (enemyMovement != null)
        {
            enemyMovement.ApplySlow(slowMultiplier, slowDuration); 
        }

        Destroy(gameObject); 
    }
}
