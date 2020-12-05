
package com.adaptionsoft.games.uglytrivia.infra;

import com.adaptionsoft.games.uglytrivia.domain.Game;

import java.util.Random;

public class GameRunner {

	private static boolean winner;

	public static void main(String[] args) {
		playGame(new Random());
	}

	public static void playGame(Random rand) {
		Game aGame = new Game(new GeneratedQuestions());

		aGame.add("Chet");
		aGame.add("Pat");
		aGame.add("Sue");

		do {
			aGame.roll(rand.nextInt(5) + 1);

			if (rand.nextInt(9) == 7) {
				winner = aGame.wrongAnswer();
			} else {
				winner = aGame.wasCorrectlyAnswered();
			}
		} while (!winner);
	}
}
