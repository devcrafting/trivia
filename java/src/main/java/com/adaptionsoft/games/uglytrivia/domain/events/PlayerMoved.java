package com.adaptionsoft.games.uglytrivia.domain.events;

public class PlayerMoved {
    public final String playerName;
    public final int newLocation;

    public PlayerMoved(String playerName, int newLocation) {
        this.playerName = playerName;
        this.newLocation = newLocation;
    }
}
