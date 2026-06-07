using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using NUnit.Framework.Internal;

public class CombatHandler : MonoBehaviour
{
    public static CombatHandler Instance { 
        get; 
        set;
        }

        public PlayerHP playerHP;

        [Header("Combat Stats")]
        public int enemyMaxHP = 80;

        [Header("Damage Values")]
        public int winDamage = 25;
        // the damage dealt when player's choice beats the enemy.
        public int tieDamage = 10;
        // the damage dealt when both choices are the same.
        public int loseDamage = 0;
        // the damage dealt when player's choice loses to the enemy.

        [Header("Timing")]
        public float resultDisplayTime = 2f;

        public int EnemyHP
        {
            get;
            set;
        }

        public enum CombatChoice{Attack, Block, Dodge}
        public enum TurnState{Waiting, Resolving, GameOver}
        public TurnState CurrentState
        {
            get;
            set;
        }

        public UnityEvent OnCombatStart;
        public UnityEvent<int, int> OnHealthChanged;
        public UnityEvent<CombatChoice, CombatChoice, string> OnRoundResolved;
        public UnityEvent<bool> OnCombatEnd;

        private static int Resolve(CombatChoice player, CombatChoice enemy)
    {
        if(player == enemy) return 0;

        bool playerWins = (player == CombatChoice.Block && enemy == CombatChoice.Attack) ||
                          (player == CombatChoice.Dodge && enemy == CombatChoice.Block) ||
                          (player == CombatChoice.Attack && enemy == CombatChoice.Dodge);

        if (playerWins == true)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartCombat();
    }
    
    public void StartCombat()
    {
        EnemyHP = enemyMaxHP;
        CurrentState = TurnState.Waiting;

        OnCombatStart?.Invoke();
        //OnHealthChanged?.Invoke(PlayerHP, EnemyHP);
    }

    public void PlayerChoose(CombatChoice playerChoice)
    {
        if(CurrentState != TurnState.Waiting)
        return;

        CurrentState = TurnState.Resolving;
        StartCoroutine(ResolveRound(playerChoice));
    }

    public IEnumerator ResolveRound(CombatChoice playerchoice)
    {

        CombatChoice enemyChoice = EnemyAI.PickChoice();

        int outcome = Resolve(playerchoice, enemyChoice);

        int damageToEnemy = 0;
        string resultText;

        switch(outcome)
        {
            case 1:
                damageToEnemy = winDamage;
                resultText = "You win the round!";
                break;
            case -1:
                playerHP.Reduce(loseDamage);
                resultText = "You lose the round!";
                break;
            default:
                damageToEnemy = tieDamage;
                playerHP.Reduce(tieDamage);
                resultText = "It's a tie!";
                break;
        }

        EnemyHP = Mathf.Max(0, EnemyHP - damageToEnemy);

        //OnHealthChanged?.Invoke(PlayerHP, EnemyHP);
        OnRoundResolved?.Invoke(playerchoice, enemyChoice, resultText);

        yield return new WaitForSeconds(resultDisplayTime);

        if(EnemyHP <= 0)
        {
            CurrentState = TurnState.GameOver;
            bool playerWon = EnemyHP <= 0;
            OnCombatEnd?.Invoke(playerWon);
        }
        else
        {
            CurrentState = TurnState.Waiting;
        }
    }
}
