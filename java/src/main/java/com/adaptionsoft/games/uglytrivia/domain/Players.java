package com.adaptionsoft.games.uglytrivia.domain;

import java.util.ArrayList;
import java.util.List;

public class Players {
    private List<Player> players = new ArrayList<>();
    private int currentPlayer;

    public void add(String playerName) {
        players.add(new Player(playerName));
        System.out.println(playerName + " was added");
        System.out.println("They are player number " + players.size());
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
