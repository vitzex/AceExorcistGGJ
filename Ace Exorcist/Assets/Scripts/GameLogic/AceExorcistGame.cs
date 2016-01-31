using System;

using System.Collections.Generic;
using Cards.Collections;

public class AceExorcistGame
{
	public AceExorcistGame()
	{
        //Create Deck and Hand for Exorcist, and draw 5 cards from Deck to Hand
        ExorcistLibrary = new Deck(IsExorcist);
        ExorcistHand = new Hand();
        for (int i = 1; i < 6; i++)
        {
            ExorcistHand.addCard( ExorcistLibrary.TakeCard() );
        }

        //Create Deck and Hand for Exorcist, and draw 5 cards from Deck to Hand
        IsExorcist = false;
        SummonerLibrary = new Deck(IsExorcist);
        for (int i = 1; i < 6; i++)
        {
            SummonerHand.addCard(SummonerLibrary.TakeCard());
        }


        //Create Summon Zone
        SummonZone = new Hand();

        //Create Discards (as a Hand - but not in this implementation)

	}

    private Deck ExorcistLibrary;
    private Deck SummonerLibrary;
    //public Discard ExorcistDiscard;
    //public Discard SummonerDiscard;
    public Card SummonerLibraryCard;
    private Hand ExorcistHand;
    private Hand SummonerHand;
    private List<Card> CardsPlayed; //is this needed?

    public int ExorcistHP = 30;
    public int SummonerHP = 60;

    public int MaxHandSize = 6;

    public Hand SummonZone;

    public bool IsExorcistTurn = false;
    public bool IsExorcist = true;

    public Hand GetCardsInHand() //is this handled in Hand?
    {
        //Check if player is Exorcist
        if (IsExorcistTurn)
        {
            return ExorcistHand;
        }
        else
        {
            return SummonerHand;
        }
    }

    public void ToggleCard(int index, ref List<Card> CardsPlayed)
    { Hand theHand=null;

        if (IsExorcistTurn) theHand = ExorcistHand;
        else theHand = SummonerHand;

        if ((index > 0) && (index <= theHand.getHandCount()))  //if index out of bounds for the hand size
        {
            foreach (Card theCard in CardsPlayed)
            {
                if (theCard == theHand.hand[index - 1])
                    CardsPlayed.Remove(theHand.hand[index - 1]); //remove index card if already played
                return; //break method in this case
            }

            CardsPlayed.Add(theHand.hand[index - 1]); //add index card to CardsPlayed, if everything safe
        }

        else return; //break method if index out of bounds
    }

    public bool DoExorcistAttack(List<Card> AttackWithCards)
    {

        int AttackValue = 0;

        if (!IsExorcistTurn)
            return false;

            //int ExorcistAttack = 1; //defaulted
                                    // int ExorcistHeal = 1; //defaulted

        foreach (Card theCard in AttackWithCards) // correct enumeration error for AttackWithCards?
        {            AttackValue = AttackValue + (int)theCard.cardValue;
            if (theCard.Suit != AttackWithCards[0].Suit) //exorcist flush = attack
                return false; //no flush no attack
        }

        //if (ExorcistAttack == 0) return false;
        //Check that all in List are of same suit and that cards are from ExorcistHand 
            

            //Check that there is nothing in Summon Zone
            if (SummonZone.getHandCount() == 0) //if Summon Zone empty, attack library

        {
            SummonerLibraryCard = SummonerLibrary.GetTopCard(); //FirstOrDefault();

            while (AttackValue > 0)
            {
                if ((int)SummonerLibraryCard.cardValue <= AttackValue)
                {
                    SummonerHP = SummonerHP - (int)SummonerLibraryCard.cardValue; //change HP
                    AttackValue = AttackValue - (int)SummonerLibraryCard.cardValue; //remaining attackvalue
                    //SummonerDiscard.Add(SummonerLibraryCard); // (Takes it and removes it)
                    SummonerLibrary.TakeCard();    //REMEMBER TO ADD TO DISCARD PILE
                    // Exorcist must also draw one card
                    ExorcistHand.addCard(ExorcistLibrary.TakeCard());
                }
                else
                {
                    AttackValue = -1; //go out of the loop
                    // keep showing the last card on top of the SummonerLibrary
                }

             }

            }

        else //attack Summon Zone
        {
            int i = 0; //choose a card to be attacked (0,1,2)

            if ( (int)SummonZone.hand[i].cardValue <= AttackValue)
            {
                SummonerHP = SummonerHP - (int)SummonZone.hand[i].cardValue; //change HP
                SummonZone.hand.Remove(SummonZone.hand[i]); // (Takes it and removes it)                                     
                // SummonZone[i].Discard;   //REMEMBER TO ADD TO DISCARD PILE
            }
            // Debug.Log("Not enough attack value to destroy that Summoned Card");
            else return false; //attack too small means no attack
        }

        //finally, take the cards played from the Exorcist's hand, and discard them
        foreach (Card theCard in AttackWithCards)
            ExorcistHand.removeCard(theCard); ;// (Takes it and removes it) 
           // SummonZone[i].Discard;   //REMEMBER TO ADD TO DISCARD PILE

        //Do changes to game state (hit points - done, remove cards from hand - done, etc)

        return true;
        //return true if success; false if failed or illegal move
    }

