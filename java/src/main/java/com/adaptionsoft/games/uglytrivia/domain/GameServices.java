package com.adaptionsoft.games.uglytrivia.domain;

import java.util.Random;

public class GameServices implements GameFacade {
    private final Games games;
    private final Questions questions;

    public GameServices(Games games, Questions questions) {
        this.games = games;
        this.questions = questions;
    }

    @Override
    public int newGame() {
        Game game = new Game(questions);
        games.save(game);
        return 0;
    }

    @Override
    public void addPlayer(int gameId, String playerName){
        Game game = games.get(gameId);
        game.add(playerName);
        games.save(game);
    }

    @Override
    public void roll(int gameId){
        Game game = games.get(gameId);
        Random random = new Random();
        game.roll(random.nextInt(5) + 1);
        games.save(game);
    }
}
