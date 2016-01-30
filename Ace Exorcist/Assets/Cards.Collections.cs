using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Cards.Collections
{
	//order 52 cards based on suit and value; to be changed to SUIT our needs(get it?) later
	public enum Suit
	{
		Clubs = 1,   
		Diamonds = 2,   
		Hearts = 3,  
		Spades = 4, 
	}

	public enum cardValue
	{
		Ace = 1,
		Two = 2,
		Three = 3,
		Four = 4,
		Five = 5,
		Six = 6,
		Seven = 7,
		Eight = 8,
		Nine = 9,
		Ten = 10,
		Jack = 11,
		Queen = 12,
		King = 13,

	}
	
	

	public class Card
	{
		public Suit Suit { get; set; }
		public cardValue cardValue { get; set; }
	}
	
  
	public class Deck
	{
		public List<Card> Cards { get; set; } //each card has a suit and value
		
		public Deck()
		{
			CreateDeck();
		}

		//creates the deck
		public void CreateDeck()
		{
			Cards = new List<Card>();
			for(int i = 1;i<5;i++)//for each suit...
			{
				for(int j = 2;j<15;j++)//creates a card with this suit and this value
				{
					Card c = new Card();
					c.cardValue=(cardValue)j;
					c.Suit=(Suit)i;
					Cards.Add(c);
				}

			}
		}

		public void swap(int x, int y)//to swap cards at positions x and y on the Cards array
		{
			Card tempx = Cards[x];
			Card tempy = Cards[y];
			//remove x and y and reintroduce their replacements

			Cards.RemoveAt(x);
			Cards.Insert(x,tempy);

			Cards.RemoveAt(y);
			Cards.Insert(y,tempx);

		}

		public void shuffleDeck()
		{
			for(int i = 0;i<Cards.Count;i++)//for each card, find a random number and swap positions with that card
			{
				int r =UnityEngine.Random.Range(0,52);
				swap(i,r);//swaps cards at positions i and r
			}

		}



		/*
		public void Reset()
		{
			Cards = Enumerable.Range(1, 4).SelectMany(
				suit => Enumerable.Range(1, 13).Select(
				cardValue => new Card() { Suit = (Suit)suit, cardValue = (cardValue)cardValue })).ToList();
		}
		
		/// <summary>
		///  Embaralha ordem das cartas do deck
		/// </summary>  
		public void DeckShuffle()
		{
			Cards = Cards.OrderBy(cardValue => Guid.NewGuid()).ToList();
		}*/
		
		/// <summary>
		///  Remove a proxima carta do DECK e retorna esta Carta
		/// </summary>  
		public Card TakeCard()
		{
			if (Cards.Count > 0)
			{
				Card card = Cards.FirstOrDefault(); //Pega o primeiro da sequencia no Deck
				Cards.Remove(card); //Removemos esta carta do Deck
				return card;
			}
			else
			{                               
				return null;
			}
		}
		
		
		
	}
	
}