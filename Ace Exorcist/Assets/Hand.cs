using UnityEngine;
using System.Collections;
using Cards.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {


	public List<Card> hand;//saves the hand being used by player or summoner(can also be possibly used by the summon Zone

	public int upperLimit;//limits the number of cards in hand
	public int currentCardNumber;//number of cards currently in hand

	public float exorcistX;//saves the x position the exorcist's hand will be displayed
	public float summonerX;//same thing for summoner
	public float exorcistY;//saves the height the exorcist's hand will be displayed
	public float summonerY;//same thing for summoner
	RaycastHit2D hit;

	float cardWidth = 2.3f;//hard coded value for now



	public void addCard(Card c)
	{

		hand.Add(c);
		currentCardNumber++;
	}

	public void removeCard(Card c)
	{
		if(currentCardNumber<1){
			Debug.Log("No cards in hand");
			return;
		}

		hand.Remove(c);
		currentCardNumber--;
	}

	public int getHandCount()//returns the amount of cards the hand currently has
	{
		return currentCardNumber;
	}
	
	public void showHand() //lists the hand
	{
		foreach(Card c in hand)
		{
			Debug.Log(c.cardValue + " of " + c.Suit);
		}
	}

	public void displayCards()//shows the cards that exist
	{
		int counter=0;//to move each card 
		Debug.Log("Rearranging the cards...");
		foreach(Transform t in transform)//gets all childs of the hand, i.e., the cards themselves
		{
			Debug.Log("Rearrange "+(counter+1));
			if(this.gameObject.name == "Player_Hand")
				t.position = new Vector2(exorcistX+(cardWidth*counter),exorcistY);
			else
				t.position = new Vector2(summonerX+(cardWidth*counter),summonerY);

			counter++;

		}
	}





	// Use this for initialization
	void Start () {
		upperLimit = 7;//for now at least
		currentCardNumber=0;
		hand = new List<Card>();

		//to know where to start creating the cards of each hand
		summonerX = GameObject.Find("Enemy_Hand").transform.position.x;
		exorcistX = GameObject.Find("Player_Hand").transform.position.x;
		summonerY = GameObject.Find("Enemy_Hand").transform.position.y;
		exorcistY = GameObject.Find("Player_Hand").transform.position.y;

	}




}
