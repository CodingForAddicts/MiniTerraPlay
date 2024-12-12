# How to Play 🕹️

This documentation explains how to play the game, where you interact using a command file (`io.in`) to control actions and view game progress in the output file (`io.out`). All commands must be written in `io.in`, and the game processes them sequentially.

## Game Concept 🌍

You manage resources, construct buildings 🏗️, and aim to survive and grow through turns. Each building costs **5 Sestertius** 💰, and you must balance spending and resource generation. Menhirs 🪶 can be sold to earn Sestertius, with their price increasing due to random inflation 📈 every turn.

## Input File (`io.in`) 📥

The input file contains commands to perform specific actions. Commands are executed sequentially, with the game updating after each command.

## Available Commands 📋

1. **`next`** ➡️  
   Progresses the game to the next turn. Events like resource generation 🌾 and inflation adjustments 💸 occur during this phase.

2. **`buy`** 🛒  
   Purchases a building and places it on the grid.  
   After the `buy` command, the following inputs are required:
   - The building's name (e.g., `quarry`, `pond`, `forest`, etc.).
   - The coordinates (X, Y) where the building will be placed.  
   Each building costs **5 Sestertius** 💰.

3. **`sell`** 💸  
   Used **only** to sell **Menhirs** 🪶.  
   - Menhirs are sold at a **base price of 5 Sestertius per Menhir**, which increases every turn due to **random inflation** 📈.
   - Example: On Turn 0, each Menhir sells for 5 Sestertius. By Turn 5, due to inflation, the price might increase to 8 Sestertius or higher per Menhir.
   - Inflation is recalculated randomly at the start of every turn.
   - Example: Typing `sell 2` in `io.in` would sell 2 Menhirs at the current inflated price (e.g., 2 × 8 = 16 Sestertius).

4. **`exit`** 🚪  
   Ends the game prematurely.

## Buildings and Their Effects 🏛️

| **Building** | **Effect**             |
|--------------|-------------------------|
| Quarry       | Produces Menhirs 🪶     |
| Sculptor     | Produces Menhirs faster 🪶✨ |
| Pond         | Produces Food 🍽️       |
| Forest       | Provides natural growth 🌳 |
| Hunter       | Gathers Food 🍖        |

> **Note**: All buildings cost **5 Sestertius** 💰.

## Output File (`io.out`) 📤

The game writes the state after processing each command to `io.out`. The state includes:
1. A **map** 🗺️ showing the grid with placed buildings.
2. **Resources** 💰 (Money), 🪶 (Menhirs), and 🍖 (Food).
3. The current **Turn** ⏳.
4. The **current selling price of Menhirs** 🪶 due to inflation 📈.

#### Example Output:
```plaintext
-----------------
|h             p|
|               |
|   Qpf         |
|               |
|q             f|
-----------------
Money: 30 Sestertius
Menhirs: 6
Food: 0
Turn: 8
Current Menhir Price: 8 Sestertius

buy/sell/next/exit
