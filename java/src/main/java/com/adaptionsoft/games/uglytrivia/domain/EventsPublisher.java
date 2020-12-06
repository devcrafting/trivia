package com.adaptionsoft.games.uglytrivia.domain;

import java.util.List;
import java.util.function.Consumer;

public interface EventsPublisher {
    <T> void register(Class<T> clazz, Consumer<T> handler);
    void publish(List<Object> events);
}
