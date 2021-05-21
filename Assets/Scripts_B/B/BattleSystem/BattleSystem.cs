using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum BattleState
{
    START, PLAYERTURN, ENEMYTURN,WON,LOST
}
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    //THe transform position of where to spawn the sprites
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TMP_Text dialogueText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    
    public BattleState state;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());

    }

    IEnumerator SetUpBattle()
    {
        //Set up the UI
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "And so the battle begins ";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        bool isDead;
        
        if (enemyUnit.blocking == false)
        {
            isDead = enemyUnit.TakeDamage(playerUnit.damage);
            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "Your blow has landed" + playerUnit.unitName;

        }
        else
        {
            isDead = enemyUnit.isBlocking(playerUnit.damage, enemyUnit.block);
            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "your blow has been blocked" + playerUnit.unitName;
            enemyUnit.blocking = false;
        }

        yield return new WaitForSeconds(2f);

        //Check if the enemy is dead 
        //if dead change state 
        if (isDead)
        {
            //End the battle 
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //EnemyTurn
            state = BattleState.ENEMYTURN;
            EnemyChance();
           
        }
    }
    IEnumerator EnemyBlocking()
    {
        enemyUnit.blocking = true;
        dialogueText.text = enemyUnit.unitName+" is blocking " ;
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    IEnumerator PlayerBlocking()
    {
        playerUnit.blocking = true;
        dialogueText.text = playerUnit.unitName + " is blocking ";
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        EnemyChance();
    }
    IEnumerator EnemyTurn()
    {
        bool isDead;
        EnemyChance();
        if (playerUnit.blocking == false)
        {
            isDead = playerUnit.TakeDamage(enemyUnit.damage);
            playerHUD.SetHP(playerUnit.currentHP);
            dialogueText.text = "Your blow has landed" + enemyUnit.unitName;

        }
        else
        {
            isDead = playerUnit.isBlocking(enemyUnit.damage, playerUnit.block);
            playerHUD.SetHP(playerUnit.currentHP);
            dialogueText.text = "your blow has been blocked" + enemyUnit.unitName;
            playerUnit.blocking = false;
        }

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            //End the battle 
            state = BattleState.WON;
            EndBattle();

        }
        else
        {
            //Player Turn
            state = BattleState.PLAYERTURN;
            PlayerTurn();

        }

    }

    private void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Skorn";

        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "To Valhalla with you";
        }
    }

    private void PlayerTurn()
    {
        dialogueText.text = playerUnit.unitName +" Choose an Action :";
    }
    private void EnemyChance()
    {
        dialogueText.text = enemyUnit.unitName + " Choose an Action :";
    }

    public void OnAttackButton()
    {
        //check if its the players turn
        if (state != BattleState.PLAYERTURN)
            StartCoroutine(EnemyTurn());
        else
            StartCoroutine(PlayerAttack());



    }

      public void OnHealButton()
    {
        //check if its the players turn
        if (state != BattleState.PLAYERTURN)
            StartCoroutine(EnemyHeal());
        else
            StartCoroutine(PlayerHeal());


    }

    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
            StartCoroutine(EnemyBlocking());
        else
            StartCoroutine(PlayerBlocking());

    }
    IEnumerator EnemyHeal()
    {
        enemyUnit.Heal(5);
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = enemyUnit.unitName + "You've been healed";

        //if (state == BattleState.PLAYERTURN)
        //    playerUnit.Heal(5);
        //else if (state == BattleState.ENEMYTURN)
        //    enemyUnit.Heal(5);

        //action before 
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = playerUnit.unitName + "Youve been healed";

        //if (state == BattleState.PLAYERTURN)
        //    playerUnit.Heal(5);
        //else if (state == BattleState.ENEMYTURN)
        //    enemyUnit.Heal(5);

        //action before 
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        EnemyChance();
        //if (state == BattleState.PLAYERTURN)
        //{

        //    dialogueText.text = playerUnit.unitName + " you've been healed ";
        //    EnemyTurn();
        //}
        //else if (state == BattleState.ENEMYTURN)
        //{
        //    dialogueText.text = enemyUnit.unitName + "Youve been healed ";
        //    PlayerTurn();
        
    }
    
}
