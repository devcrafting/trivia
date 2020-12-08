<?php


namespace Trivia\Domain;


class GameServices implements GameFacade
{
    /**
     * @var GameRepository
     */
    private $gameRepository;
    /**
     * @var \Closure
     */
    private $println;

    public function __construct(GameRepository $gameRepository, \Closure $println)
    {
        $this->gameRepository = $gameRepository;
        $this->println = $println;
    }

    function newGame(): string
    {
        $game = new Game($this->println);
        $this->gameRepository->save($game);
        return uniqid();
    }

    function addPlayer(string $gameId, string $playerName)
    {
        $game = $this->gameRepository->get($gameId);
        $game->add($playerName);
        $this->gameRepository->save($game);
    }

    function roll(string $gameId)
    {
        $game = $this->gameRepository->get($gameId);
        $game->roll(rand(0, 5) + 1);
        $this->gameRepository->save($game);
    }
}