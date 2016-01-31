using UnityEngine;
using System.Collections;
using Cards.Collections;

public class ClickTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			//hit=Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
			Vector2 cameraPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//Debug.Log("Clicked at "+cameraPoint.x+","+cameraPoint.y);
			Collider2D col =Physics2D.OverlapPoint(cameraPoint);
			if(col!=null)
			{
				//if it clicks on a card, it takes it out of the game
				//creates a new card object to remove it from the deck
				Card cardClicked = new Card();
				cardClicked.cardValue = col.transform.GetComponent<CardModel>().cardValue;
				cardClicked.Suit = col.transform.GetComponent<CardModel>().cardSuit;
				col.transform.parent.GetComponent<Hand>().removeCard(cardClicked);
				Debug.Log("Current cards = "+col.transform.parent.GetComponent<Hand>().currentCardNumber);
				col.transform.parent.GetComponent<Hand>().displayCards();
				//after you take the card from player's hand, destroy the game object and rearrange the hand

				Hand parentHand = col.gameObject.transform.parent.GetComponent<Hand>();

				DestroyImmediate(col.gameObject);
				parentHand.displayCards();



			}
		}
	}
}
