package com.adaptionsoft.games.trivia;

import com.adaptionsoft.games.uglytrivia.infra.GameRunner;
import org.approvaltests.Approvals;
import org.junit.jupiter.api.Test;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.util.Random;
import java.util.stream.IntStream;

public class GoldenMaster {

	@Test
	public void should_not_change() {
		Random random = new Random(0);
		ByteArrayOutputStream resultStream = new ByteArrayOutputStream();
		System.setOut(new PrintStream(resultStream));

		IntStream.range(1,15).forEach(i -> GameRunner.playGame(random));

        Approvals.verify(resultStream.toString());
	}
}
