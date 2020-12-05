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

    public List<String> add(String playerName) {
        return players.add(playerName);
    }

    public List<String> roll(int roll) {
        List<String> messages = new ArrayList<>();
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

    private List<String> moveAndAskQuestion(int roll) {
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

    public boolean wasCorrectlyAnswered() {
        if (players.getCurrentPlayer().isInPenaltyBox()) {
            if (isGettingOutOfPenaltyBox) {
                System.out.println("Answer was correct!!!!");
                players.getCurrentPlayer().winAGoldCoin();
            }
            return players.switchToNextPlayer();
        } else {
            System.out.println("Answer was corrent!!!!");
            players.getCurrentPlayer().winAGoldCoin();

            return players.switchToNextPlayer();
        }
    }

    public boolean wrongAnswer() {
        System.out.println("Question was incorrectly answered");
        System.out.println(players.getCurrentPlayer().name + " was sent to the penalty box");
        players.getCurrentPlayer().sendToPenaltyBox();

        return players.switchToNextPlayer();
    }
}
