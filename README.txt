# 🚀 Rodando o projeto com Docker

O projeto já vem com um `docker-compose.yml` na raiz que sobe **todo o ambiente completo**: Traefik, SQL Server, migrador de banco, API, WebNext e MailHog.

---

## 📋 Pré-requisitos

1. **Docker Desktop** instalado e em execução (Windows com WSL2 habilitado).
2. Configurar o arquivo `hosts` para resolver os domínios locais usados pelo Traefik.

### 🧩 Configuração do hosts (obrigatório)

Abra como administrador:

```
C:\Windows\System32\drivers\etc\hosts
```

Adicione as linhas:

```
127.0.0.1 localtest.me
127.0.0.1 api.localtest.me
```

---

## ⚙️ Variáveis de ambiente (opcional)

Copie o arquivo `.env.example`:

```powershell
copy .env.example .env
```

A senha padrão do SQL Server é:

```
Mudar@123
```

---

## 📁 Estrutura do projeto

```
ra10184441-25
├───Api
└───WebNext
```

---

## ▶️ Subindo o projeto

Na raiz do projeto, abra o PowerShell e rode:

```powershell
docker compose up --build
```

Para rodar em background:

```powershell
docker compose up --build -d
```

---

## 🔄 Ordem de inicialização

O Docker Compose gerencia automaticamente a ordem dos serviços:

1. SQL Server (com healthcheck)
2. Migrator (aplica as migrations no banco)
3. API + MailHog
4. WebNext
5. Traefik (proxy reverso na porta 80)

---

## 🌐 URLs do sistema

| Serviço                     | URL                          |
|------------------------------|-------------------------------|
| Frontend principal            | http://localtest.me          |
| Tenant (exemplo)               | http://{slug}.localtest.me   |
| API                             | http://api.localtest.me     |
| MailHog (e-mails de teste)     | http://localhost:8025        |
| SQL Server                     | localhost:1433                |

---

## 🧰 Comandos úteis

```powershell
# Ver containers
docker compose ps

# Ver logs da API
docker compose logs -f api

# Ver logs do frontend
docker compose logs -f webnext

# Parar tudo
docker compose down

# Reset completo (limpa banco)
docker compose down -v
docker compose up --build
```

---

## ⚠️ Problemas comuns

- **Porta 80 em uso**: IIS, Skype ou outros serviços podem bloquear. Libere ou altere no compose.
- **localtest.me não funciona**: verifique o hosts e se o Docker está rodando.
- **Build lento na primeira execução**: normal (download de imagens e restore de dependências).
- **Tenant não abre**: adicione o subdomínio manualmente no hosts.

---

## 💡 Alternativa sem Docker

- API: `dotnet run`
- WebNext: `npm run dev`
- Banco: SQL Server local

---

👉 O Docker é o modo recomendado, pois simula o ambiente completo de produção (Traefik + multi-tenant + migrations automáticas + MailHog).
