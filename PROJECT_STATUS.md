# game-set-automation-lab 現在の状況

最終更新：2026-09-29（Day13）

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
* Unity Test Framework 1.6.0（`Packages/manifest.json`に登録済み。Test Runnerは開けるが、テストの認識・実行は未確認）
* Git / GitHub
* Day13にPR #1を`main`へマージし、Riderでローカルの`main`を更新済み

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
* Riderでローカルの`main`を更新
* 最初のGame Coreロジックを、固定マップでの上下左右1マス移動と壁判定に決定。ゲーム仕様書とテスト戦略書へルールを反映
* Day13の制作ログを作成し、作業時間表を更新

## 現在の状態

* UnityのSampleSceneを開ける
* Day12に両テスト用asmdefのApply後、Unity Editorに赤いエラーは出ていない（開発者本人の確認）
* PR #1の構成変更は`main`へ取り込み済み。ローカルの`main`も更新済み（Day13）
* ゲーム固有のC#コードはまだ実装していない
* Unityの初期アセットに加え、`Assets/_Project`に`Core`、`Presentation`、`Automation`、`Tests/EditMode`、`Tests/PlayMode`の構成がある
* asmdefは`Vivienne.GameCore`、`Vivienne.Unity`、`Vivienne.Automation`、`Vivienne.EditModeTests`、`Vivienne.PlayModeTests`の5つ。対応する`.meta`と参照先のGUIDを確認済み
* `Vivienne.GameCore`はUnityエンジンへの参照を無効化。`Vivienne.Unity`と`Vivienne.Automation`は`Vivienne.GameCore`を参照
* 両テスト用asmdefは`Vivienne.GameCore`、Unity Test Runner、NUnitを参照し、`UNITY_INCLUDE_TESTS`を設定。EditModeはEditor専用、PlayModeは全プラットフォーム対象
* テストスクリプトはまだない。Test Runnerを開けることと、テストが認識・実行できることは別であり、後者は未確認
* 最初の移動ルール：入力方向を向き、歩行可能な空きマスなら1マス進んで1ターン消費する。壁・マップ外・敵のいるマスでは向きだけ変わり、位置とターン数は変わらない。敵への移動入力は自動攻撃にならない
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

## 次にやること

1. 更新済みの`main`から実装用ブランチを作る
2. `AGENTS.md`にコミット・PR文案の日本語／英語の順序と、Codex Botのレビューを日本語にする指示を追加する
3. 開発者本人が固定マップの1マス移動を`Core`に実装し、対応するEditModeテストを`Tests/EditMode`に作成する
4. Test Runnerでテストの認識・実行を確認し、結果を記録する
5. 必要な動作ができた段階でPlayModeテストの対象を検討する
6. READMEへプロジェクトの目的と開発環境を追記する

## 未解決・検討事項

* ゲーム仕様書とテスト戦略書をリポジトリのどこへ置くか
* テストスクリプト作成後のTest Runnerでの認識・実行結果
* CIを導入する時期と構成
* 自動操作Botの実装方法

## 関連記録

* [制作ログ（Day13）](https://docs.google.com/document/d/1ksAZwAt-A6k_rbg475j3aQe5Dt1U60L1cWRbszWz9GE/edit)
* [作業時間表](https://docs.google.com/spreadsheets/d/1He70CnsMwKe-eGSXBvuIYKwaueKuBBzZpusN-AjDOOc/edit)

## 作業を再開するとき

1. `AGENTS.md`を読む
2. この`PROJECT_STATUS.md`を読む
3. Gitの変更状況を確認する
4. Unityでエラーが出ていないことを確認する
5. 「次にやること」の先頭から再開する
