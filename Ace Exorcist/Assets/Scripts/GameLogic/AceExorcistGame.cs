using System;

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

    public bool DoExorcistAttack(Card AttackedCardInSummon, List<Card> AttackWithCards)
    {

        int AttackValue = 0;

        if (!IsExorcistTurn)
            return false;

            int ExorcistAttack = 1; //defaulted
                                    // int ExorcistHeal = 1; //defaulted

        foreach (Card theCard in CardsPlayed)
        {            AttackValue = AttackValue + theCard.CardValue;  
            if (theCard.cardValue.Suit != CardsPlayed[0].Suit) //exorcist flush = attack
                ExorcistAttack = ExorcistAttack * 0; //no flush no attack
        }

        if (ExorcistAttack == 0) return false;
        //Check that all in List are of same suit and that cards are from ExorcistHand 
            

            //Check that there is nothing in Summon Zone
            if (SummonZone.Count == 0) //if Summon Zone empty, attack library

        { SummonerLibraryCard = SummonerLibrary.FirstOrDefault();

            while (AttackValue > 0)
            {
                if (SummonerLibraryCard.CardValue <= AttackValue)
                {
                    SummonerHP = SummonerHP - SummonerLibraryCard.CardValue; //change HP
                    AttackValue = AttackValue - SummonerLibraryCard.CardValue; //remaining attackvalue
                    SummonerLibraryCard.TakeCard(); // (Takes it and removes it)
                                                  // SummonerLibraryCard.Discard;   //REMEMBER TO ADD TO DISCARD PILE
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
                SummonZone[i].TakeCard(); // (Takes it and removes it) 
                                        //CHECK IF TAKECARD IS A FUNCTION FOR ANY LIST (as opposed to just decks)
                                        // SummonZone[i].Discard;   //REMEMBER TO ADD TO DISCARD PILE
            }
            else return false; //attack too small means no attack
            // DebugLog("Not enough attack value to destroy that Summoned Card");
        }

        foreach (Card theCard in CardsPlayed)
           theCard.TakeCard(); // (Takes it and removes it) 
                               //CHECK IF TAKECARD IS A FUNCTION FOR ANY LIST (as opposed to just decks)
                               // SummonZone[i].Discard;   //REMEMBER TO ADD TO DISCARD PILE

        //Do changes to game state (hit points - done, remove cards from hand - done, etc)

        return true;
        //return true if success; false if failed or illegal move
    }

    public bool DoExorcistHeal(List<Card> HealWithCards)
    {
        if (!IsExorcistTurn)
            return false;
        //Validation similar to above
    }

    public bool DoSummonerAttack(List<Card> AttackWithCards)
    {
        if (IsExorcistTurn)
            return false;
        //Validation
    }

    public bool DoSummonerPlaySummon(Card SummonCard)
    {
        if (IsExorcistTurn)
            return false;

        //Validation

        //Add to summonzone
    }

    public bool DoSummonerDrawCards(List<Card> DrawWithCards)
    {
        if (IsExorcistTurn)
            return false;

        //Validation

        //Draw cards
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
