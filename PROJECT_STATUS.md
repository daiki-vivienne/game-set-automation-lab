# game-set-automation-lab 現在の状況

最終更新 2026-11-12（`main` の文書整理）

## 現在地と次の一手

`main` には固定マップ上の移動・向き変更と、GitHub ActionsでのEditModeテストが統合済み。Milestone 2「最初のCI」を完了し、次はMilestone 3「Unityへ表示する動く骨組み」を進める。

Unityの表示試作は作業ブランチ `feature/unity-position-preview` で進行中。次は、固定マップの床・壁をUnityのXY平面に表示する。キー入力と移動の接続、GameEvent、接続を確かめるPlayModeテストは後続。

## 実装済みと設計上の前提

- **移動と状態**。Coreの `GameState.Move(direction, map)` は、入力方向を向き、歩ける場合に位置を更新してターンを1増やす。壁・地図外への入力や `ChangeFacing` では位置とターン数を維持する。地形判定は `FixedMap`、状態更新は `GameState` が担当する。既存のコンストラクターと初期状態テストを維持して移動を追加できるよう、呼び出し側で用意した同じ地図を移動時に渡す。[TECH_ARCHITECTURE §4.2](https://drive.google.com/file/d/10wfhZBM1yn0YHzlrLL0v2yc-4tp5_Ab0/view?usp=drivesdk)
- **座標と地形**。原点は左下で、右へX、上へYが増える。文字地図は上から保存するため、`FixedMap` は座標が範囲内かを先に確認し、`Height - 1 - y` で行を選ぶ。`IsWalkable` は床で `true`、壁と地図外で `false` を返す。
- **開発環境**。Unity 6.3 LTS（6000.3.24f1、Universal 3D／URP）、Rider、Unity Test Framework 1.6.0。CoreはUnityに依存せず、Unity側のPresentationから参照する。

## 確認結果

| 確認 | 対象と結果 |
|---|---|
| GitHub ActionsのEditMode（2026-11-12 JST） | 文書整理前の `main`（`0489ede`）で、`GameStateTests` 8件と `FixedMapTest` 6件が成功。計14件、失敗・スキップ0。成果物 `editmode-test-results` の保存を確認。[Actions実行](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/43069590869)・[テスト集計](https://github.com/daiki-vivienne/game-set-automation-lab/runs/130695940882) |
| ローカルのEditMode（Day25、2026-10-26分） | Unity Test RunnerでCoreの14件が成功。失敗・スキップ0。提示画像と保存済みXMLで確認した記録。[Day33版の確認記録](https://github.com/daiki-vivienne/game-set-automation-lab/blob/d9714518d0bbfedcd62eeedf69aabe4a3b382b96/PROJECT_STATUS.md) |

CIはPushと同一リポジトリから `main` へのPRでEditModeを実行済み。外部fork PRではUnityジョブをスキップする設定で、実イベントは未検証。[Workflow](.github/workflows/editmode-tests.yml)・[PR #3](https://github.com/daiki-vivienne/game-set-automation-lab/pull/3)

## 保存状態と参照先

- Coreの移動・向き変更とEditModeテストは[PR #2](https://github.com/daiki-vivienne/game-set-automation-lab/pull/2)、CI設定は[PR #3](https://github.com/daiki-vivienne/game-set-automation-lab/pull/3)で `main` へ統合済み。
- CIは意図的な誤りを1件検出し、復旧後に14件成功した。導入時の判断と失敗調査の記録。[Day33制作ログ](https://docs.google.com/document/d/1if7VTwKOo9Y5NqrglwYs_MqtObemm1pD_VEJb6zqOyk/edit)・[誤り検出のCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/42377597976)・[復旧後のCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/42377660368)
- 開発順序、仕様・設計・テスト方針と過去の制作記録。[ROADMAP](https://drive.google.com/file/d/1jaU-5QqbKx3NrHswDS7wCpg1-ttMNNW7/view?usp=drivesdk)・[仕様・設計資料フォルダ](https://drive.google.com/drive/folders/1JPWF1LwqBv_yED0qeqgQlT1r14COcE1Q)・[TEST_STRATEGY §16・§23](https://drive.google.com/file/d/1FCNIBfNum6iua3B7nrhqTkZYCA-alcMy/view)・[Vivienne QA Labの記録フォルダ](https://drive.google.com/drive/folders/18TJtEXtzkT8-hLh6TFMAtqBlVua3gk5F)
