using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cards.Collections;

public class  EnemyDeck : MonoBehaviour {
	
	
	public int cardAmount;
	public int cardsRemaining;
	public Deck deck;
	
	
	// Use this for initialization
	void Start () {
		//for now, has the same amount as a normal deck?
		cardAmount = 52;
		cardsRemaining=cardAmount;
		//do I have to put them in manually...?
		deck = new Deck();
		
		
		//Tests to see if card deck really works
		Debug.Log("Amount of cards: " + deck.Cards.Count);
		
		//shuffles deck
		deck.shuffleDeck();
		
		//Shows each card for that deck
		Debug.Log("Cards on this deck");
		for(int i =0;i< deck.Cards.Count;i++)
		{
			Debug.Log(deck.Cards[i].cardValue + " of "+ deck.Cards[i].Suit);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))//will make the player draw a card
		{
			Card cardDrawn = deck.TakeCard();
			Debug.Log("Enemy drew a "+cardDrawn.cardValue + " of " + cardDrawn.Suit);

			//creates the card in the game world
			GameObject card = Instantiate(Resources.Load("CardModel")) as GameObject;
			card.GetComponent<CardModel>().cardValue = (int)cardDrawn.cardValue;
			card.GetComponent<CardModel>().cardSuit = (int)cardDrawn.Suit;
			//Debug.Log("Number of sprites in vector: "+ card.GetComponent<CardModel>().cardFaces.Length);
			Sprite cardSprite = Resources.Load<Sprite>("Sprites/aceSpades");

			card.GetComponent<SpriteRenderer>().sprite = cardSprite;


			card.transform.parent = this.gameObject.transform;//make this card a child of the enemy deck

		}
	}
}
