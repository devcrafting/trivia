package com.adaptionsoft.games.uglytrivia.domain.events;

public class SentToPenaltyBox {
    public final String playerName;

    public SentToPenaltyBox(String playerName) {
        this.playerName = playerName;
    }
}
