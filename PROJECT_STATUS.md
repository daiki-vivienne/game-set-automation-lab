# game-set-automation-lab 現在の状況

最終更新：2026-10-04（Day16：初期状態のEditModeテスト1件が成功した地点をローカルコミットに保存。Push・PR未実施）

## プロジェクト概要

Unity・C#・自動テスト・CI/CDを学び、ゲームSET/SDETを目指すための学習兼ポートフォリオ。

クリスタル洞窟を舞台にした、ターン制ローグライクゲームを制作する予定。

ゲームの完成だけでなく、以下を重視する。

* テストしやすい設計
* Seedによる再現性
* Unit・Integration・E2Eテスト
* 自動操作Bot
* CIによる自動ビルドとテスト
* 不具合の再現・調査・改善

## 開発環境

* Unity 6.3 LTS（6000.3.24f1）
* Universal 3D（URP）
* C#
* JetBrains Rider
* Unity Test Framework 1.6.0（`Packages/manifest.json`に登録済み。Day16にEditModeテスト1件の認識・実行・成功を確認）
* Git / GitHub
* Day13にPR #1を`main`へマージし、Riderでローカルの`main`を更新した

Unity、Rider、Gitの環境準備は完了している。

ローカルリポジトリ：

`C:\Users\draqu\Desktop\vivienne-qa-lab\game-set-automation-lab`

GitHubリポジトリ：

`daiki-vivienne/game-set-automation-lab`

## 完了したこと

* GitHubリポジトリをローカルへClone
* Unityプロジェクトをリポジトリ直下へ配置
* Unity向け`.gitignore`を設定
* Unity初期プロジェクトをコミット・Push
* Unityプロジェクトが正常に起動することを確認
* UnityのAsset Serializationを`Force Text`に設定
* UnityのVersion Controlを`Visible Meta Files`に設定
* Unityの外部スクリプトエディターをRiderに設定
* Unity Hubへ正しいプロジェクトフォルダを登録
* Riderから正しいソリューションを開けることを確認
* 古い`GameSetAutomationLab.sln`を削除
* 現在使用する`game-set-automation-lab.sln`だけを残した
* 学習方針と開発ルールを記載した`AGENTS.md`を作成
* 現在地を引き継ぐための`PROJECT_STATUS.md`を作成
* `AGENTS.md`、`PROJECT_STATUS.md`、`.gitignore`をコミット・Push
* 制作ログDay1〜Day11を作成
* 作業時間表を作成
* Day12に`chore/project-structure`ブランチを作成
* Unity Editorで`Assets/_Project`配下に`Core`、`Presentation`、`Automation`、`Tests/EditMode`、`Tests/PlayMode`を作成
* 上記5フォルダに各1つのasmdefを作成し、親フォルダを含む7つのフォルダと5つのasmdefに対応する`.meta`を確認
* 5つのasmdefの配置・設定・参照先を確認。両テスト用asmdefのApply後、Unity Editorに赤いエラーは出ていない
* Day13に構成変更のPR #1を作成。Codexのマージ前レビューとGitHubのCodex Botレビューを確認し、`main`へマージ
* PR #1のマージ後、Riderでローカルの`main`を更新
* 最初のGame Coreロジックを、固定マップでの上下左右1マス移動と壁判定に決定。ゲーム仕様書とテスト戦略書へルールを反映
* Day13の制作ログを作成し、作業時間表を更新
* `AGENTS.md`にコミット・PRの表記順、PR本文の項目、日本語レビューと進捗文書の更新基準を反映
* Day14に開発者本人がRiderで`feature/player-movement`ブランチを作成
* 開発者本人が`Assets/_Project/Core/Direction.cs`を作成し、`Up`・`Down`・`Left`・`Right`を持つ公開enumを定義
* 開発者本人が`Assets/_Project/Core/GameState.cs`を作成。位置・向き・ターン数のprivate変数、開始位置と向きを受け取るコンストラクター、横位置を読み取る`X`プロパティを記述
* Day14時点で上記2つのC#ファイルと対応する`.meta`の保存を確認。この時点ではUnityでのコンパイル・動作・テスト成功は未確認だった
* Day15に開発者本人が`GameState`へ`Y`・`Facing`・`TurnCount`の読み取り専用プロパティを追加。Codexは目的を説明し、本人の記述後に実ファイルを確認した
* Day15にUnity Consoleのログ・警告・エラーがすべて0件であることを本人の提示画像で確認。保存済みソースより新しい`Vivienne.GameCore.dll`があり、4つの公開プロパティを含むことを読み取り確認した。この時点ではCoreのコンパイル生成物は確認済みだったが、動作・テスト成功は未確認だった
* Day15に開発者本人が`Assets/_Project/Tests/EditMode/GameStateTests.cs`を作成。`public class GameStateTests`、`public void InitialStateIsSet()`と、その中で`GameState game = new GameState(1, 1, Direction.Up);`を書くところまで進めた。対応する`.meta`と保存済み本文を確認した
* Day16に開発者本人が`GameStateTests.cs`へ`using NUnit.Framework;`、4つの`Assert.That`、`[Test]`、保証内容を示すコメントを追加。初期状態がX=1・Y=1・Facing=Up・TurnCount=0であることを確認するテストを完成させた
* Day16に本人がUnity Test RunnerのEditModeから`InitialStateIsSet`を選択してRun Selectedで実行。認識画面と実行後の画面を提示し、Codexが成功1件・失敗0件・スキップ0件を確認した。コマンドライン実行は行っていない

