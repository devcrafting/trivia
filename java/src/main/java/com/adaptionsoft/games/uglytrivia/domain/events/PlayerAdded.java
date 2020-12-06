package com.adaptionsoft.games.uglytrivia.domain.events;

public class PlayerAdded {
    public final String name;

    public PlayerAdded(String name) {
        this.name = name;
    }
}
