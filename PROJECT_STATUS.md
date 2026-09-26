# game-set-automation-lab 現在の状況

最終更新：2026-09-26（Day11）

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
* Unity Test Framework（導入・動作状態は次回確認）
* Git / GitHub
* mainブランチ

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

## 現在の状態

* UnityのSampleSceneを開ける
* Unityプロジェクト自体のエラーは確認されていない
* ゲーム固有のC#コードはまだ実装していない
* Unityの初期アセットと初期設定のみ存在する
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

1. `chore/project-structure`ブランチを作成する
2. `Assets`フォルダを最低限の構成に整理する
3. Unity Test Frameworkが使用できる状態か確認する
4. 最初に実装する小さなゲームロジックを決める
5. 最初のC#コードを開発者本人が実装する
6. 実装したロジックのEditModeテストを作成する
7. READMEへプロジェクトの目的と開発環境を追記する

## 未解決・検討事項

* 最初に実装するゲームロジック
* `Assets`配下の具体的なフォルダ構成
* ゲーム仕様書とテスト戦略書をリポジトリのどこへ置くか
* Unity Test Frameworkの初期設定
* CIを導入する時期と構成
* 自動操作Botの実装方法

## 関連記録

* [制作ログ（Day1〜Day11）](https://docs.google.com/document/d/1X6zc0omQqdFbmZOWgythtnpK-Jga4jC-ZSHYnuEgRoA/edit)
* [作業時間表](https://docs.google.com/spreadsheets/d/1He70CnsMwKe-eGSXBvuIYKwaueKuBBzZpusN-AjDOOc/edit)

## 作業を再開するとき

1. `AGENTS.md`を読む
2. この`PROJECT_STATUS.md`を読む
3. Gitの変更状況を確認する
4. Unityでエラーが出ていないことを確認する
5. 「次にやること」の先頭から再開する