## 現在の状態

* UnityのSampleSceneを開ける
* Day12に両テスト用asmdefのApply後、Unity Editorに赤いエラーは出ていない（開発者本人の確認）
* PR #1の構成変更は`main`へ取り込み済み。ローカルの`main`と取得済みの`origin/main`は`9e5bcfd`。以前の「進捗文書の更新コミットは未取得」という記述は現在のローカルGit状態に合わせて解消した。今回リモートへの再取得は行っていない
* 現在のブランチは`feature/player-movement`。Day16に本人の依頼で、初期状態テストが成功した地点までの関連変更をローカルコミットに保存。Push・PR作成は行っていない。最新のコミットIDと以後の未コミット変更は、再開時にGitで確認する
* コミット対象は`Direction.cs`、`GameState.cs`、`GameStateTests.cs`とそれぞれの`.meta`、`PROJECT_STATUS.md`の7ファイル。作成当初は3つのC#ファイルの空の内容がステージされていたため、Day16のコミット前に最新本文へ更新し、ステージ済み内容と作業フォルダの一致を確認する
* `PROJECT_STATUS.md`は初期状態テスト成功までの現在地として、関連コードと同じローカルコミットに含める。次回はブランチと保存済みファイル、未コミット差分を確認して再開する
* `Direction`は`public enum`で定義した方向の型。`GameState`とともにnamespaceはまだ記述していない
* `GameState`には`private Direction facing`、`private int x = 0`、`private int y = 0`、`private int turnCount = 0`がある
* `GameState(int startX, int startY, Direction startFacing)`で受け取った値を、`x`・`y`・`facing`に保存する。ターン数は0から始まる。開始位置と向きは作る側が指定する構成で、ゲーム本番の開始位置・向きを固定したわけではない
* 読み取り窓口は`public int X`・`public int Y`・`public Direction Facing`・`public int TurnCount`の4つ。各`get`は対応するprivateフィールドの現在の値を返す。外からの代入用の`set`はなく、テストだけでなく将来の画面表示からも使う窓口
* マップ、移動命令、向きだけ変更する命令は未実装
* CoreのC#にはUnityEngineへの依存がない。Day15にCoreのコンパイル生成物を確認済み。Day16にはUnity上でテストが認識・実行され、初期状態を生成して4つのプロパティから値を読む動作が確認できた。テスト実行可能な状態までコンパイルできていることも確認できた
* Unityの初期アセットに加え、`Assets/_Project`に`Core`、`Presentation`、`Automation`、`Tests/EditMode`、`Tests/PlayMode`の構成がある
* asmdefは`Vivienne.GameCore`、`Vivienne.Unity`、`Vivienne.Automation`、`Vivienne.EditModeTests`、`Vivienne.PlayModeTests`の5つ。対応する`.meta`と参照先のGUIDを確認済み
* `Vivienne.GameCore`はUnityエンジンへの参照を無効化。`Vivienne.Unity`と`Vivienne.Automation`は`Vivienne.GameCore`を参照
* 両テスト用asmdefは`Vivienne.GameCore`、Unity Test Runner、NUnitを参照し、`UNITY_INCLUDE_TESTS`を設定。EditModeはEditor専用、PlayModeは全プラットフォーム対象
* `GameStateTests.cs`にはNUnitの`[Test]`を付けた`InitialStateIsSet()`が1つある。`new GameState(1, 1, Direction.Up)`で作った状態のX・Y・Facing・TurnCountを、それぞれ1・1・Direction.Up・0と比較する。Day16のEditMode実行結果は成功1件・失敗0件・スキップ0件で、本人の提示画像と保存済みソースを照合した。移動処理や別の開始値の動作まで確認した結果ではない
* 最初の移動ルール：入力方向を向き、歩行可能な空きマスなら1マス進んで1ターン消費する。壁・マップ外・敵のいるマスでは向きだけ変わり、位置とターン数は変わらない。敵への移動入力は自動攻撃にならない
* 向きだけ変更する命令は位置とターン数を変えない。マップ外かを確認してから地形を参照する。敵、斜め移動、ランダム生成、戦闘、Bot、CIは今回の実装範囲に含めない
* 実装の主担当は開発者本人
* ChatGPT Workは、仕様・設計・学習支援・制作ログ・作業時間・次回計画を担当する
* Codexは、リポジトリを参照した相談・既存コードの説明・変更箇所の案内・差分レビュー・テスト実行・エラー調査を担当する
* ゲーム仕様、プレイヤーから見える挙動、設計方針、テスト方針の変更は、ChatGPT Work側で相談して決める
* `PROJECT_STATUS.md`は、リポジトリ側の技術的な現在地を記録するために使用する
* ChatGPT Workがこのファイルを直接読めない場合は、Codexの「ChatGPT Workへの作業報告」で内容を戻す
* 作業時間は専用の作業時間表のみで管理する
* `PROJECT_STATUS.md`と制作ログには、開始時刻、終了時刻、制作時間、累計時間を記載しない
* Skillsは現在の運用を数回試してから、終了処理のSkill化を検討する
* MCPは現時点では導入しない

