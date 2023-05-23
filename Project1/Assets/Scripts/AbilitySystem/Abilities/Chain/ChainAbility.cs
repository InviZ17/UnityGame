using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Abilities/ChainAbility")]
public class ChainAbility : Ability
{
    public GameObject chainPrefab;
    public float maxRange;
    public LayerMask collisionLayer;
    public float damage;

    public int maxBounces;
    public float rangePerBounce;
    public float damagePerBounce;
    public float delayBetweenBounces;

    public override void Activate(GameObject parent)
    {
        PlayerController pc = parent.GetComponent<PlayerController>();
        Camera cam = Camera.main;

        Vector3 mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // Vector2 direction = (new Vector2(mousePos.x, mousePos.y) - (Vector2) pc.transform.position).normalized;

        // Instantiate the chainPrefab
        GameObject chainInstance = Instantiate(chainPrefab, pc.transform.position, Quaternion.identity);

        // Initialize the chain lightning component
        ChainLightningController chainLightningController = chainInstance.GetComponent<ChainLightningController>();
        chainLightningController.Initialize(this, pc.transform);
    }

    public Transform FindNextTarget(Transform currentTarget, float range, LayerMask layerMask, HashSet<Transform> hitTargets)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(currentTarget.position, range, layerMask);
        float minDistance = float.MaxValue;
        Transform closestTarget = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.transform != currentTarget && !hitTargets.Contains(collider.transform))
            {
                float distance = Vector2.Distance(currentTarget.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = collider.transform;
                }
            }
        }

        return closestTarget;
    }

}