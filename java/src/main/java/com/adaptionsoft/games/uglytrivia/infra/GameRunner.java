
package com.adaptionsoft.games.uglytrivia.infra;

import com.adaptionsoft.games.uglytrivia.domain.Game;
import com.adaptionsoft.games.uglytrivia.domain.OutputWriter;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class GameRunner {

	private static OutputWriter output =
			lines -> lines.stream().forEach(System.out::println);
	private static boolean winner;

	public static void main(String[] args) {
		playGame(new Random());
	}

	public static void playGame(Random rand) {
		Game aGame = new Game(new GeneratedQuestions());

		output.write(aGame.add("Chet"));
		output.write(aGame.add("Pat"));
		output.write(aGame.add("Sue"));

		do {
			output.write(aGame.roll(rand.nextInt(5) + 1));

			List<Object> messages = new ArrayList<>();
			if (rand.nextInt(9) == 7) {
				winner = aGame.wrongAnswer(messages);
			} else {
				winner = aGame.wasCorrectlyAnswered(messages);
			}
			output.write(messages);
		} while (!winner);
	}
}
