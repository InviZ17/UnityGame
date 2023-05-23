using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChainLightningController : MonoBehaviour
{
    public ChainAbility chainAbility;
    public Transform start;
    private LineRenderer lineRenderer;
    private PlayerController pc;

    private int bounceCount;
    private Transform currentTarget;
    private float timer;
    private HashSet<Transform> hitTargets;

    void Start()
    {
        // Find the LineRenderer component in the UI Canvas
        lineRenderer = GameObject.Find("LineRendererContainer").GetComponent<LineRenderer>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        // Set LineRenderer properties if needed
        lineRenderer.positionCount = 0;

        // Initialize the HashSet for hit targets
        hitTargets = new HashSet<Transform>();
    }

    public void Initialize(ChainAbility chainAbility, Transform start)
    {
        this.chainAbility = chainAbility;
        this.start = start;
        this.bounceCount = 0;
        this.currentTarget = start;
        this.timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= chainAbility.delayBetweenBounces && bounceCount < chainAbility.maxBounces)
        {
            timer = 0;
            if (currentTarget)
            {
                Transform nextTarget = chainAbility.FindNextTarget(currentTarget, chainAbility.rangePerBounce, chainAbility.collisionLayer, hitTargets);
                if (nextTarget != null)
                {
                    // Add the target to the hitTargets HashSet
                    hitTargets.Add(nextTarget);

                    // Deal damage to the target
                    Damageable d = nextTarget.GetComponent<Damageable>();

                    Vector2 direction = (nextTarget.transform.position - pc.transform.position).normalized;

                    Debug.Log(direction);
                    Damage dmg = new Damage(chainAbility.damage, direction, 0.5f, Damage.Type.Magical);
                    d.TakeDamage(dmg);
                    

                    // Update the LineRenderer
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, currentTarget.position);
                    lineRenderer.SetPosition(1, nextTarget.position);

                    // Move to the next target
                    currentTarget = nextTarget;
                    bounceCount++;
                }
                else
                {
                    // Clear the LineRenderer if there are no valid targets left
                    lineRenderer.positionCount = 0;

                    // Destroy the ChainLightningController GameObject
                    Destroy(gameObject);
                }
            }
            else
            {
                // Find a new target if the current target is destroyed
                Transform newTarget = chainAbility.FindNextTarget(start, chainAbility.rangePerBounce, chainAbility.collisionLayer, hitTargets);
                if (newTarget != null)
                {
                    currentTarget = newTarget;
                }
                else
                {
                    // Clear the LineRenderer if there are no valid targets left
                    lineRenderer.positionCount = 0;

                    // Destroy the ChainLightningController GameObject
                    Destroy(gameObject);
                }
            }
        }
    }

}
