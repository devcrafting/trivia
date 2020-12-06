package com.adaptionsoft.games.uglytrivia.domain;

import com.adaptionsoft.games.uglytrivia.domain.events.GoldCoinWon;
import com.adaptionsoft.games.uglytrivia.domain.events.PlayerWon;
import com.adaptionsoft.games.uglytrivia.domain.events.SentToPenaltyBox;

import java.util.*;

public class Player {
    public final String name;
    private int location;
    private int goldCoins;
    private boolean isInPenaltyBox;

    public Player(String name) {
        this.name = name;
    }

    private Player(String name, int location, int goldCoins, boolean isInPenaltyBox) {
        this.name = name;
        this.location = location;
        this.goldCoins = goldCoins;
        this.isInPenaltyBox = isInPenaltyBox;
    }

    public static Player loadState(String name, int location, int goldCoins, boolean isInPenaltyBox) {
        return new Player(name, location, goldCoins, isInPenaltyBox);
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

    public List<Object> hasPlayerWon() {
        return goldCoins == 6
                ? Arrays.asList(new PlayerWon(name))
                : Collections.emptyList();
    }

    public boolean isInPenaltyBox() {
        return isInPenaltyBox;
    }

    public SentToPenaltyBox sendToPenaltyBox() {
        isInPenaltyBox = true;
        return new SentToPenaltyBox(name);
    }
}
