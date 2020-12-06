package com.adaptionsoft.games.uglytrivia.infra;

import com.adaptionsoft.games.uglytrivia.domain.OutputWriter;
import com.adaptionsoft.games.uglytrivia.domain.events.PlayerAdded;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.function.Consumer;

public class ConsoleWriter implements OutputWriter {
    private Map<Class, Consumer> handlers = new HashMap<>();

    public ConsoleWriter() {
        handlers.put(String.class, System.out::println);
        register(PlayerAdded.class, this::handle);
    }

    private <T> void register(Class<T> clazz, Consumer<T> handler) {
        handlers.put(clazz, handler);
    }

    private void handle(PlayerAdded event) {
        System.out.println(event.name + " was added");
        System.out.println("They are player number " + event.numberOfPlayers);
    }

    @Override
    public void write(List<Object> lines) {
        lines.forEach(l -> handlers.get(l.getClass()).accept(l));
    }
}
