package com.adaptionsoft.games.uglytrivia.domain;

import com.adaptionsoft.games.uglytrivia.domain.events.PlayerAdded;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Players {
    private List<Player> players;
    private int currentPlayer;

    public Players() {
        this(new ArrayList<>());
    }

    private Players(List<Player> players) {
        this.players = players;
    }

    public static Players loadState(Player... players) {
        return new Players(Arrays.asList(players.clone()));
    }

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
