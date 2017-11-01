# F# refactoring towars a more functional style talk

## At a first glance

* Very ugly F# code : I like that in F#, it allows not pure functional code, but then it hurts your eyes
* I already created a Golden Master (sort of characterization test)
* Check how cool it is : VSCode + command line, even in the .NET world ;)

## Where could we start?

* Have a look at GameRunner module : mutability of notAWinner and aGame.roll seems to do something, but what? Mutability again? Side effects?
* Have a look at Game type : even more mutability and side effects in methods...

## 