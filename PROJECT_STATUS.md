# game-set-automation-lab 現在の状況

最終更新：2026-09-26（Day 11）

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
* Unity Test Framework
* Git / GitHub
* mainブランチ

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

## 現在の状態

* UnityのSampleSceneを開ける
* Unityプロジェクト自体のエラーは確認されていない
* ゲーム固有のC#コードはまだ実装していない
* Unityの初期アセットと初期設定のみ存在する
* `AGENTS.md`と`PROJECT_STATUS.md`は未コミット
* Riderからのコミット・Pushはまだ実践していない

以前表示された`Token Exchange`エラーは、Unityのオンライン認証通信による一時的なもので、ゲームやC#コードのエラーではない。Clear後に再発していない。

## 次にやること

1. `AGENTS.md`と`PROJECT_STATUS.md`の内容を確認する
2. Riderから2つのファイルをコミットする
3. RiderからGitHubへPushする
4. `Assets`配下のフォルダ構成を決める
5. Unity Test Frameworkを使用できる状態にする
6. 最初に実装する小さなゲームロジックを決める
7. 最初のC#コードを開発者本人が実装する
8. 実装したロジックのEditModeテストを作成する
9. READMEへプロジェクトの目的と開発環境を追記する

## 未解決・検討事項

* 最初に実装するゲームロジック
* `Assets`配下の具体的なフォルダ構成
* ゲーム仕様書とテスト戦略書をリポジトリのどこへ置くか
* Unity Test Frameworkの初期設定
* CIを導入する時期と構成
* 自動操作Botの実装方法

## 作業を再開するとき

1. `AGENTS.md`を読む
2. この`PROJECT_STATUS.md`を読む
3. Gitの変更状況を確認する
4. Unityでエラーが出ていないことを確認する
5. 「次にやること」の先頭から再開する
