﻿using UnityEngine;
using System.Collections;
using Cards.Collections;
using System.Collections.Generic;

public class Enemy_Turn : MonoBehaviour {
	
	
	List<Card> cardsChosen;//cards chosen by the player; get them through the clicking script
	
	// Use this for initialization
	void Start () {
		cardsChosen = new List<Card>();
		Debug.Log("It's the summoner's turn.");
		Debug.Log("Select the cards you want to use.");
		Debug.Log("Once they're chosen, press A to attack the exorcist's health, D to draw, S to summon or P to Pass");
	}
	
	void endTurn()//ends the player turn; modifies the exorcistTurn boolean and destroys this component
	{
		this.gameObject.GetComponent<MainGameLoop>().AEG.IsExorcistTurn=true;
		Destroy(this);//removes component
	}
	
	// Update is called once per frame
	void Update () {
		//waits for the player to input a keyboard input
		
		//TODO: How to get the list of cards to send it to the proper functions?
		//TODO: is putting every keyboard input the only way I can do it...?

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			AceExorcistGame.toggleCard(1, ref cardsChosen);//adds/removes the card at index 1 from the cardsChosen
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			AceExorcistGame.toggleCard(2, ref cardsChosen);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			AceExorcistGame.toggleCard(3, ref cardsChosen);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			AceExorcistGame.toggleCard(4, ref cardsChosen);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha5))
		{
			AceExorcistGame.toggleCard(5, ref cardsChosen);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha6))
		{
			AceExorcistGame.toggleCard(6, ref cardsChosen);
		}


		//gets the actions of the summoner


		if(Input.GetKeyDown(KeyCode.A))
		{
			if(cardsChosen.Count<1)//no cards chosen for attack
			{
				Debug.Log("Can't attack; no cards chosen");
			}
			else
			{
				//checks for the validity of the attack
				if(AceExorcistGame.doSummonerAttack(cardsChosen))//if the attack succeeds, this will return true
					endTurn();//from here, it can just finish the turn
				else
				{
					Debug.Log("Couldn't attack with the chosen cards");
				}
			}
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			//should get the cards and see if it's a pair and if you can heal with them
			if(cardsChosen.Count<1)
			{
				Debug.Log("Can't attack; no cards chosen");
			}
			else
			{
				//checks for the validity of the healing
				if(AceExorcistGame.doSummonerDraw(cardsChosen))
				{
					//from here, ends the player's turn
					endTurn();
				}
				else
				{
					Debug.Log("Couldn't draw with the chosen cards/max number of cards reached");
				}
			}
		}

		else if(Input.GetKeyDown(KeyCode.S))
		{
			//should get the cards and see if they're a high card that's eligible for summons
			if(cardsChosen.Count<1)
			{
				Debug.Log("Can't attack; no cards chosen");
			}
			else
			{
				//checks for the validity of the summon
				if(Ac
				   endTurn();

		}
		
		else if(Input.GetKeyDown(KeyCode.P))
		{
			//doesn't need to wait for card input, passes automatically
			AceExorcistGame.passTurn();
			endTurn();
		}
		
	}
}
