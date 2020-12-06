package com.adaptionsoft.games.uglytrivia.domain.events;

import com.adaptionsoft.games.uglytrivia.domain.Question;

public class QuestionAsked {
    public final Question question;

    public QuestionAsked(Question question) {
        this.question = question;
    }
}
