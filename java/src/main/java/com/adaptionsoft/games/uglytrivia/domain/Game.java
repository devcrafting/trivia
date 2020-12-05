package com.adaptionsoft.games.uglytrivia.domain;

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

    public void roll(int roll) {
        System.out.println(players.getCurrentPlayer().name + " is the current player");
        System.out.println("They have rolled a " + roll);

        if (players.getCurrentPlayer().isInPenaltyBox()) {
            if (roll % 2 != 0) {
                isGettingOutOfPenaltyBox = true;

                System.out.println(players.getCurrentPlayer().name + " is getting out of the penalty box");
                moveAndAskQuestion(roll);
            } else {
                System.out.println(players.getCurrentPlayer().name + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox = false;
            }
        } else {
            moveAndAskQuestion(roll);
        }
    }

    private void moveAndAskQuestion(int roll) {
        players.getCurrentPlayer().move(roll);
        int location = players.getCurrentPlayer().getLocation();
        System.out.println(players.getCurrentPlayer().name
                + "'s new location is "
                + location);
        Question question = questions.drawQuestion(location);
        System.out.println("The category is " + question.category);
        System.out.println(question.text);
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
