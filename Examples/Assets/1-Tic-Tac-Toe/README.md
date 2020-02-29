# Example 1: Tic-Tac-Toe

This is a simple example demonstrating usage of AReSSO on a small turn based game. I'll assume everyone knows how tic-tac-toe works.

## Dependencies

This example depends on AReSSO and the UniRx library.

## Where to start

The center of this example is `TicTacToeStore` and `TicTacToeState`. These form the backbone that everything else builds off of. Take a quick look at `TicTacToeState` to see how the state is organized.

### Reducers

It is probably best to start at the reducers and follow the flow throughout the program. The `RootReducer` is defined in `TicTacToeStore`. The root reducer reacts to actions in order to modify state.

There are two action types in this example, `NewGameAction` and `TileClickedAction`. The root reducer handles `NewGameAction` simply by returning a brand new game state. It handles `TileClickedAction` by delegating to `TileClickedReducer.Reduce`.

`TileClickedReducer.Reduce` itself delegates to 3 sub-reducers. Each sub-reducer is responsible for changing one bit of state:

- `NextBoard` takes the current board, current player, and location of the tile click action and spits out the next board state.
- `DetermineWinner` takes a given board state and determines if either player has won or if there has been a tie.
- Finally, `NextPlayer` takes the current player and returns who the next player should be.

Notice that these sub reducers don't modify state, they simply return the next value for their little slice of state. `TileClickedReducer.Reduce` aggregates the results from each of these to compile the final next state.

Once these reducers are done, the final result goes back up to the root reducer and set within the store. The store then moves on to notifying consumers.

### Consumers and Selectors

There are three consumers that observe changes to state in this example:

- `BoardTile` observes changes to a particular location in the board and updates the visual representation of that location.
- `TurnIndicator` observes the current player and displays a UI element conveying the current player to the user.
- `WinModal` monitors the win state and pops up a dialog box saying who the winner is.

Each of these consumers aquire an `IObservable` of the state from the Store, then filter it through a selector. Selectors are functions that narrow down the global state to get a particular piece of state that a consumer is interested in. The selectors can be found in `Select.cs`. They're very simple, but they play a vital role in maintaining separation of concerns: consumers should not care where the data they are interested in is stored. If the structure or organization of the state object changes, consumers should not need to change.

Each of these consumers is extremely simple. They very straightforwardly take state and update the UI to match. All of the logic happens in the reducers!

### Actions

Actions are really what kicks off change in the store. As mentioned earlier, there are two actions in this example: `NewGameAction` and `TileClickedAction`. These are pretty much self explanatory. Actions can hold data that is necessary to act on them, for instance the `TileClickedAction` contains information about which tile was clicked.

These Actions are dispatched to the Store by `ClickableBoardTile` and `NewGameButton` respectively. These objects simply listen for unity events fired when their corresponding ui element is clicked and dispatch an action to the store when the event is fired.

## That's Tic Tac Toe!

Comb through the code in the order specified here. The individual pieces of code should be recognizable; hopefully the intricacies of implementation don't obscure the architecture.
