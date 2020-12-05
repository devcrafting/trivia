package com.adaptionsoft.games.uglytrivia;

import java.util.HashMap;
import java.util.LinkedList;
import java.util.Map;

public class Questions {
    private Map<Integer, LinkedList<Question>> questions = new HashMap<>();

    public Questions() {
        questions.put(0, new LinkedList<>());
        questions.put(1, new LinkedList<>());
        questions.put(2, new LinkedList<>());
        questions.put(3, new LinkedList<>());
        for (int i = 0; i < 50; i++) {
            questions.get(0).addLast(new Question("Pop", "Pop Question " + i));
            questions.get(1).addLast(new Question("Science", "Science Question " + i));
            questions.get(2).addLast(new Question("Sports", "Sports Question " + i));
            questions.get(3).addLast(new Question("Rock", "Rock Question " + i));
        }
    }

    public Question drawQuestion(int location) {
        return questions.get(location % 4).removeFirst();
    }
}
