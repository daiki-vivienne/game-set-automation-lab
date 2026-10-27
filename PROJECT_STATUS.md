# game-set-automation-lab 現在の状況

最終更新：2026-10-27（Day26終了・2026-10-27分）

## 現在地と次回の開始点

Day26の到達点は、本人が合意・承認した学習目標と支援方針の整理、`AGENTS.md`への統合、現在地文書の更新と保存。Workは`TEST_STRATEGY_v0.1.md`の16・23節を更新し、Codexは最新版を読み取り確認してリポジトリの運用文書へ反映した。

本人が文法・処理の流れ・役割を理解しながらコードを書く進め方を維持し、検証の優先順位、確認方法の設計・実装、証拠からの失敗原因の切り分けを少しずつ補う。節目の代表的な課題を理解度に合わせて扱い、毎回の試験や前提知識なしの設計課題にはしない。具体的な支援手順は`AGENTS.md`を参照する。

本人による新しいゲーム・テスト・Workflowの実装はない。今回の方針決定と文書化を、設計・実装・調査の力を実際に練習・習得できた証拠とは扱わない。Day26の作業ではUnity・CIを実行していない。

進行中の対象はROADMAPのMilestone 2「最初のCI」。次回は「本人が自動化したい作業を日本語で整理する」から再開し、必要な説明・段階的なヒントを受けながら本人がWorkflowを作る。現存するWorkflowはCodex作成の未追跡案であり、本人の作成・理解確認済みの成果とは扱わない。表示・入力接続はMilestone 3で扱う。

## プロジェクトと環境

Unity・C#・自動テスト・CI/CDを学び、ゲームSET/SDETを目指す学習兼ポートフォリオ。クリスタル洞窟を舞台にしたターン制ローグライクを制作予定。

* Unity 6.3 LTS（6000.3.24f1）、Universal 3D（URP）
* C#、JetBrains Rider、Unity Test Framework 1.6.0
* ローカル：`C:\Users\draqu\Desktop\vivienne-qa-lab\game-set-automation-lab`
* GitHub：`daiki-vivienne/game-set-automation-lab`
* 構成：`Assets/_Project`配下にCore、Presentation、Automation、Tests/EditMode、Tests/PlayMode
* asmdef：Vivienne.GameCore、Vivienne.Unity、Vivienne.Automation、Vivienne.EditModeTests、Vivienne.PlayModeTests
* CoreはUnityEngineへの依存なし。EditModeはEditor専用でCore・Unity Test Runner・NUnitを参照し、UNITY_INCLUDE_TESTSを設定
* UnityのForce Text・Visible Meta Files・Rider接続とテスト基盤は準備済み。構成変更のPR #1はmainへ取り込み済み

## 実装済みの機能

| ファイル | 現在できること |
|---|---|
| Assets/_Project/Core/Direction.cs | Up・Down・Left・Rightを表す公開enum |
| Assets/_Project/Core/GameState.cs | 初期状態と読み取り専用プロパティ。Moveは方向と地図を受け取り、歩ける場合だけ移動・ターン更新。ChangeFacingは方向だけ受け取り、位置・ターン数を変えず向きを更新 |
| Assets/_Project/Core/FixedMap.cs | 文字列配列で地図を保持。Height・Width、IsInside(x, y)の範囲確認、IsWalkable(x, y)の床判定 |
| Assets/_Project/Tests/EditMode/GameStateTests.cs | 初期状態、4方向の床移動、壁・マップ外への入力、ChangeFacingKeepsPositionAndTurnCountの計8件 |
| Assets/_Project/Tests/EditMode/FixedMapTest.cs | HeightAndWidthAreCorrect、EdgePositionsAreInsideMap、OutsidePositionsAreNotInsideMap、IsWalkableIsTrue、FloorCellsWalkable、IsWalkableIsFalseの6件 |

固定マップと左右移動のテスト用地図は3行・横幅5。上下移動と壁・マップ外への入力には4行・横幅5の地図を使用し、各行は同じ長さ。本番マップの大きさ・配置を決めたものではない。上下移動の地図には縦に隣り合う床がある。マップ外のテストでは(4, 1)を床にした専用配置から(5, 1)への入力を試す。本番地図の外周を決めるものではなく、Coreの地図外への移動禁止ルールを確認するための配置である。高さと幅を異なる値にし、幅の処理が誤って行数を返しても区別できるようにしている。FloorCellsWalkableでは上段中央を壁、下段中央を床とし、上下の行を取り違えてもテストで区別できる配置にしている。

