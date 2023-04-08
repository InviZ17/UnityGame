using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Abilities/DashAbility")]
public class DashAbility : Ability
{
    public float dashSpeed;
    public float maxClampMagnitude;
    public float maxRange;
    public float maxHitRange;
    public LayerMask collisionLayer;

    public override void Activate(GameObject parent)
    {
        PlayerController pc = parent.GetComponent<PlayerController>();
        Camera cam = Camera.main;

        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (new Vector2(mousePos.x, mousePos.y) - (Vector2)pc.transform.position).normalized;

        Vector2 movement = Vector2.ClampMagnitude(direction, maxClampMagnitude);

        CoroutineRunner.instance.StartCoroutine(PerformDash(pc, movement));
    }

    private IEnumerator PerformDash(PlayerController pc, Vector2 dashDirection)
    {
        float duration = maxRange / dashSpeed;

        float elapsedTime = 0f;
        Vector3 startingPosition = pc.transform.position;
        Vector3 targetPosition = startingPosition + (Vector3)dashDirection * maxRange;

        HashSet<GameObject> hitEnemies = new HashSet<GameObject>(); // Store hit enemies

        while (elapsedTime <= duration)
        {
            RaycastHit2D hit = Physics2D.Raycast(pc.transform.position, dashDirection, maxHitRange, collisionLayer);

            if (hit.collider != null)
            {
                GameObject other = hit.collider.gameObject;

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

            pc.transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        pc.transform.position = targetPosition;
    }
}
