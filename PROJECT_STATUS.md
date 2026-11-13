# game-set-automation-lab 現在の状況

最終更新 2026-11-13（Day36の床表示と文字地図の上下を確認）

## 現在地と次の一手

Milestone 3「Unityへ表示する動く骨組み」を進めている。`SampleScene` の固定マップで壁と青い床を表示し、文字地図と画面の上下が対応することを手動Playで確認した。確認用に変更した地図は元の5×3マスに戻した。

次は、キーを1つ押したときに既存の `GameState.Move` を呼び、更新されたX・Yを画面へ反映する。開始位置 `(2, 1)` から右隣の床 `(3, 1)` への1マス移動を試す。

## 実装済みと設計上の前提

- **Core**（`Assets/_Project/Core`）の `GameState.Move(direction, map)` は、入力方向を向き、歩ける場合にだけ位置を更新してターンを1増やす。壁・地図外への入力や `ChangeFacing` では位置とターン数を維持する。地形判定は `FixedMap`、状態の更新は `GameState` が担当する。既存のコンストラクターと初期状態テストを維持して移動を追加できるよう、呼び出し側で用意した同じ地図を移動時に渡す設計を採用した。[TECH_ARCHITECTURE §4.2](https://drive.google.com/file/d/10wfhZBM1yn0YHzlrLL0v2yc-4tp5_Ab0/view?usp=drivesdk)
- **座標と地形**は左下が原点で、右へX、上へYが増える。文字地図は上から保存するため、`FixedMap` は範囲内か確認してから `Height - 1 - y` で行を選ぶ。`IsWalkable` は床で `true`、壁と地図外で `false` を返す。複数マスの表示でこの判定を使う場合は、対象座標が地図内であることを確認する。
- **Unity表示**（`Assets/_Project/Presentation`）はCoreを参照し、CoreはUnityに依存しない。`PlayerPositionPreview` は試作用 `GameState` のX・Yを仮プレイヤーの位置に使う。`WallPositionPreview` は `Start` で試作用 `FixedMap` を作り、全マスに対する `IsWalkable` の結果で壁と床を分け、地図と同じX・Y、Z=0に立方体を置く。床は `GetComponent<MeshRenderer>()` で描画部品を取得し、その `material.color` を `Color.blue` にする。旧 `WallPreview` のMesh Rendererはオフ。
- **開発環境**はUnity 6.3 LTS（6000.3.24f1、Universal 3D／URP）、Rider、Unity Test Framework 1.6.0。CoreのEditModeテストは `GameStateTests` 8件（初期状態・移動・向き）、`FixedMapTest` 6件（寸法・範囲・歩行可否）。

## 最新の確認結果

| 確認 | 対象と結果 |
|---|---|
| Unityの手動Play（Day36、2026-11-13） | 本人がGameビューで中央3マスの青い床を確認（再生中に仮プレイヤーのMesh Rendererを一時オフ）。文字地図の上段だけを `#.###` に変えると、予想どおり画面の上段左から2番目が青くなり、下段は壁のまま。上段を `#####` に戻して再生し、青い床が中央3マスだけに戻ることを確認した。 |
| Unityの手動Play（Day35、2026-11-12） | 本人がConsoleで全3段それぞれのXが0〜4と出ることを確認。中央段は `False, True, True, True, False` で `#...#` と一致。Gameビューの画像で、外周の壁と中央のプレイヤー、床表示をまだ入れていない左右の空きマスを確認した。上下の向きはDay36に確認した。 |
| Unityの手動Play（Day34、2026-11-11分） | 本人が `SampleScene` を再生し、GameビューとInspectorで `PlayerPreview` が `(2, 1, 0)`、`WallPreview` が `(4, 1, 0)` に表示されることを確認。シーン内の両スクリプト参照と対応する `.meta` のGUIDも一致。[Day34制作ログ](https://docs.google.com/document/d/19pUtebgwESrCnsTunv1FVOKEEPr782qDvYTyKHCb9uE/edit) |
| GitHub ActionsのEditMode（2026-11-12 JST） | `main` の用語報告ルール更新 `7e83e79` に対するPush CI。実行ログ内XMLで `GameStateTests` 8件、`FixedMapTest` 6件の計14件が成功。失敗・スキップ0。成果物 `editmode-test-results` の保存を確認。[Actions実行](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/43208328027) |
| ローカルのEditMode（Day25、2026-10-26分） | 本人がUnity Test RunnerでCoreの14件を実行し、成功14・失敗0・スキップ0。提示画像と保存済みXMLで確認した記録。[当時の結果を含むDay33版](https://github.com/daiki-vivienne/game-set-automation-lab/blob/d9714518d0bbfedcd62eeedf69aabe4a3b382b96/PROJECT_STATUS.md) |

作業ブランチのDay35コミット `c0b1ed8` に対するEditMode CIは、実行ログ内XMLで成功14件・失敗0件・スキップ0件を確認した。Day36の床表示は手動Playで確認し、今回の差分に対する自動テストは未実行。[Day35のCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/43232480864)

## Gitと保存状態

- 作業ブランチ `feature/unity-position-preview` はDay35の壁表示を `c0b1ed8` でCommit・Push済み。Day36の床表示と確認結果の保存対象は `WallPositionPreview.cs` と本書。
- ローカル `main`・`origin/main`・GitHubの `main` は `7e83e79` で一致。
- Unityが生成した `ProjectSettings/SceneTemplateSettings.json` は未追跡。

## 未確認・後続作業

- キー入力と既存の移動処理の接続、移動結果の表示反映は後続。プレイヤーの箱と中央の床の箱は同じ位置にあるため、床を数える確認ではプレイヤーの表示を一時オフにした。最小のGameEventと、入力・移動・表示の接続を確かめるPlayModeテストは後続。
- CIはPushと同一リポジトリから `main` へのPRでEditModeを実行済み。外部fork PRではUnityジョブをスキップする設定で、実イベントは未検証。[Workflow](.github/workflows/editmode-tests.yml)・[PR #3](https://github.com/daiki-vivienne/game-set-automation-lab/pull/3)
- Day34の保存済み差分には、Unityのシーン出力とプレビュースクリプトの行末空白が残っている。

## 設計資料と過去の記録

- 開発順序と決定済みの仕様・設計・テスト方針。[ROADMAP](https://drive.google.com/file/d/1jaU-5QqbKx3NrHswDS7wCpg1-ttMNNW7/view?usp=drivesdk)・[仕様・設計資料フォルダ](https://drive.google.com/drive/folders/1JPWF1LwqBv_yED0qeqgQlT1r14COcE1Q)・[TEST_STRATEGY §16・§23](https://drive.google.com/file/d/1FCNIBfNum6iua3B7nrhqTkZYCA-alcMy/view)
- Milestone 2「最初のCI」は完了。向き変更に意図的な誤りを入れた実行で1件の失敗を検出し、復旧後に14件成功した。導入の経緯と確認内容は制作ログから追える。[Day33制作ログ](https://docs.google.com/document/d/1if7VTwKOo9Y5NqrglwYs_MqtObemm1pD_VEJb6zqOyk/edit)・[誤り検出のCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/42377597976)・[復旧後のCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/42377660368)
- 過去の制作ログ、復習資料、作業時間表。[Vivienne QA Labの記録フォルダ](https://drive.google.com/drive/folders/18TJtEXtzkT8-hLh6TFMAtqBlVua3gk5F)・[作業時間表](https://docs.google.com/spreadsheets/d/1He70CnsMwKe-eGSXBvuIYKwaueKuBBzZpusN-AjDOOc/edit)
