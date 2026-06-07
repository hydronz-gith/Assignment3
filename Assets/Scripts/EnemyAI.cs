using UnityEngine;
#if FALSE
public static class EnemyAI
{
    private static readonly CombatHandler.CombatChoice[] choices =
    {
        CombatHandler.CombatChoice.Attack,
        CombatHandler.CombatChoice.Block,
        CombatHandler.CombatChoice.Dodge
    };

    public static CombatHandler.CombatChoice PickChoice()
    {
        return choices[Random.Range(0, choices.Length)];
    }

    public static CombatHandler.CombatChoice PickWeighted(float attackWeight, float blockWeight, float dodgeWeight)
    {
        float total = attackWeight + blockWeight + dodgeWeight;
        float roll = Random.Range(0f, total);
        if(roll < attackWeight) return CombatHandler.CombatChoice.Attack;
        else if(roll < attackWeight + blockWeight) return CombatHandler.CombatChoice.Block;
        else return CombatHandler.CombatChoice.Dodge;
    }
}
#endif