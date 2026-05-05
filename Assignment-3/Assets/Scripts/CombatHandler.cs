using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    [SerializeField] private Transform Player;

    private CharacterBattle playerCharacterBattle;
    private CharacterBattle enemyCharacterBattle;

    private void Start()
    {
        playerCharacterBattle = SpawnCharacter(true);
        enemyCharacterBattle = SpawnCharacter(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           playerCharacterBattle.Attack(enemyCharacterBattle); 
        }
    }

    private CharacterBattle SpawnCharacter(bool isPlayerTeam)
    {
        Vector3 position;
        if (isPlayerTeam)
        {
            position = new Vector3(-50, 0);
        } else
        {
            position = new Vector3(50, 0);
        }
        return ;
    }
}
