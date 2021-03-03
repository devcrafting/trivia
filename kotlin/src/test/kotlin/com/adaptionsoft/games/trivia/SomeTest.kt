package com.adaptionsoft.games.trivia

import com.adaptionsoft.games.trivia.runner.playGame
import org.approvaltests.Approvals
import org.junit.jupiter.api.Test
import java.io.ByteArrayOutputStream
import java.io.PrintStream
import java.util.*

class SomeTest {

    @Test
    fun `it is locked down`() {
        val out = ByteArrayOutputStream()
        System.setOut(PrintStream(out))
        (1..10).forEach { playGame(Random(it.toLong())) }
        Approvals.verify(out.toString())
    }
}
