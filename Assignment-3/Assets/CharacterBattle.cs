using UnityEngine;

public class CharacterBattle : MonoBehaviour
{

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void Attack(CharacterBattle targetCharacterBattle)
    {
        Vector3 attackDirection = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
    }
}
