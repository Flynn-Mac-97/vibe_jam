# Playbook C · 风险-收益走位

天花板最高，可靠性最低。团队状态好且主题要求策略性移动时才选。

> 与英文版的同步说明：本 .zh.md 在 .en.md 更新时同步翻译。字段顺序和编号严格一致。

## 1. 一句话定义

场景里有"安全区"和"得分区"；停在得分区的时间越长分数涨得越快，但得分区始终有威胁 —— 玩家必须判断什么时候冲进去、什么时候撤出来。

## 2. 三层核心循环

- **10 秒**：测试者发现得分区在加分、安全区安静。
- **60 秒**：开始短暂冲入得分区，每次冲入都有"要不要贪一下"的决策。
- **3 分钟**：追打最长连续停留时间 —— 贪与保的张力就是整个游戏。

## 3. 成功与失败条件

- **成功**：分数驱动 + 最长单次"在得分区停留"作为次要战利品
- **失败**：在得分区被击中一次 → Game Over。安全区永远安全。

## 4. 使用的工具包模板（勾选）

- [x] `GameStateMachine.cs`
- [x] `InputMap.cs`
- [x] `SceneBootstrap.cs`
- [x] `GameConfig.cs`（加 `baseZoneScorePerSecond`、`zoneStreakMultiplierCurve`）
- [x] `PlayerController2D.cs`（TopDown 或 SideScroller 都行）
- [x] `Spawner.cs`（只生成威胁，威胁穿过 / 进入得分区）
- [x] `ScoreManager.cs`（扩展支持连续按秒累积分数）
- [x] `AudioService.cs`（在区里用 Tick，Hit、Fail、Win、BgmMain）
- [x] `UIBinder.cs`（加一个"IN ZONE"指示器）
- [x] `DebugToggle.cs`

## 5. 决策密度来源

- "再多待一秒吗？"
- "威胁近了 —— 再撑一下拉高倍率？"
- "连击刚过阈值 —— 落袋为安还是继续？"

## 6. 重玩动机来源

- "差一点点"的张力 —— 玩家往往是多贪一秒就凉了
- 连击倍率让分数"值得" —— 曲线奖励胆量

## 7. 3 必做 / 3 不做

**必做**
- 得分区视觉清晰可辨 —— 明显的轮廓 / 颜色 / 亮度
- 在区里赚分时有 tick 或 pulse 音效（用声音强化风险感）
- 停留越久加速度越高 —— 玩家能感觉到"升级"

**不做**
- 多个规则不同的得分区（scope 爆炸）
- 会影响区规则的 powerup
- "强制撤出"计时器 —— 决策权留给玩家才是重点

## 8. 主题映射示例

| 主题 | 套法 |
|---|---|
| 共生 | 区里的分数从同伴资源条扣 —— 你自私时别人付代价 |
| 循环 | 每次 Game Over 后区变小一点 |
| 碎片 | 在区里收集的碎片填进度条，到阈值升倍率 |
| 光与影 | 得分区黑暗、安全区有光；威胁只在黑暗出现 |
| 双生 | 两个交替开放的区，只有"开"的那个得分，位置来回切 |
| 贪婪 | 字面化 —— 分越多，威胁越多 |

## 9. AI prompt 起点

```
You are helping build a Unity 2.5D Game Jam demo (2D sprites with z-layering for depth) in a URP 2D project.
Rules: Unity 2022.3.62f1, legacy Input (no Input System), TextMeshPro, namespace VibeJam, [SerializeField] private fields.

Given these existing kit scripts: GameStateMachine, InputMap, GameConfig, PlayerController2D, Spawner, ScoreManager, AudioService, UIBinder.

Implement Playbook C · Risk-Reward Positioning:
- A "ScoringZone" MonoBehaviour with a Collider2D (isTrigger = true) that detects the player staying inside via OnTriggerStay2D. While the player is inside AND GameState == Playing, the zone accrues fractional score at a rate baseZoneScorePerSecond * zoneStreakMultiplierCurve.Evaluate(currentStreakSeconds), and calls ScoreManager.Instance.Add at whole-point boundaries.
- Plays AudioService.Play(SfxKey.Tick) periodically (every 0.5s) while the player is inside.
- Resets streak when the player exits the zone.
- Spawner inside the zone spawns hazards that the player must dodge within the zone — hitting one triggers GameStateMachine.Instance.EndGame().

Produce ScoringZone.cs first (include AI EXTENSION HINTS).
```
