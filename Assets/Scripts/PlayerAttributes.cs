using UnityEngine;

public abstract class PlayerAttributes : ScriptableObject
{
    public abstract void AffectCharacter(GameObject character, float val);
}
