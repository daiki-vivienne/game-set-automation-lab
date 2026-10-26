# game-set-automation-lab 現在の状況

最終更新：2026-10-23（Day24終了・2026-10-22開始）

## 現在地と次回の開始点

固定マップの範囲・床判定、B案のGameState.Moveによる上下左右の床移動を実装済み。Day24は本人が壁・マップ外への入力テストに加え、ChangeFacing(Direction direction)とChangeFacingKeepsPositionAndTurnCountを追加した。本人の依頼でCodexが説明コメントを追記した後、2026-10-23に本人がEditModeのRun Allを再実行し、全14件成功を提示画像・保存済みコードで確認。Move本体と既存コンストラクターはDay24には変更していない。初期移動機能の差分を確認し、学習支援方針と実装・テストを別々にコミット。Git用のGitHub認証を本人が登録した後、作業ブランチへのPushと同期確認まで完了した。

初期移動機能の[PR #2](https://github.com/daiki-vivienne/game-set-automation-lab/pull/2)はCodex review完了・指摘なしを確認し、本人が2026-10-23にmainへマージ済み。ローカルmainとorigin/mainもマージコミット220f9b6へ同期した。床移動は各方向1例、壁・マップ外への入力は右方向各1例、向きだけ変更する命令はUpからRight・開始0ターンの1例を確認済み。次回は保存状態を照合し、次の機能・追加ケースの要否をWorkと相談する。新しいゲーム・テストコードを書く前に、必要なら次の作業ブランチを作る。

学習支援はAGENTS.mdの「学習者主体の実装」に従い、目的から本人が考える時間を先に取る。詳しい復習・説明方法の改善点はChatGPT Workへの報告と関連記録を参照する。

実装・テストは本人が担当し、Codexは説明・レビュー・確認を支援する。詳しい運用はAGENTS.md、決定済みの仕様・設計・テスト方針は末尾の仕様資料、過去の学習・判断の経緯は必要なGoogle Drive記録を参照する。

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

* 確認日：2026-10-23（Day24）
* 方法：本人がUnity Test RunnerのEditModeでRun Allを実行。Codexは保存済みコードと提示画像で確認。コマンドライン実行は行っていない
* 結果：成功14件・失敗0件・スキップ0件。GameStateTestsの8件とFixedMapTestの6件。IsWalkableIsFalseの4方向のAssertは1テストとして数える
* Day24開始時の既存11件は本人の通過報告あり。壁追加後の12件、マップ外の配置修正後の13件、向きだけ変更する命令追加後の14件成功を提示画像で確認した
* Codexが本人の依頼でGameState.csとGameStateTests.csに説明コメントのみ追記し、処理・宣言・期待値が変わっていないことを比較確認。その後、本人が再実行した全14件成功を最新画像で確認した。以降、ゲーム・テストコードは変更していない。AGENTS.mdとPROJECT_STATUS.mdの文書差分も確認
* Codex reviewはPR作成をきっかけに自動実行され、対象c0b5b01で完了・指摘なし・未解決指摘なしを結果データで確認した。2026-10-23にmainへマージ後、c0b5b01と220f9b6の保存済みツリーが完全一致することをGitで確認。マージ後のUnity再実行は行っていない

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
* 敵・戦闘・斜め移動・ランダム生成・Bot・CIは今回の範囲外
* 座標・原点・文字地図の保存方針は本人が了承し、ChatGPT Workへ共有済み

## Gitと保存状態

* 現在のブランチ：`main`。初期移動機能を統合したマージコミットは`220f9b6bf63e1451d22b047891d1851276ad1747`。マージ直後にローカルmain・origin/main・GitHub側の一致を確認し、その後の変更は現在地文書のみ。最新HEADと同期状態はGitで確認する
* 2026-10-23に本人の依頼でCodexが差分をレビューし、全文を提示したメッセージでローカルコミットを作成。学習支援方針は`85c41d8`（AGENTS.mdのみ）、初期移動機能は`d1db1f0`（GameState.cs・GameStateTests.cs・FixedMapTest.cs）。Day19のIsWalkableIsFalse追加も含む。ステージしたコードの本文と保存済みコードの一致を確認済み
* PROJECT_STATUS.mdの確認結果とGit状態は文書専用コミット`8e7bfbc`に保存し、Push完了の記録は`c0b5b01`でコミット・Push済み。PR作成・レビュー・マージと同期の今回の記録には、本人が了承した「現在地文書だけをmainへ直接コミット・Pushする」運用を適用し、新しいPRは作成しない
* 当初はGitの認証管理に利用可能なGitHubアカウントがなくPushに失敗。本人が通常のPowerShellからGit Credential Managerへログイン登録した後、CodexがPushのdry-run成功を確認し、作業ブランチをPushした。2026-10-23にローカルHEAD・origin/feature/player-movement・GitHub側が`c0b5b01`で一致することを確認
* [PR #2](https://github.com/daiki-vivienne/game-set-automation-lab/pull/2)：`feature/player-movement` → `main`。12ファイルの変更、14件成功の確認結果と未確認範囲を日本語・英語で記載。PR本文は作成者自身の視点へ修正済み。Codexの自動レビュー完了後、本人が通常のマージで取り込んだ。マージコミットは`220f9b6`（固定マップと初期移動を統合 / merge fixed map and basic movement）
* `85c41d8`のAGENTS.mdの学習支援方針もPR #2でmainへ反映済み。作業ブランチ`feature/player-movement`は`c0b5b01`のまま残し、削除していない。PR本文・コミット文を作成者の視点で書く方針をAGENTS.mdへ追記する件は、本人の依頼により今後の文書見直し時の候補として保留
* stashはない。未コミットだったPROJECT_STATUS.mdは内容を保持したままmainへ切り替え、マージ後の現在地へ更新した。無関係なファイル・設定・アセットは変更していない
* ゲーム・テストコードは本人が記述。Codexのコードへの直接編集は、本人が依頼した説明コメントのみ

## 未完了と今後の作業

1. 初期移動機能は既存14件成功、差分レビュー、Push、Codex review完了・指摘なし、PR #2のmainへのマージとローカル同期まで完了。次回は文書の保存とGit状態を照合し、次の機能・確認範囲をWorkで相談する。ゲーム・テストコードの変更は新しい作業ブランチで進める
2. 壁・マップ外への入力と向きだけ変更する命令は各1例を確認済み。他方向・非0ターンなどへの拡張は必要性を見て検討する。敵の占有判定・戦闘は今回の範囲外
3. 必要ならテスト名とコメントを目的に合わせて整理する。FloorCellsWalkableは床・壁とYと行番号の対応を確認している。IsWalkableIsFalse内の地図説明コメントはコピー元の上下対応の説明が残っている
4. 移動と壁判定の確認後、Unityでの表示・入力接続、PlayModeテストの対象を検討する
5. ゲーム仕様書・テスト戦略書のリポジトリ内の配置、README追記、CI・Botの導入時期は今後の検討事項

行末の余分な空白などの体裁は後で見直す対象。未決のゲーム仕様や大きな設計判断は、選択肢と影響を整理してChatGPT Workで相談する。

## 関連記録と再開方法

* [仕様・設計資料フォルダ](https://drive.google.com/drive/folders/1JPWF1LwqBv_yED0qeqgQlT1r14COcE1Q)：決定済みのゲーム仕様はGAME_SPEC、設計はTECH_ARCHITECTURE、テスト方針はTEST_STRATEGYの対応する最新資料を参照。2026-10-22のDay24開始時に各v0.1資料の今回に関わる箇所とTECH_ARCHITECTUREの4.2節を読み取り確認済み
* [TECH_ARCHITECTURE_v0.1.md](https://drive.google.com/file/d/10wfhZBM1yn0YHzlrLL0v2yc-4tp5_Ab0/view?usp=drivesdk)：4.2節にGameStateでの移動処理とB案の受け渡し方針・採用理由を記録済み
* [Vivienne QA Labの記録フォルダ](https://drive.google.com/drive/folders/18TJtEXtzkT8-hLh6TFMAtqBlVua3gk5F)
* [制作ログ（Day24）](https://docs.google.com/document/d/1e9YuRkN-O7SCJHZJAl5o2cZu2l9oBr9ichaelOCgE0c/edit?pli=1&tab=t.0#heading=h.clobmuuk62bd)
* [作業時間表](https://docs.google.com/spreadsheets/d/1He70CnsMwKe-eGSXBvuIYKwaueKuBBzZpusN-AjDOOc/edit)

再開時はAGENTS.mdとこの文書を読み、Gitの状態・保存済みコード・Unityの確認状況を照合する。背景が必要なら、AGENTS.mdに従ってDriveの関連記録を読み取り確認する。制作ログ・復習資料・作業時間表の更新はChatGPT Workが担当し、この文書には作業時間を記載しない。
