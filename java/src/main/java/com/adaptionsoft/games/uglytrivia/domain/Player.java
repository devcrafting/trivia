package com.adaptionsoft.games.uglytrivia.domain;

import com.adaptionsoft.games.uglytrivia.domain.events.GoldCoinWon;

public class Player {
    public final String name;
    private int location;
    private int goldCoins;
    private boolean isInPenaltyBox;

    public Player(String name) {
        this.name = name;
    }

    public void move(int roll) {
        location += roll;
        if (location > 11) location = location - 12;
    }

    public int getLocation() {
        return location;
    }

    public GoldCoinWon winAGoldCoin() {
        goldCoins++;
        return new GoldCoinWon(name, goldCoins);
    }

    public boolean hasPlayerWon() {
        return goldCoins == 6;
    }

    public boolean isInPenaltyBox() {
        return isInPenaltyBox;
    }

    public void sendToPenaltyBox() {
        isInPenaltyBox = true;
    }
}
