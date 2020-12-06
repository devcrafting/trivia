package com.adaptionsoft.games.uglytrivia.domain.events;

public class TryToGetOutOfPenaltyBoxFailed {
    public final String playerName;

    public TryToGetOutOfPenaltyBoxFailed(String playerName) {
        this.playerName = playerName;
    }
}
