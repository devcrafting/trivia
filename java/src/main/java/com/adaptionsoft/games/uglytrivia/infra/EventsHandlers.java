package com.adaptionsoft.games.uglytrivia.infra;

import com.adaptionsoft.games.uglytrivia.domain.EventsPublisher;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.function.Consumer;

public class EventsHandlers implements EventsPublisher {
    private Map<Class, Consumer> handlers = new HashMap<>();

    @Override
    public <T> void register(Class<T> clazz, Consumer<T> handler) {
        handlers.put(clazz, handler);
    }

    @Override
    public void publish(List<Object> events) {
        events.forEach(l -> handlers.get(l.getClass()).accept(l));
    }
}
