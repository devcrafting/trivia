package com.adaptionsoft.games.uglytrivia.domain;

public interface GameFacade {

    int newGame();

    void addPlayer(int gameId, String playerName);

    void roll(int gameId);
}
