package com.adaptionsoft.games.trivia;

import com.adaptionsoft.games.uglytrivia.domain.Game;
import com.adaptionsoft.games.uglytrivia.domain.Player;
import com.adaptionsoft.games.uglytrivia.domain.Players;
import com.adaptionsoft.games.uglytrivia.domain.events.GoldCoinWon;
import com.adaptionsoft.games.uglytrivia.domain.events.PlayerWon;
import com.adaptionsoft.games.uglytrivia.infra.GeneratedQuestions;
import org.junit.jupiter.api.Test;

import java.util.List;
import java.util.UUID;

import static org.assertj.core.api.Assertions.assertThat;

public class GameShould {
    @Test
    public void return_PlayerWon_event() {
        // Arrange : Player is about to answer to win
        UUID gameId = UUID.randomUUID();
        Game game =
            Game.loadState(gameId,
                Players.loadState(
                    Player.loadState("toto", 1, 5, false)),
                new GeneratedQuestions());

        // Act
        List<Object> events = game.wasCorrectlyAnswered();

        // Assert
        assertThat(events).containsExactly(
                new GoldCoinWon("toto", 6),
                new PlayerWon("toto"));
    }
}
