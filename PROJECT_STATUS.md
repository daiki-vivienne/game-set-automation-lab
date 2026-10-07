# game-set-automation-lab 現在の状況

最終更新：2026-10-07（Day18終了）

## 現在地と次回の開始点

固定マップの高さ・横幅・座標の範囲確認に加え、IsWalkableを実装済み。地図外ならfalse、範囲内ではYから行番号へ変換して地形を読み、床ならtrueを返す。床・壁と上下の行の対応を確認するテストを含め、EditModeの全6件が成功。移動処理は未実装。

次回は、地図外(5, 1)でIsWalkableがfalseを返すテストを本人が追加するところから再開する。地図外の文字を読まずに答えを返せることを確認した後、床へ1マス移動する処理とテストへ進む。目的を説明して本人が試す時間を取り、詰まった部分だけ助ける。

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
| Assets/_Project/Core/GameState.cs | 開始位置・向きを受け取り、位置・向き・ターン数を保持。X・Y・Facing・TurnCountは読み取り専用。作成直後のターン数は0 |
| Assets/_Project/Core/FixedMap.cs | 文字列配列で地図を保持。Height・Width、IsInside(x, y)の範囲確認、IsWalkable(x, y)の床判定 |
| Assets/_Project/Tests/EditMode/GameStateTests.cs | InitialStateIsSetで初期状態を確認 |
| Assets/_Project/Tests/EditMode/FixedMapTest.cs | HeightAndWidthAreCorrect、EdgePositionsAreInsideMap、OutsidePositionsAreNotInsideMap、IsWalkableIsTrue、FloorCellsWalkableの5件 |

テスト用地図は3行・横幅5、各行が同じ長さ。本番マップの大きさ・配置を決めたものではない。高さと幅を異なる値にし、幅の処理が誤って行数を返しても区別できるようにしている。FloorCellsWalkableでは上段中央を壁、下段中央を床とし、上下の行を取り違えてもテストで区別できる配置にしている。

## 最新の確認結果

* 確認日：2026-10-07
* 方法：本人がUnity Test RunnerのEditModeでRun Allを実行。Codexは提示画像と保存済みコードで確認。コマンドライン実行は行っていない
* 結果：成功6件・失敗0件・スキップ0件。IsWalkable実装と2件のテスト追加後に、既存テストも含めて全件を認識・実行できている
* 6件成功後、本人の依頼でCodexがFixedMap.csとFixedMapTest.csに短い説明コメントのみ追記した。コメント以外の処理・期待値は変更していない。追記後も本人がテストを再実行し、全6件通過したとの報告あり

| テスト | 確認した内容 |
|---|---|
| InitialStateIsSet | 作成直後のX=1、Y=1、Facing=Up、TurnCount=0 |
| HeightAndWidthAreCorrect | 渡した地図の高さ3・横幅5 |
| EdgePositionsAreInsideMap | (0, 0)、左端(0, 1)、右端(4, 1)、上端(2, 2)、下端(2, 0)でtrue |
| OutsidePositionsAreNotInsideMap | 右外(5, 1)、左外(-1, 1)、上外(2, 3)、下外(2, -1)でfalse |
| IsWalkableIsTrue | 床(1, 1)でIsWalkableがtrueを返す |
| FloorCellsWalkable | 上段中央の壁(2, 2)でfalse、下段中央の床(2, 0)でtrue |

IsInsideは範囲確認で、歩行可能かの判定ではない。IsWalkableは範囲確認後にHeight - 1 - yで行を選び、Xの位置の文字が床かを比較する。IsWalkableに地図外を渡すテストは未追加。Day18のゲーム・テストコードは本人が実装し、Codexは説明・レビュー・結果確認・現在地の記録を行った。

## 決定済みの仕様と前提

* 固定マップで上下左右へ1マス移動。右でX増加、左でX減少、上でY増加、下でY減少。原点は左下のマス(0, 0)
* 文字地図は上から行順に保存する。この保存順とYの増加方向が逆なので、範囲内のYに対する行番号はHeight - 1 - yで求める。IsWalkable内で変換を実装済み
* 移動入力を受けると、移動先にかかわらず入力方向を向く。歩行可能な空きマスなら1マス進み、ターン数を1増やす
* 壁・マップ外・敵のいるマスでは向きだけ変わり、位置とターン数は変わらない。敵への移動入力は自動攻撃にせず、攻撃ボタンでのみ攻撃する
* 向きだけ変更する命令は、位置とターン数を変えない
* マップ外かを確認してから地形を参照する
* Game Coreは通常のC#とし、UnityEngineに依存させない。キー入力・画面表示は後で接続し、今回はテストから命令を渡す
* 敵・戦闘・斜め移動・ランダム生成・Bot・CIは今回の範囲外
* 座標・原点・文字地図の保存方針は本人が了承し、ChatGPT Workへ共有済み