## 最新の確認結果

* 最新のUnity確認はDay25（2026-10-26分、確認日2026-10-27）のローカル実行。本人がUnity Test RunnerのEditModeで実行し、成功14件・失敗0件・スキップ0件を提示画像と保存済みTestResults.xmlでCodexが確認した。XMLはtotal=14、result=Passed、passed=14、failed=0、skipped=0、inconclusive=0。Console画像には通常ログ3件が表示され、赤いエラーは見当たらなかった
* 対象はGameStateTestsの8件とFixedMapTestの6件。IsWalkableIsFalseの4方向のAssertは1テストとして数える。Day25の確認以降、ゲーム・テストコードは変更していない。Codexによるコマンドライン実行はしていない
* Day26は運用文書の更新と保存のみ。Unity・CIを実行していない。CIでの認証成功、意図したテストの実行、意図的な失敗の検出・復旧後の成功は未確認
* Day25にCodex作成のWorkflow案はactionlint 1.7.12で構文検証を通過した。これは本人のWorkflow作成実績や、Unity・CIでの実行成功を示さない
* 過去の統合確認：PR #2のCodex reviewは対象c0b5b01で完了・指摘なし。2026-10-23のマージ後、c0b5b01と220f9b6の保存済みツリー一致をGitで確認済み

| テスト | 確認した内容 |
|---|---|
| InitialStateIsSet | 作成直後のX=1、Y=1、Facing=Up、TurnCount=0 |
| MoveRightOnFloorUpdatesState | 隣り合う床の地図で(1, 1)・Up・0ターンから右へ移動し、X=2・Y=1・Facing=Right・TurnCount=1 |
| MoveLeftOnFloorUpdatesState | 隣り合う床の地図で(2, 1)・Up・0ターンから左へ移動し、X=1・Y=1・Facing=Left・TurnCount=1 |
| MoveUpOnFloorUpdatesState | 縦に隣り合う床の地図で(2, 1)・Right・0ターンから上へ移動し、X=2・Y=2・Facing=Up・TurnCount=1 |
| MoveDownOnFloorUpdatesState | 同じ配置の地図で(2, 2)・Up・0ターンから下へ移動し、X=2・Y=1・Facing=Down・TurnCount=1 |
| MoveIntoWallOnlyChangesFacing | (3, 1)の床・Up・0ターンから右隣(4, 1)の壁へ入力し、X=3・Y=1・Facing=Right・TurnCount=0 |
| MoveOutsideMapOnlyChangesFacing | 端の床(4, 1)・Up・0ターンから右外(5, 1)へ入力し、X=4・Y=1・Facing=Right・TurnCount=0 |
| ChangeFacingKeepsPositionAndTurnCount | (2, 1)・Up・0ターンでRightを向く命令を渡し、X=2・Y=1・Facing=Right・TurnCount=0 |
| HeightAndWidthAreCorrect | 渡した地図の高さ3・横幅5 |
| EdgePositionsAreInsideMap | (0, 0)、左端(0, 1)、右端(4, 1)、上端(2, 2)、下端(2, 0)でtrue |
| OutsidePositionsAreNotInsideMap | 右外(5, 1)、左外(-1, 1)、上外(2, 3)、下外(2, -1)でfalse |
| IsWalkableIsTrue | 床(1, 1)でIsWalkableがtrueを返す |
| FloorCellsWalkable | 上段中央の壁(2, 2)でfalse、下段中央の床(2, 0)でtrue |
| IsWalkableIsFalse | 右外(5, 1)、左外(-1, 1)、上外(2, 3)、下外(2, -1)でIsWalkableが例外を出さずfalseを返す |

IsInsideは範囲確認で、歩行可能かの判定ではない。IsWalkableは範囲確認後にHeight - 1 - yで行を選び、Xの位置の文字が床かを比較する。地図外では文字を読む前にfalseを返す。4方向の地図外確認まで完了。ゲーム・テストコードは本人が実装し、Codexは説明・レビュー・結果確認・現在地の記録を行った。

## 決定済みの仕様と前提

