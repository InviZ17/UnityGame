using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Abilities/DashAbility")]
public class DashAbility : Ability
{
    public float dashSpeed;
    public float maxRange;
    public float maxHitRange;
    public LayerMask collisionLayer;
    public LayerMask playerCollisionLayer;

    private PlayerController pc;
    private float elapsedTime;
    private Vector2 dashVelocity;
    private HashSet<GameObject> hitEnemies;

    public override void Activate(GameObject parent)
    {
        pc = parent.GetComponent<PlayerController>();
        Camera cam = Camera.main;

        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (new Vector2(mousePos.x, mousePos.y) - (Vector2)pc.transform.position).normalized;

        dashVelocity = direction * dashSpeed;
        elapsedTime = 0f;

        hitEnemies = new HashSet<GameObject>();

        pc.OnUpdate += PerformDash;
    }

    private void PerformDash()
    {
        if (elapsedTime < maxRange / dashSpeed)
        {
            float delta = Time.deltaTime;
            float remainingTime = (maxRange / dashSpeed) - elapsedTime;
            delta = Mathf.Min(delta, remainingTime);

            Vector2 movement = dashVelocity * delta;

            // Use raycasts to detect if the player is about to collide with a wall
            RaycastHit2D hitWall = Physics2D.Raycast(pc.transform.position, movement.normalized, movement.magnitude, playerCollisionLayer);
            if (hitWall.collider != null)
            {
                float distance = hitWall.distance - 0.01f;
                movement = movement.normalized * distance;
            }

            // Use raycasts to detect if the player is about to collide with an enemy
            RaycastHit2D hitEnemy = Physics2D.Raycast(pc.transform.position, dashVelocity.normalized, movement.magnitude, collisionLayer);
            if (hitEnemy.collider != null)
            {
                GameObject other = hitEnemy.collider.gameObject;

                // Check if the enemy has not been hit before during the current dash
                if (!hitEnemies.Contains(other))
                {
                    hitEnemies.Add(other); // Add the enemy to the hitEnemies HashSet

                    DamageHandler cs = other.GetComponent<DamageHandler>();
                    cs.TakeDamage(1f,0f,new Vector2());
                    //float health = cs.GetStatValueByName("Health");
                    // if (health <= 1)
                    // {
                    //     other.gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    //     Destroy(other.gameObject, 1f);
                    // }
                    // else
                    // {
                    //     Debug.Log("-1");
                    //     cs.ModifyStatValueByName("Health", -1);
                    // }
                }
            }

            pc.transform.position += (Vector3)movement;

            elapsedTime += delta;
        }
        else
        {
            // Stop updating when the dash is complete
            pc.OnUpdate -= PerformDash;
        }
    }
}
