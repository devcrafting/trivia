package com.adaptionsoft.games.uglytrivia;

public class Player {
    private String name;
    private int place = 0;
    private int goldCoins = 0;
    private boolean isInPenaltyBox = false;

    public Player(String playerName) {
        name = playerName;
    }

    public String getName() {
        return name;
    }

    public int getPlace() {
        return place;
    }

    public PlayerMoved move(int roll) {
        this.place = (place + roll) % 12;
        return new PlayerMoved(place, name);
    }

    public GoldCoinWon winGoldCoin() {
        goldCoins++;
        return new GoldCoinWon(name, goldCoins);
    }

    public boolean hasWon() {
        return goldCoins == 6;
    }

    public boolean isInPenaltyBox() {
        return this.isInPenaltyBox;
    }

    public PlayerSentToPenaltyBox goToPenaltyBox() {
        this.isInPenaltyBox = true;
        return new PlayerSentToPenaltyBox(name);
    }
}