* 固定マップで上下左右へ1マス移動。右でX増加、左でX減少、上でY増加、下でY減少。原点は左下のマス(0, 0)
* 文字地図は上から行順に保存する。この保存順とYの増加方向が逆なので、範囲内のYに対する行番号はHeight - 1 - yで求める。IsWalkable内で変換を実装済み
* 移動入力を受けると、移動先にかかわらず入力方向を向く。歩行可能な空きマスなら1マス進み、ターン数を1増やす
* 壁・マップ外・敵のいるマスでは向きだけ変わり、位置とターン数は変わらない。敵への移動入力は自動攻撃にせず、攻撃ボタンでのみ攻撃する
* 向きだけ変更する命令は、位置とターン数を変えない
* マップ外かを確認してから地形を参照する
* Day21のWork相談でB案を採用。移動メソッドはGameStateに置き、移動のたびに方向とFixedMapを受け取る。同じ地図では用意済みの同じFixedMapインスタンスを渡す。地形判定はFixedMap.IsWalkable、位置・向き・ターン数の更新はGameStateが担当。現在のGameStateコンストラクターは変更しない
* B案の理由は、既存コンストラクター・初期状態テストを維持して移動を小さく追加できること。Bは実際のゲームにも使える設計で、Aへの変更を前提としない。判断と理由はTECH_ARCHITECTURE_v0.1.mdの4.2節にWorkが記録し、2026-10-20にCodexが読み取り確認済み
* Game Coreは通常のC#とし、UnityEngineに依存させない。キー入力・画面表示は後で接続し、今回はテストから命令を渡す
* 敵・戦闘・斜め移動・ランダム生成・Botは初期移動試作の範囲外。CIはDay25からMilestone 2として扱う
* 座標・原点・文字地図の保存方針は本人が了承し、ChatGPT Workへ共有済み
* 最初のEditModeテスト、Milestone 2のCI、Milestone 3のUnity表示・入力接続の順に進める。早期から変更を継続検証し、ローカルとCIの実行差・失敗調査を学ぶための順序で、TECH_ARCHITECTUREの14・15節とROADMAPに整合させた。ゲーム仕様・CoreとUnityの分離・B案は変更しない

## CIの事前準備とWorkflow案

* `.github/workflows/editmode-tests.yml`はDay25にCodexが作成した未追跡案。Day26も本文と未追跡状態を保持し、編集・コミット・Push・CI実行はしない
* 案の実行対象はブランチへのPushとmain向けPRでのEditModeのみ。GitHub-hosted Ubuntu 24.04とGameCIを使い、外部forkのPRはUnity用Secretsが渡らないためジョブをスキップする
* Unity版はProjectVersion.txtから読み、`unityci/editor:ubuntu-6000.3.24f1-base-3.2.2`の公開をDay25に確認済み。ActionはコミットSHA、GameCI CLIはv0.1.72へ固定した案を保持する
* 結果XMLとログは成功・失敗にかかわらず`editmode-test-results`として14日間保存する設定。テスト起動前の失敗でファイルがなければActionsのジョブログを調べる。意図した対象・件数・結果・ログで実行成立を確認し、未実行・起動失敗・意図しない0件をテスト合格と扱わない。件数はその時点のテスト構成に合わせる
* Day25の事前確認：本人がUnity Hub 3.22.4でPersonalライセンスの条件に同意し、Unity_lic.ulfの存在を確認済み。GitHubの`UNITY_LICENSE`・`UNITY_EMAIL`・`UNITY_PASSWORD`は登録名を提示画像で確認した。Codexはライセンス・Secretsの値を読まず、同意操作も代行していない
* Actionsの「Allow all actions and reusable workflows」、基本権限「Read repository contents and packages permissions」をDay25の提示画像で確認済み。ライセンス・認証を含むCI上の動作確認はこれから行う

## Gitと保存状態

確認日：2026-10-27（JST）。Day26の対象日も2026-10-27。

