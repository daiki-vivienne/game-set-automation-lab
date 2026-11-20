# game-set-automation-lab 現在の状況

最終更新 2026-11-20（Day40の上下移動確認と移動結果データの準備）

## 現在地と次の一手

Milestone 3「Unityへ表示する動く骨組み」を進めている。`SampleScene` でA・Dによる左右移動に続き、確認用の地図でW・Sによる上下移動と地図外での停止を手動Playで確認した。確認後、文字地図は元の形に戻した。移動元と移動先を保持する `ActorMoved` を追加し、`Move` では座標を更新する前に移動元を控えるところまで書いた。

次は `Move` の移動前後の座標と `ActorMoved` の読み取り口を見直す。そのうえで移動に成功したときの結果を呼び出し元に渡す経路をつなぎ、`GameStateTests` で1回の移動結果を確認する。

## 実装済みと設計上の前提

- **Core**（`Assets/_Project/Core`）の `GameState.Move(direction, map)` は、入力方向を向き、歩ける場合にだけ位置を更新してターンを1増やす。壁・地図外への入力や `ChangeFacing` では位置とターン数を維持する。地形判定は `FixedMap`、状態の更新は `GameState` が担当する。既存のコンストラクターと初期状態テストを維持して移動を追加できるよう、呼び出し側で用意した同じ地図を移動時に渡す設計を採用した。[TECH_ARCHITECTURE §4.2](https://drive.google.com/file/d/10wfhZBM1yn0YHzlrLL0v2yc-4tp5_Ab0/view?usp=drivesdk)
- **移動結果の準備**。`ActorMoved` は移動元と移動先のX・Yを受け取り、外から読み取れるデータとして保持する。現在の `Move` は `void` で、成功時に移動元の座標を控えるまで実装している。[TECH_ARCHITECTURE §5–6](https://drive.google.com/file/d/10wfhZBM1yn0YHzlrLL0v2yc-4tp5_Ab0/view?usp=drivesdk)
- **座標と地形**は左下が原点で、右へX、上へYが増える。文字地図は上から保存するため、`FixedMap` は範囲内か確認してから `Height - 1 - y` で行を選ぶ。`IsWalkable` は床で `true`、壁と地図外で `false` を返す。複数マスの表示でこの判定を使う場合は、対象座標が地図内であることを確認する。
- **Unity表示と入力**（`Assets/_Project/Presentation`）はCoreを参照し、CoreはUnityに依存しない。`WallPositionPreview` は `Start()` で `FixedMap` を1つ作り、各マスの `IsWalkable` の結果に応じて壁と青い床を地図と同じX・Y、Z=0に表示する。`PlayerPositionPreview` は `Start()` で `GameState` を初期化する。W・A・S・Dを押した瞬間に、対応する方向と壁側の `LevelMap` を `GameState.Move` に渡し、更新後のX・Yを `transform.position` に反映する。表示のZは開始時と移動後に `-0.5f` を使い、箱のScaleはシーンで `(0.5, 0.5, 0.5)` にした。入力には `Unity.InputSystem` を使う。
- **開発環境**はUnity 6.3 LTS（6000.3.24f1、Universal 3D／URP）、Rider、Unity Test Framework 1.6.0。CoreのEditModeテストは `GameStateTests` 8件（初期状態・移動・向き）、`FixedMapTest` 6件（寸法・範囲・歩行可否）。

## 最新の確認結果

| 確認 | 対象と結果 |
|---|---|
| Unityの手動Play（Day40、2026-11-20） | 上下の中央を床にした確認用の文字地図で、本人が同じ開始位置 `(2, 1, -0.5)` から、Wで `(2, 2, -0.5)`、Sで `(2, 0, -0.5)` へ1マス進むことと、地図外に出ないことを確認した。確認後は元の文字地図に戻し、シーンと文字地図の差分が残っていないことをGitで確認した。 |
| Coreのコンパイル（Day40、2026-11-20） | Unity付属のC#コンパイラーと.NET Standard 2.1の参照アセンブリを使い、`ActorMoved.cs` を含むCoreの4ファイルをライブラリとしてコンパイルした。終了コード0。 |
| Unityの手動Play（Day39、2026-11-19〜20） | 本人が、表示調整後も青い床と箱を見分けられることをGameビューで確認した。Dで右の床 `(3, 1, -0.5)` に移ったことはInspectorの画像で確認した。本人は次のDで右壁に対して停止し、Aでは左の床へ進んでから次のAで左壁に対して停止することも確認した。新しく再生してW・Sをそれぞれ押すと、Consoleに入力ログが出て、開始座標 `(2, 1, -0.5)` に留まった。 |
| Unityの手動Play（Day38、2026-11-19） | 本人が `SampleScene` を再生し、Dキーで `PlayerPreview` を右へ1マス移動。提示画像のInspectorで、開始位置 `(2, 1, 0)` に対して更新後の `Position` が `(3, 1, 0)` であることを確認した。Gameビューでは箱と床が重なるため、移動は座標で判定した。 |
| Unityの手動Play（Day37、2026-11-16） | 本人が `SampleScene` を再生し、DキーでConsoleに「Dキーを押した」が出ることを確認。画像にも表示あり。再生直後に出た `False` は、地図の生成時に `IsWalkable` の結果を表示する既存ログ。 |
| Unityの手動Play（Day36、2026-11-13） | 本人がGameビューで中央3マスの青い床を確認（再生中に仮プレイヤーのMesh Rendererを一時オフ）。文字地図の上段だけを `#.###` に変えると、予想どおり画面の上段左から2番目が青くなり、下段は壁のまま。上段を `#####` に戻して再生し、青い床が中央3マスだけに戻ることを確認した。 |
| Unityの手動Play（Day35、2026-11-12） | 本人がConsoleで全3段それぞれのXが0〜4と出ることを確認。中央段は `False, True, True, True, False` で `#...#` と一致。Gameビューの画像で、外周の壁と中央のプレイヤー、床表示をまだ入れていない左右の空きマスを確認した。上下の向きはDay36に確認した。 |
| Unityの手動Play（Day34、2026-11-11分） | 本人が `SampleScene` を再生し、GameビューとInspectorで `PlayerPreview` が `(2, 1, 0)`、`WallPreview` が `(4, 1, 0)` に表示されることを確認。シーン内の両スクリプト参照と対応する `.meta` のGUIDも一致。[Day34制作ログ](https://docs.google.com/document/d/19pUtebgwESrCnsTunv1FVOKEEPr782qDvYTyKHCb9uE/edit) |
| GitHub ActionsのEditMode（Day39のmain、2026-11-19） | 学習支援ルールを追記した `main` の `2e9b009` で、`Vivienne.EditModeTests.dll` が成功14件・失敗0件・スキップ0件。[mainのCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/44526897728) |
| GitHub ActionsのEditMode（Day39の作業ブランチ、2026-11-20） | 表示調整とWASD入力をつないだ `6042808` で、`Vivienne.EditModeTests.dll` が成功14件・失敗0件・スキップ0件。[Day39のCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/44553476769) |
| GitHub ActionsのEditMode（Day38の作業ブランチ、2026-11-19） | 右移動を追加した `10df40e` で、`Vivienne.EditModeTests.dll` が成功14件・失敗0件・スキップ0件。[Day38のCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/44353898109) |
| ローカルのEditMode（Day25、2026-10-26分） | 本人がUnity Test RunnerでCoreの14件を実行し、成功14・失敗0・スキップ0。提示画像と保存済みXMLで確認した記録。[当時の結果を含むDay33版](https://github.com/daiki-vivienne/game-set-automation-lab/blob/d9714518d0bbfedcd62eeedf69aabe4a3b382b96/PROJECT_STATUS.md) |

Day40の移動結果データを含むコミットのEditMode結果は、作業ブランチのCIで対象コミットを確認する。[作業ブランチのCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/workflows/editmode-tests.yml?query=branch%3Afeature%2Funity-position-preview)

## Gitと保存状態

- 作業ブランチは `feature/unity-position-preview`。Day39の表示調整とWASD入力は `6042808`。Day40の移動結果データの準備は `ActorMoved.cs` と対応する `.meta`、`GameState.cs`、本書が保存対象。
- Day39に `AGENTS.md` の学習支援ルール1項目を `main` の `2e9b009` としてPushした。
- Unityが生成した `ProjectSettings/SceneTemplateSettings.json` は未追跡。

## 未確認・後続作業

- `Move` から `ActorMoved` を渡す経路、移動結果を確認するEditModeテスト、入力・移動・表示の接続を確認するPlayModeテストは後続。
- CIはPushと同一リポジトリから `main` へのPRでEditModeを実行済み。外部fork PRではUnityジョブをスキップする設定で、実イベントは未検証。[Workflow](.github/workflows/editmode-tests.yml)・[PR #3](https://github.com/daiki-vivienne/game-set-automation-lab/pull/3)
- Day34の保存済み差分には、Unityのシーン出力とプレビュースクリプトの行末空白が残っている。

## 設計資料と過去の記録

- 開発順序と決定済みの仕様・設計・テスト方針。[ROADMAP](https://drive.google.com/file/d/1jaU-5QqbKx3NrHswDS7wCpg1-ttMNNW7/view?usp=drivesdk)・[仕様・設計資料フォルダ](https://drive.google.com/drive/folders/1JPWF1LwqBv_yED0qeqgQlT1r14COcE1Q)・[TEST_STRATEGY §16・§23](https://drive.google.com/file/d/1FCNIBfNum6iua3B7nrhqTkZYCA-alcMy/view)
- Milestone 2「最初のCI」は完了。向き変更に意図的な誤りを入れた実行で1件の失敗を検出し、復旧後に14件成功した。導入の経緯と確認内容は制作ログから追える。[Day33制作ログ](https://docs.google.com/document/d/1if7VTwKOo9Y5NqrglwYs_MqtObemm1pD_VEJb6zqOyk/edit)・[誤り検出のCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/42377597976)・[復旧後のCI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/42377660368)
- 過去の制作ログ、復習資料、作業時間表。[Vivienne QA Labの記録フォルダ](https://drive.google.com/drive/folders/18TJtEXtzkT8-hLh6TFMAtqBlVua3gk5F)・[作業時間表](https://docs.google.com/spreadsheets/d/1He70CnsMwKe-eGSXBvuIYKwaueKuBBzZpusN-AjDOOc/edit)
