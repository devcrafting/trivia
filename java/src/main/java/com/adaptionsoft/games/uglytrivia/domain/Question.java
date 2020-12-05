package com.adaptionsoft.games.uglytrivia.domain;

public class Question {
    public final String category;
    public final String text;

    public Question(String category, String text) {
        this.category = category;
        this.text = text;
    }
}
