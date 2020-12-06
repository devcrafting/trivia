package com.adaptionsoft.games.uglytrivia.domain;

import com.adaptionsoft.games.uglytrivia.domain.events.*;
import com.adaptionsoft.games.uglytrivia.infra.GeneratedQuestions;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.UUID;

public class Game {
    private UUID gameId;
    Players players = new Players();

    Questions questions;

    boolean isGettingOutOfPenaltyBox;

    public Game(Questions questions) {
        this.questions = questions;
    }

    public Game(UUID gameId, Players players, Questions questions) {
        this(questions);
        this.gameId = gameId;
        this.players = players;
    }

    public static Game loadState(
            UUID gameId,
            Players players,
            GeneratedQuestions generatedQuestions) {
        return new Game(gameId, players, generatedQuestions);
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

    public List<Object> wasCorrectlyAnswered() {
        ArrayList<Object> events = new ArrayList<>();
        if (players.getCurrentPlayer().isInPenaltyBox()) {
            if (isGettingOutOfPenaltyBox) {
                events.add(players.getCurrentPlayer().winAGoldCoin());
            }
        } else {
            events.add(players.getCurrentPlayer().winAGoldCoin());
        }
        events.addAll(players.switchToNextPlayer());
        return events;
    }

    public List<Object> wrongAnswer() {
        ArrayList<Object> events = new ArrayList<>();
        events.add(players.getCurrentPlayer().sendToPenaltyBox());
        events.addAll(players.switchToNextPlayer());
        return events;
    }
}
