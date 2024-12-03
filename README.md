# TuneRate-2024-08-22

<p>baixe os arquivos zipados, e clique em extrair aqui, vc terá tudo necessário para a utilização do projeto.</p>

<p>obs.: os arquivos TuneRate que são 002 ou adiante n precisa ser extraido, apenas o 001 (a ferramenta ja detecta os demais arquivos e faz a extração corretamente)
<br>
outro ponto é, o TuneRate é arquivo dos códigos, bancoTuneRate é o arquivo para restaurar backup no banco de dados.</p>
<br>
<br>
<p>
BACKUP DATABASE TuneRate
TO DISK = 'C:\bancoTuneRate\TuneRate.bak' 
WITH FORMAT; <br>
esse comando sql faz o backup do banco de dados no caminh que vc escolher! (lembre de alterar para o caminho que deseja fazer o backup)</p>




## Testando a Conexão SMTP via Telnet no CMD

### 1. Habilitar Telnet (se não estiver habilitado)

No Windows, o **Telnet** pode não estar habilitado por padrão. Para ativá-lo, siga os passos abaixo:

- Abra o **Painel de Controle**.
- Vá para **Programas** > **Ativar ou desativar recursos do Windows**.
- Marque a opção **Cliente Telnet** e clique em OK.

### 2. Comando Telnet para Testar SMTP

- Abra o **Prompt de Comando** (CMD).
- Digite o seguinte comando para testar a conexão SMTP com o servidor do Gmail:

    ```bash
    telnet smtp.gmail.com 587
    ```

    **587** é a porta usada para **STARTTLS** (uma forma de criptografar a comunicação). Você também pode tentar a porta **465**, que é usada para SSL direto.

### 3. Interação com o Servidor SMTP

Após conectar, o servidor SMTP deverá responder com algo como:

    ```sql
    220 smtp.gmail.com ESMTP Exim 4.94.2 Wed, 15 Dec 2021 09:00:00 +0000
    ```

Se você ver essa mensagem, significa que a conexão com o servidor foi bem-sucedida.

### 4. Comando HELO (ou EHLO)

Agora, envie o comando **HELO** (ou **EHLO**) para iniciar a comunicação:

    ```bash
    HELO yourdomain.com
    ```

O servidor deve responder com algo como:

    ```css
    250 smtp.gmail.com Hello [ip address]
    ```

### 5. Autenticação (AUTH LOGIN)

Para autenticar no servidor, você precisa usar o comando **AUTH LOGIN**, que envia as suas credenciais (nome de usuário e senha) codificados em base64.

- Digite:

    ```bash
    AUTH LOGIN
    ```

O servidor vai pedir o seu nome de usuário e senha codificados. Para isso, você precisará codificar seu **e-mail** e a **senha do aplicativo** em base64.

- Seu e-mail codificado seria: 

    ```
    dHVuZXJhdGUuc2VuaGFpQGdtYWlsLmNvbQ==
    ```

- A senha codificada (senha do aplicativo) seria algo como:

    ```
    cHVlaSBieGNjIGxkWGQgb3h6bQ==
    ```

