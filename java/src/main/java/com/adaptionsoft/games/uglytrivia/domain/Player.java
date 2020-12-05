package com.adaptionsoft.games.uglytrivia.domain;

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

    public void winAGoldCoin() {
        goldCoins++;
        System.out.println(name
                + " now has "
                + goldCoins
                + " Gold Coins.");
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
