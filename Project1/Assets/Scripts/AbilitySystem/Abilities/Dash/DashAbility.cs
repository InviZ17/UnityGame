using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public override void Activate(GameObject parent)
    {
        PlayerController pc = parent.GetComponent<PlayerController>();
        Camera cam = Camera.main;

        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (new Vector2(mousePos.x, mousePos.y) - (Vector2) pc.transform.position).normalized;

        Vector2 dashVelocity = direction * dashSpeed;
        Vector2 movement = Vector2.ClampMagnitude(dashVelocity, maxRange);

        CoroutineRunner.instance.StartCoroutine(PerformDash(pc, movement));
    }

    private IEnumerator PerformDash(PlayerController pc, Vector2 dashVelocity)
    {
        Vector2 startingPosition = pc.transform.position;

        HashSet<GameObject> hitEnemies = new HashSet<GameObject>(); // Store hit enemies

        float elapsedTime = 0f;
        while (elapsedTime < maxRange / dashSpeed)
        {
            float delta = Time.deltaTime;
            float remainingTime = (maxRange / dashSpeed) - elapsedTime;
            delta = Mathf.Min(delta, remainingTime);

            Vector2 movement = dashVelocity * delta;

            // Use overlap checks to detect if the player is about to collide with a wall
            Collider2D[] colliders = Physics2D.OverlapBoxAll(startingPosition + movement,
                pc.GetComponent<BoxCollider2D>().size, 0f, playerCollisionLayer);
            if (colliders.Length > 0)
            {
                // The player is about to collide with a wall, so adjust the movement vector to move just outside the collider
                Vector2 adjustedMovement = Vector2.zero;
                foreach (Collider2D collider in colliders)
                {
                    Vector2 closestPoint = collider.ClosestPoint(startingPosition);
                    Vector2 direction = (closestPoint - startingPosition).normalized;
                    RaycastHit2D hit = Physics2D.Raycast(startingPosition, direction, movement.magnitude,
                        playerCollisionLayer);
                    if (hit.collider == collider)
                    {
                        // The player is going to collide with this collider, so adjust the movement vector
                        float distance = hit.distance;
                        Vector2 adjustment = direction * (distance - 0.01f);
                        adjustedMovement += adjustment;
                    }
                }

                movement = adjustedMovement;
            }

            // Use raycasts to detect if the player is about to collide with an enemy
            RaycastHit2D hitEnemy = Physics2D.Raycast(pc.transform.position, dashVelocity.normalized,
                movement.magnitude, collisionLayer);
            if (hitEnemy.collider != null)
            {
                GameObject other = hitEnemy.collider.gameObject;

                // Check if the enemy has not been hit before during the current dash
                if (!hitEnemies.Contains(other))
                {
                    hitEnemies.Add(other); // Add the enemy to the hitEnemies HashSet

                    CharacterStats cs = other.GetComponent<CharacterStats>();
                    float health = cs.GetStatValueByName("Health");
                    if (health <= 1)
                    {
                        other.gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        Destroy(other.gameObject, 1f);
                    }
                    else
                    {
                        Debug.Log("-1");
                        cs.ModifyStatValueByName("Health", -1);
                    }
                }
            }

            pc.transform.position += (Vector3) movement;

            elapsedTime += delta;

            yield return null;
        }
    }
}
