# game-set-automation-lab 現在の状況

最終更新：2026-11-05（Day30・2026-11-04分、初回CIは実行ツールの引数エラーで停止）

## 現在地と次回の開始点

進行中の対象はROADMAPのMilestone 2「最初のCI」。Day30は本人が`.github/workflows/ci-practice.yml`へ、UnityのEditModeテスト実行・Secrets参照・実行条件・結果ファイル保存・ジョブ権限を追加した。項目名・値・部分例はCodexが提示し、本人が記述と字下げ・項目名の修正を行った。本人の依頼でCodexが振り返り用の説明コメントを追加し、コメント追加前後で設定内容が同じことを確認した。独力での設計・理解の定着とは区別する。

現在のWorkflowは、チェックアウト・メッセージ表示・Tests一覧・Unityテスト実行・結果保存の5手順。本人が`efae84e`へCommit・Pushし、[初回CI](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/41867838001)を実行した。準備用3手順は成功したが、GameCI CLIが`Unknown argument: noCoverageEnabled`を出して終了コード1となり、Unityテストは未実行。結果保存ステップはsuccessだが、対象ファイルなしの警告が出ており、Artifacts一覧も空。ゲーム・テストコードは変更していない。既存の`.github/workflows/editmode-tests.yml`は未追跡案として保存・実行対象から外す。

初回CIではActionが`coverageEnabled: false`を`--no-coverageEnabled`へ変換し、CLIが拒否した。CodexがAction・CLIのソースを照合し、環境変数から同じ値を渡す案を提示した。本人がwithの該当行を削除し、envへ`GAME_CI_COVERAGE_ENABLED: 'false'`を追加済み。引数解析ライブラリ単体でfalseとして読めることと、修正後のactionlint終了コード0・差分チェック通過をCodexが確認した。修正は未コミット・未Pushで、CIでは未検証。次は保存内容を共有してCommit・Pushを相談し、引数エラーが解消するか確認する。認証・起動とテスト自体の成否を分け、実行成立後にXMLの対象・件数・結果とログを照合する。

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