* 文書保存前のmain：最新のorigin/mainを取得し、ローカルmainとともに`0e01cfb9c97e09abdaa5420f7b185fd4eeaca694`で一致することを確認。Day26の本人承認により、`AGENTS.md`とこの文書だけを一時worktreeからmainへ直接コミット・Pushする。保存後のコミットIDと同期結果はGitとDay26終了報告を参照する
* 元のCI作業フォルダ：`C:\Users\draqu\Desktop\vivienne-qa-lab\game-set-automation-lab`。ブランチは`ci/editmode-tests`、HEADは`0e01cfb9c97e09abdaa5420f7b185fd4eeaca694`のまま保持する。mainの取り込み、CI関連のコミット・Push、PR作成は今回行わない
* CIフォルダの既存ステージは空。Workflow案は本文と未追跡状態を保持する。更新した2文書は元フォルダでも参照でき、mainへ保存する内容と同一の未コミット差分として残す。mainの保存完了と、CIブランチの未コミット状態は区別する
* 初期移動機能と以前の学習支援方針は[PR #2](https://github.com/daiki-vivienne/game-set-automation-lab/pull/2)で2026-10-23にmainへ通常マージ済み（220f9b6）。`feature/player-movement`はc0b5b01で残している。ゲーム・テストコードは本人が実装し、Codexによるコードへの直接編集は依頼された説明コメントのみ
* コミット・PR文を作成者の視点で書く方針のAGENTS.mdへの追記は、将来の文書見直し候補として保留

## 未完了と今後の作業

1. Milestone 2は「本人が自動化したい作業を日本語で整理する」から再開する。必要な説明・段階的なヒントを受けて本人がWorkflowを作り、本人が考えて書く部分とCodexへ委ねる部分を分ける。既存案は本人の作成・理解確認済みの成果には含めない
2. 本人のWorkflow作成・確認後、差分とメッセージ全文を提示し、本人の依頼でCIブランチへCommit・Pushする。CI上で意図した対象の実行件数・結果・ログを確認し、適切な節目で本人と対象・復旧方法を合意して意図的な失敗の検出と復旧後の成功を試す。未実行の段階を成功と扱わない
3. 壁・マップ外への入力と向きだけ変更する命令は各1例を確認済み。他方向・非0ターンなどへの拡張は必要性を見て検討する。敵の占有判定・戦闘は現在の範囲外
4. 必要ならテスト名とコメントを目的に合わせて整理する。FloorCellsWalkableは床・壁とYと行番号の対応を確認している。IsWalkableIsFalse内の地図説明コメントはコピー元の上下対応の説明が残っている
5. Milestone 2の後、Milestone 3でUnity表示・入力接続、PlayModeテストの対象を検討する。最初のCIが使える段階以降は、実際に見つけた不便を本人が一つ選び、必要に応じて小さく改善して前後を確認する。困っていない箇所の改修や大きな機能追加は必須にしない
6. ゲーム仕様書・テスト戦略書のリポジトリ内の配置、README追記、Botの導入時期は今後の検討事項

行末の余分な空白などの体裁は後で見直す対象。未決のゲーム仕様や大きな設計判断は、選択肢と影響を整理してChatGPT Workで相談する。

## 関連記録と再開方法

* [ROADMAP_v0.1.md](https://drive.google.com/file/d/1jaU-5QqbKx3NrHswDS7wCpg1-ttMNNW7/view?usp=drivesdk)：Day25にMilestone 2とCIの成長方針を読み取り確認。移動ルール、最初のCI、Unity表示・入力接続の順に進める
* [TEST_STRATEGY_v0.1.md](https://drive.google.com/file/d/1FCNIBfNum6iua3B7nrhqTkZYCA-alcMy/view)：Day26に更新済みの16節（CIの実行成立と合格の区別）・23節（学習とAI利用）を読み取り確認。日々の支援手順はAGENTS.mdへ統合済み
* [仕様・設計資料フォルダ](https://drive.google.com/drive/folders/1JPWF1LwqBv_yED0qeqgQlT1r14COcE1Q)：決定済みのゲーム仕様はGAME_SPEC、設計はTECH_ARCHITECTURE、テスト方針はTEST_STRATEGYの対応する最新資料を参照。2026-10-22のDay24開始時に各v0.1資料の今回に関わる箇所とTECH_ARCHITECTUREの4.2節を読み取り確認済み
* [TECH_ARCHITECTURE_v0.1.md](https://drive.google.com/file/d/10wfhZBM1yn0YHzlrLL0v2yc-4tp5_Ab0/view?usp=drivesdk)：4.2節にGameStateでの移動処理とB案の受け渡し方針・採用理由を記録済み
* [Vivienne QA Labの記録フォルダ](https://drive.google.com/drive/folders/18TJtEXtzkT8-hLh6TFMAtqBlVua3gk5F)
* [制作ログ（Day25）](https://docs.google.com/document/d/1Stl6PGy5P4TS5UIOhCWRzSCVNpY8wF6P0aLhnROlIaQ/edit?pli=1&tab=t.0#heading=h.998ibj6ous9j)
* [作業時間表](https://docs.google.com/spreadsheets/d/1He70CnsMwKe-eGSXBvuIYKwaueKuBBzZpusN-AjDOOc/edit)

再開時はAGENTS.mdとこの文書を読み、Gitの状態・保存済みコード・Unityの確認状況を照合する。背景が必要なら、AGENTS.mdに従ってDriveの関連記録を読み取り確認する。制作ログ・復習資料・作業時間表の更新はChatGPT Workが担当し、この文書には作業時間を記載しない。
