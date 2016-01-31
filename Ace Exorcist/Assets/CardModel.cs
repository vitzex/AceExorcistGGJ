using UnityEngine;
using System.Collections;
using Cards.Collections;

public class CardModel : MonoBehaviour {

	public cardValue cardValue;
	public Suit cardSuit;
	public bool faceUp;//stores if the card is face up, i.e., if it's yours
	public Sprite cardFace, cardBack;//cardBack is different depending on whether it's enemy or player cards


	public Sprite cardFaces;


	// Use this for initialization
	void Start () {
		cardFaces = Resources.Load<Sprite>("Sprites/cardSheet");
		faceUp=false;//when the card is created, it's face down, just to be safe
	}

	Sprite loadCardSprite(int value, int suit)//generates the card sprite based on its value and suit
	{
		return cardFaces;

	}

	void Update()
	{

		
	}
}
