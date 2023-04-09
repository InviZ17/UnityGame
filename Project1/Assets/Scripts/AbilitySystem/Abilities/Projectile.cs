using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float maxRange;
    public LayerMask collisionLayer;

    public override void Activate(GameObject parent)
    {
        PlayerController pc = parent.GetComponent<PlayerController>();
        Camera cam = Camera.main;

        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (new Vector2(mousePos.x, mousePos.y) - (Vector2)pc.transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, pc.transform.position, Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        CoroutineRunner.instance.StartCoroutine(DestroyProjectile(projectile));
    }

    private IEnumerator DestroyProjectile(GameObject projectile)
    {
        float elapsedTime = 0f;

        while (elapsedTime <= maxRange / projectileSpeed)
        {
            RaycastHit2D hit = Physics2D.Raycast(projectile.transform.position, projectile.GetComponent<Rigidbody2D>().velocity.normalized, Time.deltaTime * projectileSpeed, collisionLayer);

            if (hit.collider != null)
            {
                GameObject other = hit.collider.gameObject;

                AbstractStats cs = other.GetComponent<AbstractStats>();
                if (cs != null)
                {
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

                Destroy(projectile);
                yield break;
            }

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(projectile);
    }
}
