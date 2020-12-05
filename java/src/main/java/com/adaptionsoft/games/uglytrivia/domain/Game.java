package com.adaptionsoft.games.uglytrivia.domain;

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
        messages.addAll(Arrays.asList(
                players.getCurrentPlayer().name + " is the current player",
                "They have rolled a " + roll));

        if (players.getCurrentPlayer().isInPenaltyBox()) {
            if (roll % 2 != 0) {
                isGettingOutOfPenaltyBox = true;

                messages.add(players.getCurrentPlayer().name + " is getting out of the penalty box");
                messages.addAll(moveAndAskQuestion(roll));
            } else {
                messages.add(players.getCurrentPlayer().name + " is not getting out of the penalty box");
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
            players.getCurrentPlayer().name
                    + "'s new location is "
                    + location,
            "The category is " + question.category,
            question.text);
    }

    public boolean wasCorrectlyAnswered(List<Object> messages) {
        if (players.getCurrentPlayer().isInPenaltyBox()) {
            if (isGettingOutOfPenaltyBox) {
                messages.add("Answer was correct!!!!");
                messages.add(players.getCurrentPlayer().winAGoldCoin());
            }
            return players.switchToNextPlayer();
        } else {
            messages.add("Answer was corrent!!!!");
            messages.add(players.getCurrentPlayer().winAGoldCoin());

            return players.switchToNextPlayer();
        }
    }

    public boolean wrongAnswer(List<Object> messages) {
        messages.add("Question was incorrectly answered");
        messages.add(players.getCurrentPlayer().name + " was sent to the penalty box");
        players.getCurrentPlayer().sendToPenaltyBox();

        return players.switchToNextPlayer();
    }
}