以前表示された`Token Exchange`エラーは、Unityのオンライン認証通信による一時的なもので、ゲームやC#コードのエラーではない。Clear後に再発していない。

## Day14の学習と引き継ぎ

* 今回は実装を急がず、型・変数・enum・クラス・public/private・参照と継承の違い・コンストラクター・new・get/returnを、小さな例と本人の記述で確認した
* `enum`は型を定義するためのキーワードで、`Direction`が本人の作った型の名前。変数を宣言するときはその型を使う、という区別を説明した
* 変数を用意する宣言と、既存の変数に値を入れる代入を区別する練習をした。コンストラクターをクラスの外に置いていた点、受け取った値の保存先と代入方向を、本人が修正した
* 本人は代入後の値を具体例で追えるようになった一方、型・クラス・作る側と受け取る側の関係は理解を確認中。用語を一度に増やさず、既存コードを具体的な値で追って復習する
* public/privateの役割は説明したが、privateの恩恵を実際の不具合や修正場面で実感したわけではない。説明を繰り返して納得を求めず、必要な場面で実コードと結び付けて振り返る
* 次回は既存の`X`を復習し、同じ形の読み取り窓口を本人が追加するところから再開する
* 最初の目標「床へ1マス移動したときの位置・向き・ターン数をEditModeテストで確認し、Test Runnerで認識・実行する」は未達成。学習の進行に合わせて実装途中で区切った
* Codexは説明・小さなコード例・実ファイルのレビューとGit確認を担当。ゲームコードのファイルは直接編集せず、終了時にこの進捗文書だけを更新した。詳しい復習内容はChatGPT Workへの作業報告で共有する

## Day15の学習と引き継ぎ

