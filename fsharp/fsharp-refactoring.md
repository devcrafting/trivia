# F# refactoring towars a more functional style talk

Run `Fsi.exe GameRunner.fsx > goldenMaster.txt && git status goldenMaster.txt` in a Git Bash shell to check Golden Master is still ok.

## Introduction - 5 min

## At a first glance - 5 min

* Very ugly F# code : I like that in F#, it allows not pure functional code, but then it hurts your eyes
* I already created a Golden Master (sort of characterization test)
* Check how cool it is : VSCode + command line, even in the .NET world ;)

## Where could we start? - 15 min

* Have a look at GameRunner module : mutability of `notAWinner` and `aGame.roll` seems to do something, but what? Mutability again? Side effects?
* Have a look at Game type : even more mutability and side effects in methods...
* Let's cleanup a bit: create a Player record type for all these primitive obcession
    * Record type in F#: immutable class with default constructor and member equality (i.e a Value Object) + type inference
    * /!\ ToString() to replace /!\
    * `move` function uses "copy and update record expression"
* Live-code Name and Place property and then fast-forward other properties: `git reset --hard && git checkout -b step2 fsharp-refactoring-step2`
* Live-code QuestionsStack and Question record types (using temporary mutable QuestionsStack list in Game)

## Starting to remove mutability - 15 min

* Start with `addPlayer` instead of `Game.add`, introducing a discriminated union type
* Make `roll`, `wasCorrectlyAnswered` and `wrongAnswer` take and return a `GameState`
* Make `wasCorrectlyAnswered` and `wrongAnswer` change next player in GameState (temporary Won status to hide `isAWinner` mutable)
* Simplify `wasCorrectlyAnswered` (remove duplication)
* Encapsulate messages for `goToPenaltyBox`
