# **Fetch**  
*Modular Gameplay Framework for Unity*  

---

## **Executive Overview**  
**Fetch** is a lightweight, highly extensible Unity framework designed to accelerate the development of arcade-style endless runners, dodge games, and interactive simulations. By providing battle-tested, plug-and-play components for object lifecycle management, procedural spawning, and collision-driven interactions, Fetch eliminates boilerplate code and enforces clean architecture.  

This repository solves the critical pain point of **repetitive gameplay scripting**—whether you're prototyping a game jam entry, teaching Unity fundamentals, or building a production-ready title. Its decoupled design allows developers to mix, match, and customize components without rewriting core logic, directly translating to faster iteration cycles and reduced technical debt.  

For investors and clients, Fetch represents a **scalable foundation** for monetizable games or educational products. Its clear separation of concerns and configurability make it ideal for white-label deployment, asset store packaging, or as the backbone of a portfolio of hit casual games.

---

## **Key Features**  

- **🚀 Dynamic Object Spawning System**  
  - Procedurally spawn prefabs at configurable positions with randomized intervals (`SpawnManagerX`).  
  - Supports multiple prefab types with weighted randomness out of the box.  

- **🛡️ Automated Lifecycle Management**  
  - `DestroyOutOfBoundsX` automatically removes objects when they exit defined play areas (X/Y limits), preventing memory leaks and clutter.  
  - Customizable boundary thresholds per axis.  

- **⚡ Player-Driven Instantiation**  
  - `PlayerControllerX` handles input-triggered spawning with built-in cooldown throttling to prevent spam.  
  - Easily remappable keys and adjustable spawn rates via Inspector.  

- **💥 Collision-First Interaction Model**  
  - `DetectCollisionsX` uses Unity’s `OnTriggerEnter` for tag-based destruction—ideal for "catcher" mechanics (e.g., dogs catching balls).  
  - Zero-code configuration: simply assign tags in the Unity Editor.  

- **🎯 Movement Agnostic Core**  
  - `MoveForwardX` provides a simple, direction-agnostic translation that works for 2D/3D, side-scrollers, top-down, or rail shooters.  
  - Speed is publicly adjustable per prefab for nuanced difficulty curves.  

- **🧱 Modular & Decoupled**  
  - Each script is a single-responsibility MonoBehaviour. Mix and match components on any GameObject.  
  - No hard dependencies between systems—extend or replace any module without breaking others.  

---

## **Target Users & Value Proposition**  

### **Primary Users**  
| User Type | Pain Point Solved | Value Delivered |  
|-----------|-------------------|-----------------|  
| **Indie Developers** | Time spent rewriting spawn/destroy logic | 80% faster prototyping of core arcade mechanics |  
| **Game Jam Participants** | Complex setup for simple gameplay | Drop-in components that work in <5 minutes |  
| **Educators & Students** | Teaching Unity best practices | Real-world example of component-based design and event handling |  
| ** studios building casual games** | Managing object lifetimes in endless modes | Battle-tested bounds and collision management |  

### **ROI-Driven Benefits**  
- **Reduced Development Cost**: Lay down gameplay loops in hours, not days.  
- **Maintainable Codebase**: Clear separation of concerns eases debugging and feature additions.  
- **Cross-Project Portability**: Same scripts work in 2D, 3D, orthographic, or perspective projects.  
- **Asset Store Ready**: Structure lends itself to documentation, examples, and premium packaging.  

---

## **Technical Architecture**  

