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
