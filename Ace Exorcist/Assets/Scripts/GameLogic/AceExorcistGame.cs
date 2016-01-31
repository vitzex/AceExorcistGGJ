using System;

using System.Collections.Generic;
using Cards.Collections;

public class AceExorcistGame
{
	public AceExorcistGame()
	{
        
	}

    private ExorcistLibrary ExorcistLibrary;
    private SummonerLibrary SummonerLibrary;
    public ExorcistDiscard ExorcistDiscard;
    public SummonerDiscard SummonerDiscard;
    public Card SummonerLibraryCard;
    private PlayerHand ExorcistHand;
    private PlayerHand SummonerHand;
    private List<Card> CardsPlayed;

    public int ExorcistHP = 30;
    public int SummonerHP = 60;

    public int MaxHandSize = 6;

    public SummonZone SummonZone;

    public bool IsExorcistTurn = false;

    public PlayerHand GetCardsInHand()
    {
        //Check if player is Exorcist
        if (IsExorcistTurn)
        {
            //return ExorcistHand;
        }
        else
        {
            return SummonerHand;
        }
    }

    public bool DoExorcistAttack(List<Card> AttackWithCards)
    {

        int AttackValue = 0;

        if (!IsExorcistTurn)
            return false;

            int ExorcistAttack = 1; //defaulted
                                    // int ExorcistHeal = 1; //defaulted

        foreach (Card theCard in AttackWithCards) // correct enumeration error for AttackWithCards?
        {            AttackValue = AttackValue + (int)theCard.cardValue;  
            if (theCard.Suit != AttackWithCards[0].Suit) //exorcist flush = attack
                ExorcistAttack = ExorcistAttack * 0; //no flush no attack
        }

        if (ExorcistAttack == 0) return false;
        //Check that all in List are of same suit and that cards are from ExorcistHand 
            

            //Check that there is nothing in Summon Zone
            if (SummonZone.Count == 0) //if Summon Zone empty, attack library

        { SummonerLibraryCard = SummonerLibrary.FirstOrDefault();

            while (AttackValue > 0)
            {
                if ((int)SummonerLibraryCard.cardValue <= AttackValue)
                {
                    SummonerHP = SummonerHP - (int)SummonerLibraryCard.cardValue; //change HP
                    AttackValue = AttackValue - (int)SummonerLibraryCard.cardValue; //remaining attackvalue
                    SummonerLibraryCard.takeCard(); // (Takes it and removes it)
                    // SummonerLibraryCard.Discard;    //REMEMBER TO ADD TO DISCARD PILE
                    // Exorcist must draw one card!!!!!!!!
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

            if (SummonZone[i].CardValue <= AttackValue)
            {
                SummonerHP = SummonerHP - SummonZone[i].CardValue; //change HP
                SummonZone[i].TakeCard(); // (Takes it and removes it)                                         // SummonZone[i].Discard;   //REMEMBER TO ADD TO DISCARD PILE
            }
            // DebugLog("Not enough attack value to destroy that Summoned Card");
            else return false; //attack too small means no attack
        }

        //finally, take the cards played from the Exorcist's hand, and discard them
        foreach (Card theCard in AttackWithCards)
           theCard.TakeCard(); // (Takes it and removes it) 
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
            theCard.TakeCard(); // (Takes it and removes it) 
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
        List<Card> MitigateWithCards;

        foreach (Card theCard in ExorcistHand)
            if ((theCard.cardValue == AttackWithCards[0].cardValue - 1) || (theCard.cardValue == AttackWithCards[AttackWithCards.Count - 1].cardValue + 1))
            {
                MitigateWithCards.Add(theCard);
                MitigationPossible = true;
            }

        bool Mitigation = false;

        if (MitigationPossible)
        {
            // prompt exorcist whether he would like to mitigate
            if Mitigation DoMitigate(MitigateWithCards);
        }
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
        SummonCard.TakeCard(); // (Takes it and removes it) 

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
            theCard.TakeCard();
            // draw a new card from ExorcistLibrary and add it to ExorcistHand
        }
        //and draw one more card from ExorcistLibrary and add to ExorcistHand

        //Finally return true
        return true;

    }

    public bool DoMitigate(List<Cards> MitigateWithCards)
    {
        if (SummonZone = null)
            return false; //cannot mitigate when there's an attack on summons

        //Validation

        // DO NOT DRAW CARDS WHEN ONE CHOOSES TO MITIGATE

    }

    public bool CheckVictorySummoner()
    {
        //if 3 cards in summon zone or exorcist HP <= 0, win
    }

    public bool CheckVictoryExorcist()
    {
        //if summoner HP <= 0, win
    }

    public void PassTurn()
    {
        IsExorcistTurn = !IsExorcistTurn;
        //draw card for current player
        //should be PlayerLibrary.DrawCard(PlayerHand). Or something like that

        //Check victory conditions
    }

     public void validatePlay(List<Card> CardsPlayed)
    {
        if (IsExorcistTurn)
        { int ExorcistAttack=1; //defaulted
            // int ExorcistHeal = 1; //defaulted

            foreach (Card theCard in CardsPlayed)

                if (theCard.cardValue.Suit != CardsPlayed[0].Suit) //exorcist flush = attack
                    ExorcistAttack = ExorcistAttack * 0; //no flush no attack

                else if ((CardsPlayed.count == 2) && (CardsPlayed[0].CardValue == CardsPlayed[1].CardValue)) //pair
                    DoExorcistHeal(CardsPlayed); //healed if pair

                    if (ExorcistAttack==1) DoExorcistAttack(CardsPlayed) ; //flush => attack

                    //ADD MITIGATE CODE
        }
        else //summoner
        {
            //checking if summoning
            foreach (Card theCard in CardsPlayed)

                if ((CardsPlayed.count == 1) && ((CardsPlayed[0].CardValue > 7) || (CardsPlayed[0].CardValue == 1))) //face cards 
                    DoSummonerPlaySummon(CardsPlayed); //face cards = summon

                else if ((CardsPlayed.count == 2) && (CardsPlayed[0].CardValue == CardsPlayed[1].CardValue)) //pair
                    DoSummonerDrawCards(CardsPlayed); //pair => summoner draws

                else DoSummonerAttack(CardsPlayed);
            //REMEMBER TO CHECK IF RUN
                
            //ADD MITIGATE CODE
        }
    }
}