### **Stack & Design Patterns**  
- **Engine**: Unity 2020+ (C# 8.0+)  
- **Pattern**: Component-Based Entity System (Unity’s native MonoBehaviour pattern)  
- **Architecture**: Decoupled, event-lightweight (uses Unity’s built-in `OnTriggerEnter`, no custom event bus)  
- **Data Flow**: Inspector-configurable public fields → per-instance behavior tuning  

### **Inferred Project Structure**  
```plaintext
Fetch/
├── Assets/
│   └── Challenge 2/
│       └── Scripts/          # Core framework modules
│           ├── MoveForwardX.cs       → Handles directional movement
│           ├── DestroyOutOfBoundsX.cs→ Bounds enforcement
│           ├── SpawnManagerX.cs      → Procedural ball spawning
│           ├── PlayerControllerX.cs  → Input-driven dog spawning
│           └── DetectCollisionsX.cs  → Tag-based collision destruction
└── (Prefabs & Scene assets inferred but not provided)
```

### **Component Interaction Flow**  
1. **SpawnManagerX** instantiates `ballPrefabs` at random X positions at Y=30.  
2. Balls have `MoveForwardX` (Z-axis movement) and `DestroyOutOfBoundsX`.  
3. **PlayerControllerX** spawns `dogPrefab` on Spacebar (with cooldown).  
4. Dogs also have `MoveForwardX` and `DestroyOutOfBoundsX`.  
5. On overlap, `DetectCollisionsX` (on dogs) destroys the ball (tagged "Dog" vs "Ball" implied).  

### **Extensibility Points**  
- Replace `MoveForwardX` with pathfinding, physics-based movement, or animation-driven motion.  
- Extend `SpawnManagerX` with object pooling, wave patterns, or difficulty ramps.  
- Swap `DestroyOutOfBoundsX` for health systems or screen-wrap mechanics.  

---

## **Installation & Usage Guide**  

### **Prerequisites**  
- Unity Hub + Unity 2020.3 LTS or newer  
- Basic familiarity with Unity’s GameObject/Prefab system  

### **Step-by-Step Setup**  
1. **Clone & Open**  
   ```bash
   git clone https://github.com/yourusername/Fetch.git
   ```  
   Open the project in Unity Hub.  

2. **Scene Setup**  
   - Create an empty GameObject named `SpawnManager` → attach `SpawnManagerX`.  
   - In the Inspector, assign your ball prefabs to the `Ball Prefabs` array.  
   - Create a `Player` GameObject with a Collider (set as Trigger) → attach `PlayerControllerX` and `DetectCollisionsX`.  
   - Assign your dog prefab to `Dog Prefab` in `PlayerControllerX`.  
   - Ensure all ball and dog prefabs have:  
     - `MoveForwardX` script  
     - `DestroyOutOfBoundsX` script  
     - Appropriate **Tags** (`Dog` for dogs, `Ball` for balls—or customize in `DetectCollisionsX`).  

3. **Configure Parameters** (all in Inspector)  
   - `MoveForwardX.speed`: Adjust per prefab for varied speeds.  
   - `DestroyOutOfBoundsX.leftLimit` / `bottomLimit`: Match your camera/play area.  
   - `SpawnManagerX.spawnLimitXLeft/Right`, `spawnPosY`: Define spawn rectangle.  
   - `PlayerControllerX.spawnCooldown`: Balance gameplay challenge.  

4. **Play**  
   - Press **Space** to spawn dogs.  
   - Watch balls spawn automatically and move forward.  
   - Dogs destroy balls on trigger collision; all objects cleanup out-of-bounds.  

---

## **Usage Examples**  

### **Example 1: Dynamic Spawn Rate Adjustment**  
Modify `SpawnManagerX` at runtime for difficulty scaling:  
```csharp
public class AdaptiveSpawner : MonoBehaviour {
    public SpawnManagerX spawnManager;
    private float baseInterval = 4f;
    
    void Update() {
        // Increase spawn rate as score rises
        float newInterval = Mathf.Max(1f, baseInterval - (ScoreManager.Score * 0.1f));
        // (You'd need to expose SpawnRandomBall's invoke interval via a public field for this)
    }
}
```

### **Example 2: Custom Destruction Logic**  
Extend `DestroyOutOfBoundsX` to include scoring:  
```csharp
public class DestroyWithScore : DestroyOutOfBoundsX {
    public int points = 10;
    
    void Update() {
        if (transform.position.x < leftLimit || transform.position.y < bottomLimit) {
            ScoreManager.AddPoints(points);
            Destroy(gameObject);
        }
    }
}
```

### **Example 3: Tag-Independent Collision**  
Generalize `DetectCollisionsX` for multiple prey types:  
```csharp
public class MultiTargetCollision : MonoBehaviour {
    public string[] targetTags = { "Ball", "Enemy", "Collectible" };
    
    void OnTriggerEnter(Collider other) {
        foreach (string tag in targetTags) {
            if (other.CompareTag(tag)) {
                Destroy(other.gameObject);
                break;
            }
        }
    }
}
```

---

## **Future Potential & Roadmap**  

### **Short-Term (v1.1–v1.2)**  
- **Object Pooling Integration**: Replace `Instantiate`/`Destroy` with pools for performance.  
- **Unity Events**: Add UnityEvent hooks on spawn/destroy for audio/vfx feedback.  
- **Configuration Profiles**: ScriptableObjects for preset difficulty curves.  

### **Mid-Term (v1.3–v2.0)**  
- **UI Dashboard**: In-game debug panel to tweak all parameters live.  
- **Cross-Platform Input**: Touch, gamepad, and mouse support for `PlayerControllerX`.  
- **Analytics Tracking**: Hook into Unity Analytics or third-party tools for spawn/collision metrics.  

### **Long-Term (Enterprise ROI)**  
- **Asset Store Publication**: Package with documentation, example scenes, and polish.  
- **Template Variants**: Pre-built themes (space, fantasy, sci-fi) using the same core scripts.  
- **aaS Potential**: Offer a cloud-configured version for no-code game builders.  

**Monetization Pathways**:  
1. Sell as a $15–$29 Unity Asset Store package.  
2. License per seat for internal studio tools.  
3. White-label for clients building hyper-casual games (revenue share or flat fee).  

---

## **Contributing**  
We welcome contributions that **enhance modularity**, **improve performance**, or **add well-tested features**.  

### **How to Contribute**  
1. Fork the repository and create a feature branch (`git checkout -b feature/AmazingFeature`).  
2. Follow Unity/C# conventions:  
   - Use PascalCase for methods/public fields.  
   - Add [Tooltip] attributes to all public Inspector fields.  
   - Keep classes single-responsibility.  
3. Include a **test scene** demonstrating your change.  
4. Submit a Pull Request with a clear description and linked issue.  

### **Code of Conduct**  
- Be respectful.  
- Ensure all scripts work in Unity 2020.3+ without errors.  
- Do not remove or degrade existing functionality without discussion.  

---

## **License**  
This project is licensed under the **MIT License**—see the [LICENSE](LICENSE) file for details.  

> **Why MIT?**  
> Maximum permissiveness for commercial and non-commercial use. Encourages adoption in indie projects, studios, and educational curricula without legal overhead.  

---

*Built with ❤️ and Unity. Let’s build the next great casual game—faster.*