## Gitと保存状態

* ブランチ：`feature/player-movement`
* HEAD：`7e53223`（ゲーム状態と初期状態テストを追加 / add game state and initial state test）
* 上記のローカルコミットはDirection・GameState・GameStateTestsと各.meta、当時のPROJECT_STATUS.mdを含む。featureのコミットはPush・PR作成していない
* 未コミット変更：9ファイル。Direction.cs、GameState.cs、GameStateTests.csのコメント、FixedMap.csと.meta、FixedMapTest.csと.meta、AGENTS.md、PROJECT_STATUS.md
* 新規2つのC#ファイルは空の内容がステージされ、最新本文は作業フォルダに保存されている。次回コミット時は最新本文をステージし直す
* AGENTS.mdの仕様資料・制作ログの参照方法を、本人の承認によりmainへ反映済み（d6cd525）。2026-10-07にローカルmain・origin/main・GitHubのmainが同じコミットであることを確認。GitコマンドのPushは認証情報を取得できず失敗したため、GitHub接続から反映した。元のローカルコミット3e3a04bとは内容が完全に一致し、変更対象はAGENTS.mdだけ。今回のPROJECT_STATUS.md更新はコミットに含めていない
* featureには新しいmainの履歴をまだ取り込んでいないため、AGENTS.mdはこのブランチ上では未コミット差分として表示される。現在のファイル内容はmainのAGENTS.mdと一致する。PROJECT_STATUS.mdの整理とゲーム・テストの変更はローカル保存済み・未コミット
* ゲーム・テストコードは本人が記述。Codexのコードへの直接編集は、本人が依頼した説明コメントのみ

## 未完了と今後の作業

1. IsWalkableに地図外(5, 1)を渡してfalseになるテストを追加し、その後は他方向の外側も確認する
2. 必要ならテスト名を目的に合わせて整理する。FloorCellsWalkableは床と壁に加え、Yと行番号の対応も確認している。説明コメントは本人の依頼で追記済み
3. 隣り合う床のある地図で1マス移動し、位置・向き・ターン数をテストする。その後、上下左右の移動、壁・マップ外への移動入力と向きだけ変更する命令を確認する
4. 移動と壁判定の確認後、Unityでの表示・入力接続、PlayModeテストの対象を検討する
5. ゲーム仕様書・テスト戦略書のリポジトリ内の配置、README追記、CI・Botの導入時期は今後の検討事項

行末の余分な空白などの体裁は後で見直す対象。未決のゲーム仕様や大きな設計判断は、選択肢と影響を整理してChatGPT Workで相談する。

## 関連記録と再開方法

* [仕様・設計資料フォルダ](https://drive.google.com/drive/folders/1JPWF1LwqBv_yED0qeqgQlT1r14COcE1Q)：決定済みのゲーム仕様はGAME_SPEC、設計はTECH_ARCHITECTURE、テスト方針はTEST_STRATEGYの対応する最新資料を参照。2026-10-07に同じDrive接続でフォルダと各v0.1資料の本文を読み取り確認済み
* [Vivienne QA Labの記録フォルダ](https://drive.google.com/drive/folders/18TJtEXtzkT8-hLh6TFMAtqBlVua3gk5F)
* [制作ログ（Day17）](https://docs.google.com/document/d/1uKcpppo6i1Cjc5McBR8bBEn1pf7Brm8R_qavf2ELklA/edit)
* [作業時間表](https://docs.google.com/spreadsheets/d/1He70CnsMwKe-eGSXBvuIYKwaueKuBBzZpusN-AjDOOc/edit)

再開時はAGENTS.mdとこの文書を読み、Gitの状態・保存済みコード・Unityの確認状況を照合する。背景が必要なら、AGENTS.mdに従ってDriveの関連記録を読み取り確認する。制作ログ・復習資料・作業時間表の更新はChatGPT Workが担当し、この文書には作業時間を記載しない。
