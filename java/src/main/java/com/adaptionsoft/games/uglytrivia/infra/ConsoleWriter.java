package com.adaptionsoft.games.uglytrivia.infra;

import com.adaptionsoft.games.uglytrivia.domain.OutputWriter;
import com.adaptionsoft.games.uglytrivia.domain.events.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.function.Consumer;

public class ConsoleWriter implements OutputWriter {
    private Map<Class, Consumer> handlers = new HashMap<>();

    public ConsoleWriter() {
        handlers.put(String.class, System.out::println);
        register(PlayerAdded.class, this::handle);
        register(DiceRolled.class, this::handle);
        register(GettingOutOfPenaltyBox.class, this::handle);
        register(TryToGetOutOfPenaltyBoxFailed.class, this::handle);
        register(PlayerMoved.class, this::handle);
        register(QuestionAsked.class, this::handle);
        register(GoldCoinWon.class, this::handle);
    }

    private <T> void register(Class<T> clazz, Consumer<T> handler) {
        handlers.put(clazz, handler);
    }

    private void handle(PlayerAdded event) {
        System.out.println(event.name + " was added");
        System.out.println("They are player number " + event.numberOfPlayers);
    }

    private void handle(DiceRolled event) {
        System.out.println(event.playerName + " is the current player");
        System.out.println("They have rolled a " + event.roll);
    }

    private void handle(GettingOutOfPenaltyBox event) {
        System.out.println(event.playerName + " is getting out of the penalty box");
    }

    private void handle(TryToGetOutOfPenaltyBoxFailed event) {
        System.out.println(event.playerName + " is not getting out of the penalty box");
    }

    private void handle(PlayerMoved event) {
        System.out.println(event.playerName
                + "'s new location is "
                + event.newLocation);
    }

    private void handle(QuestionAsked event) {
        System.out.println("The category is " + event.question.category);
        System.out.println(event.question.text);
    }

    private void handle(GoldCoinWon event) {
        System.out.println("Answer was correct!!!!");
        System.out.println(event.playerName
                + " now has "
                + event.goldCoins
                + " Gold Coins.");
    }

    @Override
    public void write(List<Object> lines) {
        lines.forEach(l -> handlers.get(l.getClass()).accept(l));
    }
}
