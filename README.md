# Project Eteos

> A simulation-first game built around realistic, interconnected systems.

Project Eteos is an experimental simulation-first game focused on creating a world governed by physical and engineering principles rather than predefined game logic.

The goal is to build systems that can be **constructed, modified, tested, broken, and combined**, allowing complex behavior to emerge from their underlying rules.

## Vision

Eteos aims to simulate interconnected systems such as:

* ⚙️ Mechanics and rigid-body systems
* ⚡ Electricity and electronics
* 🔥 Heat and thermodynamics
* 💧 Fluids and pressure
* 🎛️ Control systems
* 🧠 Computing and programmable components
* 🧬 Biology and human responses
* 🔗 Interactions between all of the above

The simulation is the foundation. The game exists to give the simulation a reason to be used.

## Modes

### Sandbox

A place to freely experiment with the simulation.

Build systems, modify components, apply extreme conditions, break things, and investigate what happens.

### Game

A structured experience built on top of the same underlying simulation.

Players will have to design, operate, repair, and survive systems while dealing with realistic constraints and consequences.

## Design Philosophy

**Simulation first.**

The simulation should not depend on the presentation layer. Physics and system behavior should be able to operate independently from graphics, UI, and gameplay.

The project will prioritize:

* Accurate and understandable models
* Modular systems
* Deterministic simulation where practical
* Data-driven components
* Extensibility
* Experimentation
* Emergent behavior

## Development Status

🚧 **Early Development**

The project is currently focused on establishing the simulation architecture and core systems.

Nothing is considered final yet.

## Technology

* **Engine:** Godot
* **Language:** C#
* **Target:** Desktop
* **License:** GNU GPLv3

## License

Project Eteos is licensed under the **GNU General Public License v3.0**.

See [`LICENSE`](LICENSE) for the full license.

This project is intended to remain open source. Derivative works distributed under the GPL must comply with the license's requirements.

## Project Structure

The project is organized around the simulation rather than the game presentation:

```text
Simulation/
├── Physics/
├── Electrical/
├── Thermal/
├── Fluid/
├── Control/
├── Biology/
└── Core/

Game/
├── Gameplay/
├── World/
└── UI/

Data/
Tests/
Docs/
```

## Long-Term Goal

Build a simulation where the player doesn't need to ask:

> "What does the game allow me to do?"

but instead:

> **"What would actually happen if I did this?"**