* 当初の小さな区切りだった`Y`・`Facing`・`TurnCount`の追加とCoreのコンパイル確認は達成した。その後、初期状態を確認するEditModeテストの作成に進み、状態を作る一行まで本人が記述した
* `public`は公開範囲、`using`は別の名前空間の名前を使いやすくする書き方、継承は既存のクラスをもとにクラスを定義する仕組みで、別の役割だと説明した。継承は今回使っておらず、`using`もまだ本人のテストに追加していない
* 型は扱う値の種類や使える操作を定めるもの。`enum Direction`と`class GameState`はどちらも本人が型を定義する書き方。classはデータと処理をまとめた型を定義するもので、処理だけのまとまりという説明では不十分だった
* クラス全体の外側の波括弧は、フィールド・コンストラクター・プロパティをまとめて囲む。現在の並びは必須テンプレートではなく、内容によって役割が決まる
* `public class GameState`は型全体の定義、クラス内の同名の`public GameState(...)`は作るときに動くコンストラクターの定義。同じ名前にするのはC#の決まり。引数にturnCountがないことがコンストラクターである理由ではない
* 「初期状態」という表現を、ダンジョンに入った直後の固定状態と結び付けて理解していた。今回の意味は「そのGameStateを作った直後の状態」で、コンストラクターは受け取った開始位置と向きを保存する。ゲーム本番の開始位置を固定したわけではない
* 引数の名前を渡す側にも書く必要があると思っていた点を確認した。`new GameState(1, 1, Direction.Up)`は位置引数の順番でstartX・startY・startFacingへ値を渡す。受け取り側に書く`int startX`は値の型と受け取ったあとに使う名前を定義している
* 本人は「受け取る値の型・順番・個数が現在のコンストラクターで決まり、値そのものは指定できる」と理解を言葉にできた。`game`は作ったGameStateを扱うローカル変数で、コンストラクターに渡す値の送り元ではない
* フィールドは状態の保存先、コンストラクターの引数は値を受け取る場所、プロパティは保存した値を読む窓口。具体的な値を追う例では、startXが5を受け取り`x = startX;`を実行するとxは5になると本人が答えた
* フィールドのintは明示的な`= 0`がなくても既定値0になる。自分で書いたコンストラクターがない場合も同じで、その場合C#は引数なしのコンストラクターを自動で用意する。これはローカル変数に値を入れずに読めるという意味ではない
* `startX`と`x`は別の変数。同名にしても同じ保存場所になるわけではない。今回は受け取り用と保存用を見分けるために名前を分けた。`this.x = x`という別の書き方も説明したが、本人から説明が雑で用語が増えて混乱するとの指摘があり、以後は具体的な値一つを追う説明を優先する
* `game.X`は公開プロパティ経由で内部のxを読み取る。小文字の`game.x`はprivateフィールドへの直接アクセスで、テストからは使えない。`InitialStateIsSet`は「初期状態が設定されることを確認する」という意図を示す本人のメソッド名で、名前だけで初期化やテスト認識が起こるわけではない
* コンストラクターを使う理由は説明できても、必要な処理として自力で思いつくことにはまだ手応えがない。次回は「目的→現在のコードでできること→足りない受け渡し→使う仕組み」という順で考え、完全に理解したと断定しない
* オブジェクト・型・クラス・new・引数・フィールド・プロパティのつながりは復習途中。新しい用語を一度に増やさず、「今回作った一つのゲーム状態」と具体的な値を使って説明する。本人が試す時間を先に取り、詰まった箇所だけヒントを出す
* Riderの灰色のstartXなどは引数名の補助表示で、保存されたコードではない。表示された名前は本人が書いたコンストラクターから取得される。名前空間やprivateフィールド名、冗長な0初期化に関するスタイル警告は後で見直す対象とした
* Codexはゲーム・テストコードを直接編集していない。本人がローカル変数へのPublicの付与、行末セミコロン、括弧などを修正した。Codexによる直接編集はこの進捗文書のみ。Commit・Push・PR作成、追加のステージ操作は行っていない

## Day16の学習と引き継ぎ

