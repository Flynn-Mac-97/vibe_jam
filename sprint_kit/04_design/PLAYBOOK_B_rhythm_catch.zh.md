# Playbook B · 节奏接取

可靠性：中等。trixie 有强 BGM 循环 + 主题偏节奏 / 时间，选这个。

> 与英文版的同步说明：本 .zh.md 在 .en.md 更新时同步翻译。字段顺序和编号严格一致。

## 1. 一句话定义

BGM 的拍点触发物体下落；玩家在正确瞬间按键或移动接住物体得分，失败会让节奏加速。

## 2. 三层核心循环

- **10 秒**：测试者听到 BGM；物体开始合拍下落；测试者摸到接的时机。
- **60 秒**：基本能接中大多数拍点；偶尔出现偏拍的"陷阱"物体，教玩家对比。
- **3 分钟**：节奏逐渐加倍；玩家的最长连击就是战利品。

## 3. 成功与失败条件

- **成功**：分数驱动 + 最长连击
- **失败**：连续漏掉 3 个，或接到一个"陷阱" → Game Over

## 4. 使用的工具包模板（勾选）

- [x] `GameStateMachine.cs`
- [x] `InputMap.cs`
- [x] `SceneBootstrap.cs`
- [x] `GameConfig.cs`（在 GAMEPLAY 区添加 `beatBpm` / `toleranceMs`）
- [x] `PlayerController2D.cs`（TopDown，限制只能横向移动）
- [x] `Spawner.cs`（节奏驱动版，见 prompt）
- [x] `ScoreManager.cs`（在 GAMEPLAY 区加 combo 字段）
- [x] `AudioService.cs`（Tick、Pickup、Hit、Fail、BgmMain）
- [x] `UIBinder.cs`（加 comboText）
- [x] `DebugToggle.cs`

## 5. 决策密度来源

- "这个连击再走两步去拿，还是保底接住当前的？"
- "这个物体偏拍了 —— 是不是陷阱？"

## 6. 重玩动机来源

- 连击作为可炫耀的数值 —— 长连击感觉是赚来的，掉了就想再挑战
- 音乐本身就是奖励 —— 每一局都像在演奏

## 7. 3 必做 / 3 不做

**必做**
- 合拍物体和陷阱物体视觉明显区分
- 连击计数器很大、可见
- 接住的反馈干脆 —— 音效 + 视觉同一帧

**不做**
- 完整的节奏游戏 lane UI（吉他英雄风格），scope 爆炸
- 手工编排每一拍的 chart —— 用程序化按拍生成即可
- 难度分支（简单 / 困难），只做一条曲线

## 8. 主题映射示例

| 主题 | 套法 |
|---|---|
| 共生 | 玩家替同伴接物体；漏了两人都扣 |
| 循环 | BGM 每循环一遍更换生成模式 |
| 碎片 | 接到的碎片填满视觉进度条 |
| 光与影 | 接中亮场景；漏掉暗场景 |
| 双生 | 两条 lane，左边是正拍，右边是偏拍，分辨并选对的一方 |
| 记忆 | 先放一段模式，玩家要复现接取 |
| 时间 | 快节奏下完美接中短暂减速 |

## 9. AI prompt 起点

```
You are helping build a Unity 2.5D Game Jam demo (2D sprites with z-layering for depth) in a URP 2D project.
Rules: Unity 2022.3.62f1, legacy Input (no Input System), TextMeshPro, namespace VibeJam, [SerializeField] private fields.

Given these existing kit scripts: GameStateMachine, InputMap, GameConfig, PlayerController2D (TopDown), Spawner, ScoreManager (with combo), AudioService, UIBinder.

Implement Playbook B · Rhythm Catch:
- A "BeatConductor" MonoBehaviour under VibeJam.Gameplay that, while State == Playing, fires a C# event OnBeat every (60f / config.BeatBpm) seconds. Also plays AudioService.Play(SfxKey.Tick) on each beat.
- A "BeatSpawner" that subscribes to BeatConductor.OnBeat and spawns either a catch object (90%) or trap (10%) from Spawner's prefab list.
- A "Catcher" MonoBehaviour on the player: trigger collision with catch object adds score and increments combo; trigger with trap or missing (object reaches bottom) triggers Hit/Fail per rules.

Produce BeatConductor.cs first (one file per turn, include AI EXTENSION HINTS).
```
