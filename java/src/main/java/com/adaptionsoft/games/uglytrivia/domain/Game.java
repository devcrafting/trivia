package com.adaptionsoft.games.uglytrivia.domain;

import com.adaptionsoft.games.uglytrivia.domain.events.*;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Game {
    Players players = new Players();

    Questions questions;

    boolean isGettingOutOfPenaltyBox;

    public Game(Questions questions) {
        this.questions = questions;
    }

    public List<Object> add(String playerName) {
        return players.add(playerName);
    }

    public List<Object> roll(int roll) {
        List<Object> messages = new ArrayList<>();
        messages.add(new DiceRolled(players.getCurrentPlayer().name, roll));

        if (players.getCurrentPlayer().isInPenaltyBox()) {
            if (roll % 2 != 0) {
                isGettingOutOfPenaltyBox = true;

                messages.add(new GettingOutOfPenaltyBox(players.getCurrentPlayer().name));
                messages.addAll(moveAndAskQuestion(roll));
            } else {
                messages.add(new TryToGetOutOfPenaltyBoxFailed(players.getCurrentPlayer().name));
                isGettingOutOfPenaltyBox = false;
            }
        } else {
            messages.addAll(moveAndAskQuestion(roll));
        }
        return messages;
    }

    private List<Object> moveAndAskQuestion(int roll) {
        players.getCurrentPlayer().move(roll);
        int location = players.getCurrentPlayer().getLocation();
        Question question = questions.drawQuestion(location);
        return Arrays.asList(
            new PlayerMoved(players.getCurrentPlayer().name, location),
            new QuestionAsked(question));
    }

    public boolean wasCorrectlyAnswered(List<Object> messages) {
        if (players.getCurrentPlayer().isInPenaltyBox()) {
            if (isGettingOutOfPenaltyBox) {
                messages.add(players.getCurrentPlayer().winAGoldCoin());
            }
        } else {
            messages.add(players.getCurrentPlayer().winAGoldCoin());
        }
        return players.switchToNextPlayer(messages);
    }

    public boolean wrongAnswer(List<Object> messages) {
        messages.add(players.getCurrentPlayer().sendToPenaltyBox());
        return players.switchToNextPlayer(messages);
    }
}
