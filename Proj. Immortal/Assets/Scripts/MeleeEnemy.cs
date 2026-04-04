using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    public override void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, hitLayers);

        foreach (Collider collider in hitColliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                DamageInfo info = new DamageInfo() { Amount = attackDamage, KnockbackForce = Vector3.zero };
                damageable.TakeDamage(info);
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
