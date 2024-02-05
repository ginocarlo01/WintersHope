using System.Collections.Generic;

public static class TypeUtility
{
    // Enum for projectile types
    public enum Type
    {
        Neutral,
        Fire,
        Plant,
        Water
    }

    public enum ObjectFromPoolTag
    {
        NewProjectile,
        FollowPlayerProjectile,
        FollowCursorProjectile,
        AddHealth
    }

    // Dictionary to define advantages and disadvantages
    public static Dictionary<Type, List<Type>> TypeAdvantages = new Dictionary<Type, List<Type>>()
    {
        { Type.Fire, new List<Type> { Type.Plant } },
        { Type.Plant, new List<Type> { Type.Water } },
        { Type.Water, new List<Type> { Type.Fire } }
        // Add neutral type relationships if any
    };

    // Dictionary to define invincibility relationships
    public static Dictionary<Type, List<Type>> InvincibleAgainst = new Dictionary<Type, List<Type>>()
    {
        { Type.Fire, new List<Type> { } },
        { Type.Plant, new List<Type> { } },
        { Type.Water, new List<Type> { Type.Fire } }
        // Add neutral type relationships if any
    };

    // Function to check if the attacker has an advantage over the defender
    public static bool HasAdvantage(Type attacker, Type defender)
    {
        return TypeAdvantages.ContainsKey(attacker) && TypeAdvantages[attacker].Contains(defender);
    }

    // Function to check if the attacker is invincible against the defender
    public static bool IsInvincible(Type attacker, Type defender)
    {
        return InvincibleAgainst.ContainsKey(defender) && InvincibleAgainst[defender].Contains(attacker);
    }
}
