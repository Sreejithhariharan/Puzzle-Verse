# 🎨 Anime Sliding Puzzle

**A timed, progressive puzzle game with anime-themed visuals and engaging gameplay mechanics.**

---

## 🎮 About the Game

Anime Sliding Puzzle is a timed sliding puzzle game that challenges players to arrange tiles in the correct order against the clock. With multiple levels, increasing difficulty, and beautiful anime-themed visuals, it's an engaging brain-teaser that keeps players coming back.

**Play it now:** [Play on Itch.io](https://sreejith-hariharan.itch.io/)

---

## ✨ Features

### Gameplay
- 🧩 **Progressive Difficulty** - Multiple levels with increasing complexity
- ⏱️ **Timed Challenges** - Race against the clock for bonus points
- 🎯 **Level Progression** - Unlock new puzzles as you advance
- 📱 **Responsive Controls** - Intuitive touch and mouse controls

### Visual & Audio
- 🎨 **Anime-Themed Aesthetics** - Beautiful, themed graphics
- 🔊 **Sound Design** - Immersive audio feedback
- ✨ **Visual Polish** - Smooth animations and visual effects
- 🎭 **UI Feedback** - Clear, intuitive user interface

### Systems Implemented
- ⏰ Timer management system
- 📈 Difficulty scaling algorithm
- 💾 Game state management
- 🎮 Input handling (touch & mouse)

---

## 🛠️ Technical Details

### Technology Stack
- **Engine:** Unity 3D
- **Language:** C#
- **Platform:** WebGL / Standalone
- **Physics:** 2D (Light)

### Architecture
```
Assets/
├── Script/
│   ├── Managers/              # Core game management systems
│   ├── Music/                 # Audio and music assets
│   ├── Puzzle/                # Puzzle mechanics and logic
│   ├── UI/                    # User interface components
│   └── Recovery/              # Recovery and error handling
├── Material/                  # Shaders and materials
│   ├── Bleach/
│   ├── Materials/
│   ├── Naruto/
│   └── OnePiece/
├── Fonts/                     # Text assets
├── Prefabs/                   # Reusable game objects
└── Scenes/                    # Game scenes
```

### Key Systems

#### Timer System
```csharp
// Countdown timer with event callbacks
- Tracks remaining time
- Triggers warnings at thresholds
- Handles game-over on timeout
```

#### Puzzle Solver
```csharp
// Algorithm for puzzle generation & validation
- Generates solvable puzzle configurations
- Validates player moves
- Calculates optimal solutions
```

#### Score Calculation
```csharp
// Dynamic scoring based on performance
- Base points for completion
- Time bonus (faster = higher score)
- Difficulty multiplier
- Combo bonuses
```

---

## 🎮 Gameplay Mechanics

1. **Puzzle Board:** Click/tap tiles adjacent to the empty space to slide them
2. **Timer:** Complete the puzzle before time runs out
3. **Difficulty Levels:** Each level increases in complexity
4. **Progression:** Unlock new puzzles and themes

---

## 📦 How to Play

1. Play from [Itch.io](https://sreejith-hariharan.itch.io/)
2. Extract and run the executable
3. Click on tiles next to the empty space to move them
4. Arrange all tiles in numerical order
5. Beat the timer to maximize your score!

---

## 🚀 Installation & Setup (Development)

### Prerequisites
- Unity 2022.3 or higher
- Visual Studio or VS Code
- C# 9.0 or later

### Clone & Setup
```bash
git clone https://github.com/sreejithhariharan/anime-sliding-puzzle.git
cd anime-sliding-puzzle
open AnimeSlidingPuzzle.sln
```

### Building
- **WebGL:** File → Build Settings → Switch to WebGL → Build
- **Standalone:** File → Build Settings → Select Platform → Build

---

## 📊 Development Stats

| Metric | Value |
|--------|-------|
| Development Time | ~1 months |
| Lines of Code | 1,500+ |
| Game Scenes | 3 |
| Levels | 5+ |
| Supported Platforms | WebGL, Windows, Mac |
| Target Audience | Casual Puzzle Gamers |

---

## 🎓 Learning Outcomes

Building this game taught me:
- ✅ Timer and state management systems
- ✅ Dynamic difficulty scaling
- ✅ Touch input handling and validation
- ✅ UI/UX implementation in Unity
- ✅ Game loop optimization
- ✅ Audio system integration
- ✅ Publishing on Itch.io

---

## 🔮 Future Enhancements

- [ ] Leaderboard system (online ranking)
- [ ] Additional themes and visuals
- [ ] Daily challenges
- [ ] Social sharing features
- [ ] Multiplayer mode
- [ ] Mobile app version (iOS/Android)

---

## 📝 Credits

- **Developer:** Sreejith Hariharan
- **Game Design & Programming:** Sreejith Hariharan
- **Art & Visual Design:** Anime-themed asset packs
- **Sound Design:** Game audio libraries

---

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

## 🤝 Contributing

Interested in contributing? I'd love feedback and suggestions!
- Found a bug? Open an issue
- Have ideas? Feel free to suggest enhancements
- Want to collaborate? Reach out at sreejithhari9@gmail.com

---

## 📞 Contact & Links

- 🐙 GitHub: [github.com/sreejithhariharan](https://github.com/Sreejithhariharan)
- 🎮 Itch.io: [itch.io/@SreejithHariharan](https://sreejith-hariharan.itch.io/)
- 📧 Email: sreejithhari9@gmail.com
- 📱 Phone: +91 7736308911

---

## ⭐ Support

If you enjoy this game, please:
- ⭐ Star this repository
- 💬 Leave feedback on Itch.io
- 🔗 Share with friends
- 📬 Follow for future projects

---

**Developed with ❤️ using Unity | Last Updated: September 2026**
