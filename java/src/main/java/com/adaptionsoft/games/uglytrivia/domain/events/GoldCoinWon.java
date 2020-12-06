package com.adaptionsoft.games.uglytrivia.domain.events;

public class GoldCoinWon {
    public final String playerName;
    public final int goldCoins;

    public GoldCoinWon(String playerName, int goldCoins) {
        this.playerName = playerName;
        this.goldCoins = goldCoins;
    }
}
