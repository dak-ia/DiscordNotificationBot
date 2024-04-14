# Simple Discord Notification Bot

## 使い方

### Bot の準備

https://discord.com/developers/applications の`Applications`->`New Application`で Bot を作成し、TOKEN を取得してください。  
Bot の名前や画像などはお好みで設定してください。  
`Installation`->`Authorization Methods`->`SELECT METHODS`は`User Install`・`Guild Install`のどちらを選択してもいいですが、`Guild Install`のみのほうが他人にインストールされることを防げます。
`Bot`->`Build-A-Bot`の TOKEN をコピーして控えてください。  
表示されていない場合は`Reset Token`を押して再生成してください（再生成すると古い Token は無効化されます）。  
`Bot`->`Privileged Gateway Intents`の`PRESENCE INTENT`、`SERVER MEMBERS INTENT`、`MESSAGE CONTENT INTENT`の 3 つを有効にしてください。

### Bot の招待 URL の生成

`OAuth2`->`OAuth2 URL Generator`->`SCOPES`は`bot`を選択してください。  
そして`OAuth2`->`OAuth2 URL Generator`->`BOT PERMISSIONS`は`TEXT PERMISSIONS`の`Send Messages`を選択してください。  
`OAuth2`->`OAuth2 URL Generator`->`GENERATED URL`に生成された URL をコピーしてブラウザで開いて Bot をサーバーに招待してください。

### Bot の起動

DiscordNotificationBot.exe もしくはそのショートカットから起動してください。

### Bot の設定と使い方

各項目に必要事項を入力してください。  
`Discord Token`には先ほど控えた TOKEN を入力してください。  
`Channel ID`には、Bot がサーバーの`TEXT CHANNELS`の任意の ID を入力します。  
ID の簡単な入手は、チャンネルのリンクをコピーしてたときの`https://discord.com/channels/xxxxxxxxxxxxxxxxxx/yyyyyyyyyyyyyyyyyy`の`yyyyyyyyyyyyyyyyyy`の部分からいけます。  
右にチャンネル名が表示されます。  
`Message to Send`には任意の送信したいメッセージを入力してください。  
`Interval Time`には任意の送信間隔を入力してください。ボタンクリックで開始、もう一度クリックで停止します。

### 仕様

Bot を起動してコマンドを入力すると返答します。  
Bot のアクセス権限があるサーバー内のどのチャンネルからでもコマンドを入力できます。  
Token 初回入力時か、変更した場合はソフトを再起動してください。

- `/help`
  - もしくは Bot をメンションして`help`
  - ヘルプを表示します。
- `/ip`
  - もしくは Bot をメンションして`ip`
  - 実行環境のグローバル IPv4 アドレスを表示します。
- `/local_ip num`
  - もしくは Bot をメンションして`local_ip num`
  - 実行環境のローカル IPv4 アドレスを表示します。
  - `num`の部分は任意の数字を指定でき、複数ある IP アドレスの中から 1 つを指定することができます。
- `/server_time`
  - もしくは Bot をメンションして`server_time`
  - 実行環境の日時を表示します。
- `/system_info`
  - もしくは Bot をメンションして`system_info`
  - 実行環境のシステム情報を表示します。
  - 現状表示さえれる項目は以下です。
    - OS バージョン
    - プロセッサー数
    - 実行環境を起動してから経った時間

`Message to Send`に入力したメッセージの内、以下は変換されます（エスケープは用意していません）。

- `${Global_IPv4}`
  - 実行環境のグローバル IPv4 アドレスを表示します。
- `${Local_IPv4_num}`
  - 実行環境のローカル IPv4 アドレスを表示します。
  - `num`の部分は任意の数字を指定でき、複数ある IP アドレスの中から 1 つを指定することができます。
- `${Server_Time}`
  - 実行環境の日時を表示します。

`Interval Time`には 0 以下にすると 1 回だけ送信します。

## Usage

### Bot Setup

Create a bot on https://discord.com/developers/applications by going to `Applications` -> `New Application` and obtain the TOKEN.  
You can customize the bot's name and image as desired.  
You can choose either `User Install` or `Guild Install` under `Installation` -> `Authorization Methods` -> `SELECT METHODS`, but selecting `Guild Install` only will prevent others from installing it.
Copy and save the TOKEN from `Bot` -> `Build-A-Bot`.  
If it is not displayed, press `Reset Token` to regenerate it (regenerating will invalidate the old token).  
Enable the 3 options under `Bot` -> `Privileged Gateway Intents`: `PRESENCE INTENT`, `SERVER MEMBERS INTENT`, and `MESSAGE CONTENT INTENT`.

### Generating Bot Invitation URL

Select `bot` under `OAuth2` -> `OAuth2 URL Generator` -> `SCOPES`.  
Select `Send Messages` under `TEXT PERMISSIONS` in `OAuth2` -> `OAuth2 URL Generator` -> `BOT PERMISSIONS`.  
Copy the generated URL from `OAuth2` -> `OAuth2 URL Generator` -> `GENERATED URL` and open it in a browser to invite the bot to your server.

### Bot Startup

Start DiscordNotificationBot.exe or its shortcut.

### Bot Configuration and Usage

Enter the required information for each item.  
Enter the TOKEN you saved earlier in `Discord Token`.  
Enter any ID of the server's `TEXT CHANNELS` in `Channel ID`.  
To easily obtain the ID, copy the link of the channel and look for the `yyyyyyyyyyyyyyyyyy` part in `https://discord.com/channels/xxxxxxxxxxxxxxxxxx/yyyyyyyyyyyyyyyyyy`.  
The channel name will be displayed on the right.  
Enter any message you want to send in `Message to Send`.  
Enter any interval time for sending messages in `Interval Time`. Click the button to start and click again to stop.

### Specifications

When you enter a command, the bot will respond.  
You can enter commands from any channel within the server where the bot has access.  
If you enter or change the Token, please restart the software.

- `/help`
  - Or mention the bot and type `help`
  - Displays the help message.
- `/ip`
  - Or mention the bot and type `ip`
  - Displays the global IPv4 address of the execution environment.
- `/local_ip num`
  - Or mention the bot and type `local_ip num`
  - Displays the local IPv4 address of the execution environment.
  - You can specify any number for `num` to select one from multiple IP addresses.
- `/server_time`
  - Or mention the bot and type `server_time`
  - Displays the date and time of the execution environment.
- `/system_info`
  - Or mention the bot and type `system_info`
  - Displays the system information of the execution environment.
  - The currently displayed items are as follows:
    - OS version
    - Number of processors
    - Time elapsed since the execution environment was started

The following will be replaced in the message you entered in `Message to Send` (no escape characters are provided).

- `${Global_IPv4}`
  - Displays the global IPv4 address of the execution environment.
- `${Local_IPv4_num}`
  - Displays the local IPv4 address of the execution environment.
  - You can specify any number for `num` to select one from multiple IP addresses.
- `${Server_Time}`
  - Displays the date and time of the execution environment.

If `Interval Time` is set to 0 or less, the message will be sent only once.
