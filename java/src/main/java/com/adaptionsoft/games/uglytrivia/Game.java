package com.adaptionsoft.games.uglytrivia;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;

public class Game {
    List<Player> players = new ArrayList<>();

    LinkedList popQuestions = new LinkedList();
    LinkedList scienceQuestions = new LinkedList();
    LinkedList sportsQuestions = new LinkedList();
    LinkedList rockQuestions = new LinkedList();

    int currentPlayer = 0;
    boolean isGettingOutOfPenaltyBox;

    public Game() {
        for (int i = 0; i < 50; i++) {
            popQuestions.addLast("Pop Question " + i);
            scienceQuestions.addLast(("Science Question " + i));
            sportsQuestions.addLast(("Sports Question " + i));
            rockQuestions.addLast("Rock Question " + i);
        }
    }

    public boolean add(String playerName) {
        players.add(new Player(playerName));
        System.out.println(playerName + " was added");
        System.out.println("They are player number " + players.size());
        return true;
    }

    public int howManyPlayers() {
        return players.size();
    }

    public void roll(int roll) {
        System.out.println(players.get(currentPlayer).name + " is the current player");
        System.out.println("They have rolled a " + roll);

        if (players.get(currentPlayer).isInPenaltyBox()) {
            if (roll % 2 != 0) {
                isGettingOutOfPenaltyBox = true;

                System.out.println(players.get(currentPlayer).name + " is getting out of the penalty box");
                moveAndAskQuestion(roll);
            } else {
                System.out.println(players.get(currentPlayer).name + " is not getting out of the penalty box");
                isGettingOutOfPenaltyBox = false;
            }
        } else {
            moveAndAskQuestion(roll);
        }
    }

    private void moveAndAskQuestion(int roll) {
        players.get(currentPlayer).move(roll);
        System.out.println(players.get(currentPlayer).name
                + "'s new location is "
                + players.get(currentPlayer).getLocation());
        System.out.println("The category is " + currentCategory());
        askQuestion();
    }

    private void askQuestion() {
        if (currentCategory() == "Pop")
            System.out.println(popQuestions.removeFirst());
        if (currentCategory() == "Science")
            System.out.println(scienceQuestions.removeFirst());
        if (currentCategory() == "Sports")
            System.out.println(sportsQuestions.removeFirst());
        if (currentCategory() == "Rock")
            System.out.println(rockQuestions.removeFirst());
    }

    private String currentCategory() {
        if (players.get(currentPlayer).getLocation() % 4 == 0) return "Pop";
        if (players.get(currentPlayer).getLocation() % 4 == 1) return "Science";
        if (players.get(currentPlayer).getLocation() % 4 == 2) return "Sports";
        return "Rock";
    }

    public boolean wasCorrectlyAnswered() {
        if (players.get(currentPlayer).isInPenaltyBox()) {
            if (isGettingOutOfPenaltyBox) {
                System.out.println("Answer was correct!!!!");
                players.get(currentPlayer).winAGoldCoin();

                return switchToNextPlayer();
            } else {
                return switchToNextPlayer();
            }
        } else {
            System.out.println("Answer was corrent!!!!");
            players.get(currentPlayer).winAGoldCoin();

            return switchToNextPlayer();
        }
    }

    private boolean switchToNextPlayer() {
        boolean winner = players.get(currentPlayer).hasPlayerWon();
        currentPlayer++;
        if (currentPlayer == players.size()) currentPlayer = 0;

        return winner;
    }

    public boolean wrongAnswer() {
        System.out.println("Question was incorrectly answered");
        System.out.println(players.get(currentPlayer).name + " was sent to the penalty box");
        players.get(currentPlayer).sendToPenaltyBox();

        return switchToNextPlayer();
    }
}
