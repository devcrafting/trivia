package com.adaptionsoft.games.uglytrivia.domain;

public interface Games {
    Game get(int gameId);

    void save(Game game);
}
