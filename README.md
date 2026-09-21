# App Tempo Agora

Aplicativo .NET MAUI de previsão do tempo que consome a API **OpenWeather**.

## Conteúdo deste repositório

### 1. Serviço de dados
**Arquivo:** `Services/DataService.cs`

| Item | Descrição |
|------|-----------|
| `GetPrevisao(cidade)` | Monta a URL da API OpenWeather e faz a requisição HTTP |

Dados retornados e montados no objeto `Tempo`:

| Campo | Descrição |
|-------|-----------|
| `lat` / `lon` | Latitude e longitude da cidade |
| `sunrise` / `sunset` | Nascer e pôr do sol (convertidos para o horário local) |
| `temp_min` / `temp_max` | Temperatura mínima e máxima (°C) |
| `description` / `main` | Descrição e condição do tempo |
| `speed` | Velocidade do vento (m/s) |
| `visibility` | Visibilidade (metros) |

### 2. Tratamento de erros
Ainda em `GetPrevisao`:

- **404 (NotFound):** lança `Exception("Cidade não encontrada. Verifique o nome digitado.")`
- **Outros status HTTP:** lança `Exception($"Erro na requisição: {resp.StatusCode}")`

### 3. Tela principal
**Arquivo:** `MainPage.xaml.cs`

O evento `Button_Clicked`:

1. Verifica a conexão com a internet usando `Connectivity.Current.NetworkAccess` (se estiver offline, mostra o alerta "Sem conexão" e interrompe a busca).
2. Valida se o campo da cidade foi preenchido (caso vazio, exibe "Preencha a cidade.").
3. Chama `DataService.GetPrevisao` e exibe o resultado em `lbl_res` com latitude, longitude, nascer/pôr do sol, temperaturas, descrição, vento e visibilidade.
4. Captura qualquer exceção no `catch` e mostra a mensagem em um `DisplayAlert` (é aí que aparece o aviso de "Cidade não encontrada").
