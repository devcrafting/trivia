package com.adaptionsoft.games.uglytrivia.domain;

import com.adaptionsoft.games.uglytrivia.domain.events.PlayerAdded;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.List;

public class Players {
    private List<Player> players = new ArrayList<>();
    private int currentPlayer;

    public List<Object> add(String playerName) {
        players.add(new Player(playerName));
        return Arrays.asList(
            new PlayerAdded(playerName, players.size()));
    }

    public Player getCurrentPlayer() {
        return players.get(currentPlayer);
    }

    public List<Object> switchToNextPlayer() {
        List<Object> events = getCurrentPlayer().hasPlayerWon();
        currentPlayer++;
        if (currentPlayer == players.size()) currentPlayer = 0;
        return events;
    }
}
