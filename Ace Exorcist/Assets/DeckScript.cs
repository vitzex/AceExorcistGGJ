using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cards.Collections;

public class  DeckScript : MonoBehaviour {
	
	
	public int cardAmount;
	public int cardsRemaining;
	public Deck deck;

	public Hand playerH;
	public Hand enemyH;

	public int maxHandValue;
	
	
	// Use this for initialization
	void Start () {
		//for now, has the same amount as a normal deck?
		cardAmount = 52;
		cardsRemaining=cardAmount;
		maxHandValue = 6;
		deck = new Deck();

		//create references to the player's Hand component and the enemy's
		playerH = GameObject.Find("Player_Hand").GetComponent<Hand>();
		enemyH = GameObject.Find("Enemy_Hand").GetComponent<Hand>();

		
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
		if(Input.GetKeyDown(KeyCode.E)&&this.gameObject.name=="Enemy_Deck")//will make the enemy draw a card and show its hand
		{
			if(enemyH.currentCardNumber<maxHandValue)//max number of cards you can have in your hand
			{
			Card cardDrawn = deck.TakeCard();
			//creates the card in the game world
			GameObject card = Instantiate(Resources.Load("CardModel"),new Vector3(3,3), Quaternion.identity) as GameObject;
			card.GetComponent<CardModel>().cardValue = (int)cardDrawn.cardValue;
			card.GetComponent<CardModel>().cardSuit = (int)cardDrawn.Suit;
			Sprite cardSprite = Resources.Load<Sprite>("Sprites/aceSpades");

			enemyH.addCard(cardDrawn);

			card.GetComponent<SpriteRenderer>().sprite = cardSprite;


			Debug.Log("Enemy drew a "+cardDrawn.cardValue + " of " + cardDrawn.Suit);
			card.transform.parent = GameObject.Find("Enemy_Hand").transform;//make this card a child of the enemy hand

			Debug.Log("Enemy has "+enemyH.getHandCount());
			enemyH.showHand();
			enemyH.displayCards();

			}
			else
			{
				Debug.Log("Enemy can't draw anymore cards!");
			}

		}
		else if(Input.GetKeyDown(KeyCode.P)&&this.gameObject.name=="Player_Deck")//will make the player draw a card and show it's current hand
		{
			if(playerH.currentCardNumber<maxHandValue)//max 
			{
			Card cardDrawn = deck.TakeCard();
			Debug.Log("Player drew a "+cardDrawn.cardValue + " of " + cardDrawn.Suit);
			
			//creates the card in the game world
			GameObject card = Instantiate(Resources.Load("CardModel"), new Vector3(-3,-2),Quaternion.identity) as GameObject;
			card.GetComponent<CardModel>().cardValue = (int)cardDrawn.cardValue;
			card.GetComponent<CardModel>().cardSuit = (int)cardDrawn.Suit;

			Sprite cardSprite = Resources.Load<Sprite>("Sprites/aceSpades");
			
			card.GetComponent<SpriteRenderer>().sprite = cardSprite;

			playerH.addCard(cardDrawn);

			card.transform.parent = GameObject.Find("Player_Hand").transform;//make this card a child of the player hand

			//shows player hand
			Debug.Log("Player has "+playerH.getHandCount());
			playerH.showHand();
			playerH.displayCards();
			}
			else
			{
				Debug.Log("Player can't draw anymore cards!");
			}
		}
	}
}
