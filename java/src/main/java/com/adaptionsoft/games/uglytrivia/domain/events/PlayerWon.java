package com.adaptionsoft.games.uglytrivia.domain.events;

import java.util.Objects;

public class PlayerWon {
    private final String playerName;

    public PlayerWon(String playerName) {
        this.playerName = playerName;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        PlayerWon playerWon = (PlayerWon) o;
        return Objects.equals(playerName, playerWon.playerName);
    }

    @Override
    public int hashCode() {
        return Objects.hash(playerName);
    }
}
