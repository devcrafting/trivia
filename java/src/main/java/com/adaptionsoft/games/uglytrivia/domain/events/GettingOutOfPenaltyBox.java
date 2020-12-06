package com.adaptionsoft.games.uglytrivia.domain.events;

public class GettingOutOfPenaltyBox {
    public final String playerName;

    public GettingOutOfPenaltyBox(String playerName) {
        this.playerName = playerName;
    }
}
