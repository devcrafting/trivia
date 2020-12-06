package com.adaptionsoft.games.uglytrivia.infra;

import com.adaptionsoft.games.uglytrivia.domain.EventsPublisher;
import com.adaptionsoft.games.uglytrivia.domain.events.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.function.Consumer;

public class ConsoleWriter {
    public ConsoleWriter(EventsPublisher eventsPublisher) {
        eventsPublisher.register(PlayerAdded.class, this::handle);
        eventsPublisher.register(DiceRolled.class, this::handle);
        eventsPublisher.register(GettingOutOfPenaltyBox.class, this::handle);
        eventsPublisher.register(TryToGetOutOfPenaltyBoxFailed.class, this::handle);
        eventsPublisher.register(PlayerMoved.class, this::handle);
        eventsPublisher.register(QuestionAsked.class, this::handle);
        eventsPublisher.register(GoldCoinWon.class, this::handle);
        eventsPublisher.register(SentToPenaltyBox.class, this::handle);
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

    private void handle(SentToPenaltyBox event) {
        System.out.println("Question was incorrectly answered");
        System.out.println(event.playerName + " was sent to the penalty box");
    }
}
