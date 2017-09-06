using UnityEngine;

// Projectile controller
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ProjectileControllerScript : MonoBehaviour
{
    // Sender
    private DamagableControllerScript sender = null;

    // Damage
    private float damage = 0.0f;

    // Set values
    public void SetValues(DamagableControllerScript sender, float damage)
    {
        if ((sender != null) && (this.sender == null))
        {
            if (damage < 0.0f)
                damage = 0.0f;
            this.sender = sender;
            this.damage = damage;
        }
    }

    // On collision enter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (sender != null)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                DamagableControllerScript damagable_controller = contact.collider.GetComponent<DamagableControllerScript>();
                if (sender.CanAttack(damagable_controller))
                    damagable_controller.Damage(damage);
            }
        }
        Destroy(gameObject);
    }
}
