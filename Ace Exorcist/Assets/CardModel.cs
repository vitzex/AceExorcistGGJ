using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour {

	public int cardValue;
	public int cardSuit;
	public Sprite cardFace, cardBack;//cardBack is different depending on whether it's enemy or player cards


	public Sprite cardFaces;


	// Use this for initialization
	void Start () {
		cardFaces = Resources.Load<Sprite>("Sprites/cardSheet");
	}

	Sprite loadCardSprite(int value, int suit)//generates the card sprite based on its value and suit
	{
		return cardFaces;

	}


	// Update is called once per frame
	void Update () {
	
	}
}
