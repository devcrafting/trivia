package com.adaptionsoft.games.uglytrivia.domain.events;

public class PlayerAdded {
    public final String name;
    public final int numberOfPlayers;

    public PlayerAdded(String name, int numberOfPlayers) {
        this.name = name;
        this.numberOfPlayers = numberOfPlayers;
    }
}
