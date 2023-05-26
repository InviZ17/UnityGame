using System.Collections;
using UnityEngine;

/// <summary>
/// A Damageable component that allows a GameObject with Stats to take damage.
/// </summary>
[RequireComponent(typeof(Stats))]
public class Damageable : MonoBehaviour
{
    private Stats _characterStats;
    private Immune _immune;
    private SpriteRenderer _spriteRenderer;
    private Damage _damageInfo;

    public float redFilterDuration = 0.25f;

    public Stats GetStats()
    {
        return _characterStats;
    }

    /// <summary>
    /// Initializes the Damageable component, acquiring the Stats component and setting default immunity.
    /// </summary>
    private void Awake()
    {
        _characterStats = GetComponent<Stats>();
        _immune = Immune.None;
        _spriteRenderer = transform.gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Returns the current immune status of the GameObject.
    /// </summary>
    /// <returns>The immune status as an Immune enum.</returns>
    public Immune GetImmuneStatus()
    {
        return _immune;
    }

    /// <summary>
    /// Applies damage to the GameObject, reducing its Health stat.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to apply.</param>
    public void TakeDamage(Damage damageInfo)
    {
        _damageInfo = damageInfo;

        if (_immune != Immune.Magical && damageInfo.damageType == Damage.Type.Magical || _immune != Immune.Physical && damageInfo.damageType == Damage.Type.Physical)
        {
            float currentHealth = _characterStats.GetStatValueByName("Health");
            float newHealth = Mathf.Max(currentHealth - damageInfo.damageAmount, 0);
            _characterStats.SetStatValueByName("Health", newHealth);

            if (newHealth <= 0)
            {
                Die();
            }
            else
            {
                ApplyKnockback();
                StartCoroutine(ApplyRedFilter());
            }
        }
    }

    private IEnumerator ApplyRedFilter()
    {
        Color originalColor = _spriteRenderer.color;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(redFilterDuration);
        _spriteRenderer.color = originalColor;
    }

    private void ApplyKnockback()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(_damageInfo.knockbackDirection * _damageInfo.knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(KnockCoroutine(rb));
        }
    }

    private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(0.2f);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
    }

    /// <summary>
    /// Destroys the GameObject when its Health reaches 0 or less.
    /// </summary>
    private void Die()
    {
        Animator anim = transform.parent.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("isDead", true);
        }
        Destroy(transform.gameObject);
    }
}

/// <summary>
/// An enumeration representing different types of immunity for GameObjects.
/// </summary>
public enum Immune
{
    None,
    Magical,
    Physical
}