* Day30初回CI：run ID `41867838001`、対象`efae84ee1c0bf9dacd08e67c1c82c356361be195`、ジョブ`editmode`はfailure。GameCI CLI v0.1.72へ`--no-coverageEnabled`を渡した直後に`[WARN] Unknown argument: noCoverageEnabled`が出て終了コード1。Unity認証・テスト起動の成功は未確認で、テストの成功数・失敗数・スキップ数は集計されていない。保存ステップはsuccessでも、`No files were found ... No artifacts will be uploaded.`と出ており、Artifactsは0件。環境側の停止とテスト自体の失敗を区別する
* Day30（確認日2026-11-05）は本人が追加したUnity実行・結果保存・権限の設定を、Codexがactionlint 1.7.12で確認して終了コード0。差分チェックも通過。依頼された説明コメントの追加では、非コメント部分が追加前と一致することも確認した。構文検証は認証・Unity起動・テストの実行成功を示さない
* Day29（2026-10-31）は本人がDay28の実行ログを確認したと報告。CodexもGitHub APIで同じ実行のジョブと3手順のsuccessを読み取り再確認した。本人による表示名変更後の`ci-practice.yml`はactionlint 1.7.12と差分チェックを通過した。その後、本人の依頼でCommit・Pushし、[変更後のCI Practice](https://github.com/daiki-vivienne/game-set-automation-lab/actions/runs/41201831134)もsuccess。Codexが新しいステップ名と3手順の成功を確認し、ログで`f0d26cd`の取得、`CI practice`、Tests内の4項目の出力を確認した。Unityテストは未実行。過去のログには当時の表示名が残る
* 最新のUnity確認はDay25（2026-10-26分、確認日2026-10-27）のローカル実行。本人がUnity Test RunnerのEditModeで実行し、成功14件・失敗0件・スキップ0件を提示画像と保存済みTestResults.xmlでCodexが確認した。XMLはtotal=14、result=Passed、passed=14、failed=0、skipped=0、inconclusive=0。Console画像には通常ログ3件が表示され、赤いエラーは見当たらなかった
* Day28は本人がPowerShellから移動前の練習ファイルにactionlint 1.7.12を実行し、終了コード0を画面で確認した。Codexも移動・表示名変更後の`.github/workflows/ci-practice.yml`を同じツールで検証し、終了コード0。差分チェックも通過した
* Day28のGitHub Actions実行：Pushを契機に`CI Practice`（run ID `40872120302`、対象`59299f0b3bdfd58ff213942445bd9867dc7d0fd0`）が実行され、ジョブ`editmode`と指定した3ステップはすべてsuccess。ログで対象コミットの取得、`echo "CI practice"`の出力、`ls Assets/_Project/Tests`による4項目の出力をCodexが確認した。実行環境はUbuntu 24.04。Unityの認証・テスト起動は含まず、Unityテストは今回未実行。ジョブ識別名`editmode`自体がテスト実行を意味するものではない
* Day27は本人が書いた`.github/editmode-tests-learning.yml`に対し、Codexがactionlint 1.7.12を実行し、終了コード0で構文検証を通過した。これはecho・Unityテスト・CIの実行成功を示さない。Day27はUnity・CIの再実行をしていない
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

* 現在の実行対象は`.github/workflows/ci-practice.yml`、表示名は`CI Practice`。Pushを契機にUbuntu 24.04で、取得・echo・Tests一覧に続き、GameCIのEditMode実行と結果保存を試す設定。構文検証は通過したが、初回CIはCLIの引数エラーで停止し、Unityテストと結果ファイル保存は成立していない
* Unity Test Runnerは`32e57712352b500e17974b245a6dce9e11a73213`（v4.4.0）、内部のCLIは`v0.1.72`、実行イメージは`unityci/editor:ubuntu-6000.3.24f1-base-3.2.2`へ固定。Unity版はプロジェクトから読み、対象はEditModeのみ。コードカバレッジは無効。Day30に公式の設定仕様とCLIの公開、同Unity版のイメージの公開を確認したが、実際の起動は未確認
* 結果の作成先と保存元はともに`artifacts/editmode`。保存名は`editmode-test-results`で、`if: always()`によりテスト失敗時も生成済みファイルの保存を試す。Actionsの実行ログと、別に生成されるXMLなどのファイルを区別する。ファイル未生成時はジョブログから調査する
* ジョブの権限は`contents: read`と`checks: write`。Unity用の3つのSecretsは参照だけを記述し、値は読んだり表示したりしていない。GitHub上のテスト結果の登録に使う権限と、Unity認証は別の役割として扱う
* `.github/workflows/editmode-tests.yml`はDay25にCodexが作成した未追跡案。Day26も本文と未追跡状態を保持し、編集・コミット・Push・CI実行はしない
* 案の実行対象はブランチへのPushとmain向けPRでのEditModeのみ。GitHub-hosted Ubuntu 24.04とGameCIを使い、外部forkのPRはUnity用Secretsが渡らないためジョブをスキップする
* Unity版はProjectVersion.txtから読み、`unityci/editor:ubuntu-6000.3.24f1-base-3.2.2`の公開をDay25に確認済み。ActionはコミットSHA、GameCI CLIはv0.1.72へ固定した案を保持する
* 結果XMLとログは成功・失敗にかかわらず`editmode-test-results`として14日間保存する設定。テスト起動前の失敗でファイルがなければActionsのジョブログを調べる。意図した対象・件数・結果・ログで実行成立を確認し、未実行・起動失敗・意図しない0件をテスト合格と扱わない。件数はその時点のテスト構成に合わせる
* Day25の事前確認：本人がUnity Hub 3.22.4でPersonalライセンスの条件に同意し、Unity_lic.ulfの存在を確認済み。GitHubの`UNITY_LICENSE`・`UNITY_EMAIL`・`UNITY_PASSWORD`は登録名を提示画像で確認した。Codexはライセンス・Secretsの値を読まず、同意操作も代行していない
* Actionsの「Allow all actions and reusable workflows」、基本権限「Read repository contents and packages permissions」をDay25の提示画像で確認済み。ライセンス・認証を含むCI上の動作確認はこれから行う

## Gitと保存状態

確認日：2026-11-05（JST）。過去の保存結果とDay30の初回実行結果を区別して記録する。

* Day27の保存結果：`ci/editmode-tests`で練習ファイル`.github/editmode-tests-learning.yml`とPROJECT_STATUS.mdを`3a78bff`（Workflowの基本設定を練習 / practice basic workflow configuration）へローカルコミット済み。開始時からの現在地文書の追記を保持し、Day27の到達点を統合して保存した。Day27終了時点ではPush・PR作成・mainへのマージは行わなかった
* Day28の保存結果：本人の依頼で、練習ファイルの移動・追加した2手順・表示名変更と、PROJECT_STATUS.mdの既存変更・Day28の実行準備を`59299f0`（CIの取得・一覧表示を練習 / practice checkout and file listing in CI）へコミットし、`origin/ci/editmode-tests`へPush済み。Day27の`3a78bff`もこのPushで送信した。HEADとoriginの作業ブランチは`59299f0`で一致。PR作成・mainへのマージは未実施
* Day29の保存結果：既存のPROJECT_STATUS.mdの実行成功・保存状態の追記とDay28制作ログリンク更新を保持したうえで、本人の表示名変更とDay29の現在地更新を`f0d26cd`（チェックアウトの表示名と現在地を更新 / update checkout label and project status）へコミットし、本人の追加依頼で作業ブランチへPush済み。HEADとローカルのorigin作業ブランチは`f0d26cd`で一致。PR作成・mainへのマージは未実施
* Day30の初回実行前確認：HEADは`f0d26cd`で、開始時にGitHub側の作業ブランチとの一致を確認した。ステージは空。Workflowへの5手順化・説明コメントと、本書のDay29実行後の追記・制作ログリンク更新・Day30の現在地更新が未コミット。既存のローカル追記は保持して統合した。未追跡は既存のCodex作成案`.github/workflows/editmode-tests.yml`のみで、保存対象から外す
* Day30の保存結果：本人がWorkflowと本書を`efae84e`（EditModeテストのCI設定を追加 / configure EditMode tests in CI）へCommit・Pushした。HEADとローカルのorigin作業ブランチは同コミットで一致。ステージは空。その後のWorkflowのcoverage設定の渡し方の修正と、本書への初回CI停止・修正・保存状態の追記が未コミット。未追跡の既存案はコミットに含めていない。PR作成・mainへのマージは未実施

* 文書保存：本人承認により、確認済みの`AGENTS.md`とDay26終了時点の現在地文書だけを一時worktreeからmainへ直接コミット・Push済み。コミットは`9842c601416c8e329489b72ef6f3abf7770f3076`（学習方針と現在地を更新 / update learning guidance and project status）。Push後にGitHub側main・origin/main・ローカルmainの一致を確認し、一時worktreeは片付け済み
* Day26の同期：作業フォルダは`C:\Users\draqu\Desktop\vivienne-qa-lab\game-set-automation-lab`、ブランチは`ci/editmode-tests`。本人の追加承認で、追跡対象の本文がmain保存版と一致し、ステージが空であることを確認して、CIブランチと比較基準をmainへ揃えた。この時点ではCIのHEAD・ローカルmain・origin/mainが上記9842c60で一致し、新しいコミット、CIブランチのPush、PR作成は行わなかった
* Day26終了時のローカル追記：`PROJECT_STATUS.md`のGit欄を追加整理後の現在地へ更新し、制作ログリンクをDay26へ変更して未コミットで保持した。Day27にこの既存変更を現在地文書へ統合し、`3a78bff`へコミット済み。`.github/workflows/editmode-tests.yml`は本文を保持した未追跡案。AGENTS.mdはmain保存版と同一
* 初期移動機能と以前の学習支援方針は[PR #2](https://github.com/daiki-vivienne/game-set-automation-lab/pull/2)で2026-10-23にmainへ通常マージ済み（220f9b6）。`feature/player-movement`はc0b5b01で残している。ゲーム・テストコードは本人が実装し、Codexによるコードへの直接編集は依頼された説明コメントのみ
* コミット・PR文を作成者の視点で書く方針のAGENTS.mdへの追記は、将来の文書見直し候補として保留

## 未完了と今後の作業

1. Milestone 2はcoverage設定の渡し方を修正したWorkflowのCommit・PushとCI再実行から続ける。修正は構文検証のみ通過し、CIでは未検証。`Unknown argument: noCoverageEnabled`の解消をまず確認し、その後の認証・起動の成否とテストの対象・件数・結果・ログを区別して確認する
2. 初回実行の成立後、適切な節目で本人と対象・復旧方法を合意して、意図的なテスト失敗の検出と復旧後の成功を試す。本人が書く部分とCodexへ委ねる部分を分け、既存のCodex作成案やヒントで示した設定を独力の成果と扱わない
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
* [制作ログ（Day29）](https://docs.google.com/document/d/1BgIG3JprFkyc5kgNbp_V0nwNyUJbRLGZrXBWZeQ018U/edit?pli=1&tab=t.0#heading=h.coj1017xlrs3)
* [作業時間表](https://docs.google.com/spreadsheets/d/1He70CnsMwKe-eGSXBvuIYKwaueKuBBzZpusN-AjDOOc/edit)

再開時はAGENTS.mdとこの文書を読み、Gitの状態・保存済みコード・Unityの確認状況を照合する。背景が必要なら、AGENTS.mdに従ってDriveの関連記録を読み取り確認する。制作ログ・復習資料・作業時間表の更新はChatGPT Workが担当し、この文書には作業時間を記載しない。
