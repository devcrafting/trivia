package com.adaptionsoft.games.uglytrivia.domain;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Players {
    private List<Player> players = new ArrayList<>();
    private int currentPlayer;

    public List<Object> add(String playerName) {
        players.add(new Player(playerName));
        return Arrays.asList(
            playerName + " was added",
            "They are player number " + players.size());
    }

    public Player getCurrentPlayer() {
        return players.get(currentPlayer);
    }

    public boolean switchToNextPlayer() {
        boolean winner = getCurrentPlayer().hasPlayerWon();
        currentPlayer++;
        if (currentPlayer == players.size()) currentPlayer = 0;

        return winner;
    }
}