    public bool DoExorcistHeal(List<Card> HealWithCards)
    {
        if (!IsExorcistTurn) //Healing only for Exorcist
            return false;

        //First, check if there are exactly two cards.
        if (HealWithCards.Count != 2)
            //message to player - must play exactly two cards (of equal value)
            return false;

        //If so, check that they are of equal value (a pair)
        if (HealWithCards[0].cardValue != HealWithCards[1].cardValue)
            //message to player - not a pair
            return false;

        //If so, add the value of each card in turn to the Exorcist's HP
        foreach (Card theCard in HealWithCards)
        {
            ExorcistHP = ExorcistHP + (int)theCard.cardValue;
        }

        //finally, take the cards played from the Exorcist's hand, and discard them
        foreach (Card theCard in HealWithCards)
            // (Takes it and removes it) 
            ExorcistHand.hand.Remove(theCard);
            //ExorcistDiscard.Add(HealWithCards.TakeCard(theCard));
               // SummonZone[i].Discard;   //REMEMBER TO ADD TO DISCARD PILE

        //Do changes to game state (hit points - done, remove cards from hand - done, etc)

        return true;
        //return true if success; false if failed or illegal move (done)

    }

    public bool DoSummonerAttack(List<Card> AttackWithCards)
    {
        if (IsExorcistTurn)
            return false;

        AttackWithCards.Sort();
        //ORDER CARDSPLAYED - DONE

        for (int counter = 0; counter < AttackWithCards.Count; counter++)  //CHECK IF CONSECUTIVE RUN
        {
            if (counter < AttackWithCards.Count - 1)
              if (AttackWithCards[counter + 1].cardValue - AttackWithCards[counter].cardValue != 1)
                    return false; //if it's not a consecutive run => break method
        }

        // ADD MITIGATE
        // If exorcist has (AttackWithCards[0].cardValue - 1) 
        // or (AttackWithCards[AttackWithCards.Count-1].cardValue + 1)
        // prompt exorcist whether he would like to mitigate

        //FIX MITIGATE

        bool MitigationPossible = false;
        List<Card> MitigateWithCards=null;

        foreach (Card theCard in ExorcistHand.hand)
            if ((theCard.cardValue == AttackWithCards[0].cardValue - 1) || (theCard.cardValue == AttackWithCards[AttackWithCards.Count - 1].cardValue + 1))
            {
                MitigateWithCards.Add(theCard);
                MitigationPossible = true;
            }
        
        bool Mitigation = false;

        if (MitigationPossible)
        {
            // prompt exorcist whether he would like to mitigate
            if (Mitigation) DoMitigate(MitigateWithCards);
        }

        return true;

        //Validation
    }

