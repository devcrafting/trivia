package com.adaptionsoft.games.uglytrivia.domain.events;

public class DiceRolled {
    public final String playerName;
    public final int roll;

    public DiceRolled(String playerName, int roll) {
        this.playerName = playerName;
        this.roll = roll;
    }
}