* 最初の区切り「X=1・Y=1・Facing=Up・TurnCount=0を確認するEditModeテスト1件を完成させ、Unityで認識・実行する」は達成した。本人がRiderでコードを書き、Unity Test Runnerで実行した。Codexはコードを直接編集せず、技術的な節目としてこの文書を更新した
* `using NUnit.Framework;`はテスト用の名前を短く使えるようにする宣言、`Assert.That`は実際の値が条件に合うか確認する処理、`Is.EqualTo`は指定値と等しいという条件、`[Test]`はメソッドをテストとして認識させる目印と説明した
* 本人は補完を使って記述した。入力できることと意味を説明できることを分けて確認し、開始方向をDownにしてUpを期待したままなら失敗すると本人が答えた。この変更は思考例だけで、保存されたテストはUpのまま
* ファイル間の関係は復習途中。Testsが状態を作って値を読み比較し、GameStateが保存・読み取りの仕組みを提供し、Directionが方向の名前を定義する。値がGameState.csというソースファイルに書き込まれるわけではなく、実行中に作った状態に保存されると説明した
* メソッドは名前を付けて呼び出せる処理のまとまり。今回のInitialStateIsSetはNUnitから呼ばれる。voidは値を返さないという意味で、成功・失敗の値をreturnするという意味ではない。Codexの「成功・失敗を返す」という曖昧な説明は訂正し、Assertの確認が合わなければ失敗を知らせ、NUnitが実行結果を記録すると整理した
* フィールドの宣言、クラスの定義、テストメソッド名の役割も復習した。テストのコメントは「何を保証したいか」を書く。本人は理解が完全ではないと話しており、テスト成功と概念の習得を同一視しない
* 次の小さな区切りは、期待値を一つだけ一時的に変え、失敗時の期待値と実際の値の表示を確認して元へ戻す練習。本人から、成功地点のローカルコミット後にこの練習、固定マップと座標の整理、床への1マス移動の順で進める依頼があった。意図的な失敗実行はまだしていない

## 次にやること

1. 成功したInitialStateIsSetを短く振り返り、作る・渡す・保存する・読む・比較する流れを具体的な値で確認する。型やオブジェクトの説明を一度に増やさず、本人が試す時間を先に取る
2. 本人が期待値一つを一時変更して失敗表示を確認し、元へ戻して成功を再確認する。目的を先に説明し、本人が試す時間を取る。テストの期待値やゲーム仕様を恒久的に変える作業ではない
3. 行末の余分な空白は、本人が後で体裁を整える対象として残っている
4. 固定マップの表現と座標の扱いを整理して、範囲確認・床/壁判定・1マス移動を小さく実装する。未決のゲーム仕様や大きな設計判断はChatGPT Workで相談する
5. 最初の床移動のEditModeテストを本人が作成し、Test Runnerでの認識・実行を確認する。その後に壁・マップ外のテストを追加する
6. 必要な動作ができた段階でPlayModeテストの対象を検討する。READMEの追記は引き続き今後の作業

## 未解決・検討事項

* ゲーム仕様書とテスト戦略書をリポジトリのどこへ置くか
* 失敗時のテスト結果の読み方と、別の開始位置・向きの確認。初期状態(1, 1, Up)の生成・読み取りとテスト成功はDay16に確認済み
* マップのデータ表現、上下方向と座標の増減の対応。ゲーム仕様を変更せず、実装前に整理する
* CIを導入する時期と構成
* 自動操作Botの実装方法

## 関連記録

* [制作ログ（Day15）](https://docs.google.com/document/d/17PeI7E97xqSAcOX4V0YDPufTILIZ4cZ5oNNneI2Imec/edit?pli=1&tab=t.0#heading=h.gqvb2ayz7jla)
* [作業時間表](https://docs.google.com/spreadsheets/d/1He70CnsMwKe-eGSXBvuIYKwaueKuBBzZpusN-AjDOOc/edit)

## 作業を再開するとき

1. `AGENTS.md`を読む
2. この`PROJECT_STATUS.md`を読む
3. Gitの変更状況を確認する
4. Unityでエラーが出ていないことを確認する
5. 「次にやること」の先頭から再開する