    public bool DoSummonerPlaySummon(Card SummonCard)
    {
        if (IsExorcistTurn) //Only for Summoner
            return false;

        //Check that it's a Face card, allowed in Summon Zone
        if ((int)SummonCard.cardValue > 1 && (int)SummonCard.cardValue < 8)
            //message to player - not a Face Card so can't be part of Summons
            return false;

        //If it is, remove it from the Summoner's hand and add it to Summon Zone
        SummonZone.hand.Remove(SummonCard); // (Takes it and removes it) 

        //Do changes to game state (hit points - done, remove cards from hand - done, etc)

        return true;
        //return true if success; false if failed or illegal move (done)

    }

    public bool DoSummonerDrawCards(List<Card> DrawWithCards)
    {
        if (IsExorcistTurn)
            return false;

        //Validation
        //First, check if there are exactly two or three cards.
        if (DrawWithCards.Count != 2 && DrawWithCards.Count != 3)
            //message to player - must play exactly two or three cards (of equal value)
            return false;

        //If so, check that they are of equal value (a pair or triad)
        for (int i=0; i<2; i++)
        {
            if (DrawWithCards[i].cardValue != DrawWithCards[i + 1].cardValue)
                //message to player - cards played must be of equal value
                return false;
        }

        //Discard the cards played and draw new cards
        foreach (Card theCard in DrawWithCards)
        {
            DrawWithCards.Remove(theCard); //remove the card used to draw
            ExorcistHand.addCard(ExorcistLibrary.TakeCard());
            // draw a new card from ExorcistLibrary and add it to ExorcistHand
        }

        //Finally return true
        return true;

    }

    public bool DoMitigate(List<Card> MitigateWithCards)
    {
        if (SummonZone != null)
            return false; //cannot mitigate when there's an attack on summons

        else
        {
            //choose one or two of these cards to mitigate
            //
            return true;
        }

        //Validation

        // DO NOT DRAW CARDS WHEN ONE CHOOSES TO MITIGATE

    }

    public bool CheckVictorySummoner()
    {
        if ((SummonZone.hand.Count == 3) || (ExorcistHP <= 0))
            return true;
        else return false;
        //if 3 cards in summon zone or exorcist HP <= 0, win
    }

    public bool CheckVictoryExorcist()
    {
        if (SummonerHP <= 0)
            return true;
        else return false;
        //if summoner HP <= 0, win
    }

    public void PassTurn()
    {
        IsExorcistTurn = !IsExorcistTurn;
        //draw card for current player
        //should be PlayerLibrary.DrawCard(PlayerHand). Or something like that

        //CheckVictoryExorcist();
        //CheckVictorySummoner();
        //Check victory conditions
    }

     public void validatePlay(List<Card> CardsPlayed)
    {
        if (IsExorcistTurn)
        { int ExorcistAttack=1; //defaulted
            // int ExorcistHeal = 1; //defaulted

            foreach (Card theCard in CardsPlayed)

                if (theCard.Suit != CardsPlayed[0].Suit) //exorcist flush = attack
                    ExorcistAttack = ExorcistAttack * 0; //no flush no attack

                else if ((CardsPlayed.Count == 2) && (CardsPlayed[0].cardValue == CardsPlayed[1].cardValue)) //pair
                    DoExorcistHeal(CardsPlayed); //healed if pair

                    if (ExorcistAttack==1) DoExorcistAttack(CardsPlayed) ; //flush => attack

                    //ADD MITIGATE CODE
        }
        else //summoner
        {
            //checking if summoning
            foreach (Card theCard in CardsPlayed)

                if ((CardsPlayed.Count == 1) && (( (int)CardsPlayed[0].cardValue > 7) || ( (int)CardsPlayed[0].cardValue == 1))) //face cards 
                    DoSummonerPlaySummon(CardsPlayed[0]); //ONE face card played = summon

                else if ((CardsPlayed.Count == 2) && ( CardsPlayed[0].cardValue == CardsPlayed[1].cardValue)) //pair
                    DoSummonerDrawCards(CardsPlayed); //pair => summoner draws

                else DoSummonerAttack(CardsPlayed);
            //REMEMBER TO CHECK IF RUN
                
            //ADD MITIGATE CODE
        }
    }
}
