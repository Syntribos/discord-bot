namespace DiscordBot.CardGames;

public enum CardSuit
{
    Hearts = 0,
    Spades,
    Diamonds,
    Clubs,
}

public enum CardValue
{
    Ace = 1,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
}

public class Card
{
    private readonly bool _becameJokar;

    public Card(CardSuit suit, CardValue value)
    {
        _becameJokar = false;
        Suit = suit;
        Value = value;
    }

    private Card()
    {
        _becameJokar = true;
        Suit = CardSuit.Clubs;
        Value = CardValue.Eight;
    }
    
    public CardSuit Suit { get; }
    
    public CardValue Value { get; }

    public override string ToString()
    {
        return _becameJokar ? "Joker" : $"{Value} of {Suit}";
    }

    public static Card CreateJoker()
    {
        return new Card();
    }
}