# Playbook A · 躲避与收集

可靠性：最高。团队疲惫或主题模糊时的安全选项。

> 与英文版的同步说明：本 .zh.md 在 .en.md 更新时同步翻译。字段顺序和编号严格一致。

## 1. 一句话定义

玩家在夜空空间里控制一个角色移动，收集下落的星星获得分数，同时躲避会直接结束本局的下落威胁。

## 2. 三层核心循环

- **10 秒**：测试者学会 WASD / 方向键控制角色移动，碰到星星加分。
- **60 秒**：威胁物开始和星星一起下落；测试者开始做"这个星值不值得冒险去拿"的小决策。
- **3 分钟**：难度曲线把生成速率拉高；测试者在追打自己的最高分，抓取越来越冒险。

## 3. 成功与失败条件

- **成功**：没有"通关"概念，纯分数驱动。"成功" = GameOver 页面上超过之前最高分。
- **失败**：与一个威胁发生一次碰撞 → Game Over。

## 4. 使用的工具包模板（勾选）

- [x] `GameStateMachine.cs`
- [x] `InputMap.cs`
- [x] `SceneBootstrap.cs`
- [x] `GameConfig.cs`（使用其中的 moveSpeed / spawnInterval / difficultyCurve）
- [x] `PlayerController2D.cs`（TopDown 模式；如果主题适合重力感可用 SideScroller）
- [x] `Spawner.cs`（场景里放两个实例：一个生成星星，一个生成威胁）
- [x] `ScoreManager.cs`
- [x] `AudioService.cs`（用到 Pickup、Hit、Fail、Tick、BgmMain）
- [x] `UIBinder.cs` + 四个 UI 预制
- [x] `DebugToggle.cs`

## 5. 决策密度来源

- "要不要去抓那颗贴着威胁漂移的金色星星？"
- "保守待在中央只拿低价值的星星，还是去冒险？"

## 6. 重玩动机来源

- 个人最高分追逐（ScoreManager 通过 PlayerPrefs 持久化）
- 擦身而过的紧张感 —— 差一点撞上威胁的瞬间就是刺激点

## 7. 3 必做 / 3 不做

**必做**
- 每次拾取和每次碰撞都有即时的视觉 + 音频反馈
- 玩家游玩时"当前分 / 最高分"都可见
- GameOver 停留短（≤ 2 秒），Restart 立刻可按

**不做**
- 多生命值 / 血条。一击死，读图干净。
- 强化道具、武器升级、连击倍率（周日第二次试玩通过且有余力才加）
- 多种威胁类型（只做一种，用数量爬坡，不用种类）

## 8. 主题映射示例

| 主题 | 套法 |
|---|---|
| 共生 | 拿星星会把小伙伴慢慢回复（仍是一击死，但多一次宽容） |
| 循环 | 下落过的星星延迟后从上方再生 |
| 碎片 | 威胁是"碎片"，星星是"重组件" |
| 光与影 | 月光处安全；阴影处有威胁 |
| 双生 | 两个角色镜像移动，你控制一个，另一个自动镜像 |
| 记忆 | 分数短暂以"幽灵星星"形式跟随角色 |
| 小即是大 | 小尺寸星星分更高，大的反而便宜 |

## 9. AI prompt 起点

> 周日直接粘贴给 AI 用，已经是英文。

```
You are helping build a Unity 2.5D Game Jam demo (2D sprites with z-layering for depth) in a URP 2D project.
Rules: Unity 2022.3.62f1, legacy Input (no Input System), TextMeshPro, namespace VibeJam, [SerializeField] private fields.

Given these existing kit scripts in Assets/Scripts/: GameStateMachine, InputMap, SceneBootstrap, GameConfig (ScriptableObject), PlayerController2D (ControlMode.TopDown), Spawner, ScoreManager, AudioService, UIBinder.

Implement Playbook A · Dodge & Collect:
- A "PickupStar" prefab with a trigger Collider2D that on OnTriggerEnter2D(Collider2D) calls ScoreManager.Instance.Add(config.BasePickupScore), plays AudioService.Play(SfxKey.Pickup), and Destroys itself.
- A "HazardMeteor" prefab with a trigger Collider2D that calls AudioService.Play(SfxKey.Hit), then AudioService.Play(SfxKey.Fail), then GameStateMachine.Instance.EndGame(), and Destroys itself.
- Two Spawner instances in the scene: one for stars at top, one for hazards at top, both spawning downward via their prefab's Rigidbody2D gravity.
- Player is tagged "Player"; prefabs check for that tag on collision.

Produce the two prefab-attachable scripts (Pickup.cs under VibeJam.Gameplay, Hazard.cs under VibeJam.Gameplay). One file per turn. Include AI EXTENSION HINTS blocks.
```
