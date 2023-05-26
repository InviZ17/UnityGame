using UnityEngine;

[System.Serializable]
public class Damage
{
    public float damageAmount;
    public Vector2 knockbackDirection;
    public float knockbackForce;
    public Type damageType;

    public Damage(float damageAmount, Vector2 knockbackDirection, float knockbackForce, Type damageType)
    {
        this.damageAmount = damageAmount;
        this.knockbackDirection = knockbackDirection;
        this.damageType = damageType;
        this.knockbackForce = knockbackForce;
    }

    public enum Type
    {
        Physical,
        Magical,
    }

}

