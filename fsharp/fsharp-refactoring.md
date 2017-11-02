# F# refactoring towars a more functional style talk

Run `Fsi.exe GameRunner.fsx > goldenMaster.txt && git status goldenMaster.txt` in a Git Bash shell to check Golden Master is still ok.

## Introduction - 5 min

## At a first glance - 5 min

Start here : `git reset --hard && git checkout -b step1 fsharp-refactoring-start`

* Very ugly F# code : I like that in F#, it allows not pure functional code, but then it hurts your eyes
* I already created a Golden Master (sort of characterization test)
* Check how cool it is : VSCode + command line, even in the .NET world ;)

## Where could we start? - 15 min

* Have a look at GameRunner module : mutability of `notAWinner` and `aGame.roll` seems to do something, but what? Mutability again? Side effects?
* Have a look at Game type : even more mutability and side effects in methods...
* Let's cleanup a bit: create a Player record type for all these primitive obcession
    * Record type in F#: immutable class with default constructor and member equality (i.e a Value Object) + type inference
* Live-code Name and Place property and then fast-forward : `git reset --hard && git checkout -b step2 fsharp-refactoring-firsttypes`
    * /!\ ToString() to replace /!\
    * `move` function uses "copy and update record expression" (move printfn also)
    * `winAGoldCoin` function to increment `GoldCoins` (move printfn also)
    * QuestionsStack and Question record types (using temporary mutable QuestionsStack list in Game)
    * Use Category instead of Name (avoid properties collision)
    * `generateQuestions` function [1..50] |> fun i -> ...
    * `askAndDiscardQuestion` function taking location and all questions stacks

## Starting to remove mutability - 25 min

* Start with `addPlayer` instead of `Game.add`, introducing a discriminated union type
* Simplify `wasCorrectlyAnswered` (remove duplication around return type) and make emerge need for another return type (new GameState : `Won` or `Playing` with new turn prepared => `nextTurn` function)
* Use recursion instead of while loop with mutable value in `GameRunner` + add `nextTurn` function handling `Won` case or giving next turn `Playing` case
    * Shortcut: `git reset --hard && git checkout -b step3 fsharp-refactoring-recursive`

## Additional content

* Remove use of mutable `questionsStacks` in `Game`, use `GameTurn.QuestionsStacks` instead
    * See fsharp-refactoring-no-mutability-1: `git reset --hard && git checkout -b step4 fsharp-refactoring-no-mutability-1`
* Add new functions `roll`, `answerCorrectly`, `answerBadly` used in place of `Game` class (removed)
    * See fsharp-refactoring-no-mutability-2: `git reset --hard && git checkout -b step5 fsharp-refactoring-no-mutability-2`
