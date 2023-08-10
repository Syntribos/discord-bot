﻿using DiscordBot.Utilities;

namespace DiscordBot.CardGames;

public class Deck
{
    private static readonly CardSuit[] _suits = (CardSuit[])Enum.GetValues(typeof(CardSuit));
    private static readonly CardValue[] _values = (CardValue[])Enum.GetValues(typeof(CardValue));

    private readonly List<Card> _cards;

    private Deck(List<Card> cards)
    {
        _cards = cards;
    }

    public static Deck Create52CardDeck()
    {
        var cards = _suits.SelectMany(suit => _values.Select(value => new Card(suit, value))).ToList();
        return new Deck(cards);
    }
    
    public static Deck Create54CardDeck()
    {
        var deck = Create52CardDeck();
        deck._cards.AddRange(new List<Card> { Card.CreateJoker(), Card.CreateJoker()});
        return deck;
    }
    
    public static Deck CreateShuffled54CardDeck()
    {
        var deck = Create54CardDeck();
        deck.Shuffle();
        return deck;
    }

    public static Deck CreateShuffled52CardDeck()
    {
        var deck = Create52CardDeck();
        deck.Shuffle();
        return deck;
    }

    public void Shuffle()
    {
        _cards.Shuffle();
    }

    public Card Draw()
    {
        var card = _cards.First();
        _cards.RemoveAt(0);
        return card;
    }

    public List<Card> Draw(int count)
    {
        return count <= 0 ? Enumerable.Empty<Card>().ToList() : Enumerable.Range(0, count).Select(_ => Draw()).ToList();
    }

    public Deck Merge(Deck toMerge)
    {
        _cards.AddRange(toMerge._cards);
        return this;
    }

    public Deck MergeAndShuffle(Deck toMerge)
    {
        var newDeck = Merge(toMerge);
        newDeck.Shuffle();
        return newDeck;
    }
}