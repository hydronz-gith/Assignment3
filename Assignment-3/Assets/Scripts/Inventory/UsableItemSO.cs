using NUnit.Framework.Constraints;
using UnityEngine;

[CreateAssetMenu]

public class UsableItemSO : ItemSO, IItemAction

{
    private PlayerHP health;

    public string ActionName => "Use";

    public bool PerformAction(GameObject character)
    {
        health.maxHealth = 10;
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


