
package com.adaptionsoft.games.uglytrivia.infra;

import com.adaptionsoft.games.uglytrivia.domain.Game;
import com.adaptionsoft.games.uglytrivia.domain.EventsPublisher;
import com.adaptionsoft.games.uglytrivia.domain.events.PlayerWon;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class GameRunner {

	private static EventsPublisher eventsPublisher = new EventsHandlers();
	private static boolean winner;

	public static void main(String[] args) {
		playGame(new Random());
	}

	public static void playGame(Random rand) {
		new ConsoleWriter(eventsPublisher);
		Game aGame = new Game(new GeneratedQuestions());

		eventsPublisher.publish(aGame.add("Chet"));
		eventsPublisher.publish(aGame.add("Pat"));
		eventsPublisher.publish(aGame.add("Sue"));

		do {
			eventsPublisher.publish(aGame.roll(rand.nextInt(5) + 1));

			List<Object> messages;
			if (rand.nextInt(9) == 7) {
				messages = aGame.wrongAnswer();
			} else {
				messages = aGame.wasCorrectlyAnswered();
			}
			winner = messages.stream().anyMatch(e -> e instanceof PlayerWon);
			eventsPublisher.publish(messages);
		} while (!winner);
	}
}
