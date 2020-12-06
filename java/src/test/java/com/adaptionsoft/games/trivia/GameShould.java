package com.adaptionsoft.games.trivia;

import com.adaptionsoft.games.uglytrivia.domain.Game;
import com.adaptionsoft.games.uglytrivia.domain.events.PlayerWon;
import com.adaptionsoft.games.uglytrivia.infra.GeneratedQuestions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.stream.IntStream;

import static org.assertj.core.api.Assertions.assertThat;

public class GameShould {
    @Test
    public void return_PlayerWon_event() {
        // Arrange : Player is about to answer to win
        Game game = new Game(new GeneratedQuestions());
        game.add("toto");
        IntStream.range(1, 6).forEach(i -> {
            game.roll(1);
            game.wasCorrectlyAnswered(new ArrayList<>());
        });
        game.roll(1);

        // Act
        ArrayList<Object> events = new ArrayList<>();
        game.wasCorrectlyAnswered(events);

        // Assert
        assertThat(events).contains(new PlayerWon("toto"));
    }
}
