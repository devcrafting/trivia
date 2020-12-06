package com.adaptionsoft.games.uglytrivia.domain.events;

import java.util.Objects;

public class GoldCoinWon {
    public final String playerName;
    public final int goldCoins;

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        GoldCoinWon that = (GoldCoinWon) o;
        return goldCoins == that.goldCoins && Objects.equals(playerName, that.playerName);
    }

    @Override
    public int hashCode() {
        return Objects.hash(playerName, goldCoins);
    }

    public GoldCoinWon(String playerName, int goldCoins) {
        this.playerName = playerName;
        this.goldCoins = goldCoins;
    }
}
