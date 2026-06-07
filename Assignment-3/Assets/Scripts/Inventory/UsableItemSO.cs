using NUnit.Framework.Constraints;
using UnityEngine;

[CreateAssetMenu]

public class UsableItemSO : ItemSO, IItemAction

{
    private CombatHandler health;

    public string ActionName => "Use";

    public bool PerformAction(GameObject character)
    {
        health.playerMaxHP = 10;
        return true;
    }
}

    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        public string ActionName { get; }
        bool PerformAction(GameObject character);
    }


