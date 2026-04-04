using System.Collections;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] private GameObject projectilePrefab;

    protected override void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            if (agent.enabled)
                agent.isStopped = true;

            Vector3 lookDir = (player.position - transform.position).normalized;
            lookDir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 5f);
        }
        else
        {
            if (agent.enabled)
                agent.isStopped = false;
        }

        base.Update();
    }

    public override void Attack()
    {
        Vector3 targetPos = player.position + Vector3.up * 1f;
        Vector3 direction = (targetPos - attackPoint.position).normalized;

        GameObject projGO = Instantiate(projectilePrefab, attackPoint.position, Quaternion.LookRotation(direction));

        if (projGO.TryGetComponent(out Projectile proj))
        {
            proj.DamageInfo = new DamageInfo() { DamageAmount = attackDamage, KnockbackForce = direction * 2f };

            if (projGO.TryGetComponent(out Rigidbody rbProj))
            {
                rbProj.linearVelocity = direction * proj.Speed;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
