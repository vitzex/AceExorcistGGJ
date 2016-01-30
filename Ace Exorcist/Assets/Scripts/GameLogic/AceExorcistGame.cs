using System;
/*
public class AceExorcistGame
{
	public AceExorcistGame()
	{
        
	}

    private ExorcistLibrary ExorcistLibrary;
    private SummonerLibrary SummonerLibrary;
    public ExorcistDiscard ExorcistDiscard;
    public SummonerDiscard SummonerDiscard;
    private PlayerHand ExorcistHand;
    private PlayerHand SummonerHand;

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
        if (!IsExorcistTurn)
            return false;

        //Check that all in List are of same suit and that cards are from ExorcistHand

        //Check that there is nothing in Summon Zone

        //Do changes to game state (hit points, remove cards from hand, etc)

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
}
*/