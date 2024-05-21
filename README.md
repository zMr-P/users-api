## 🅰️API de Usuários🅰️

Esse Microsserviço te permite salvar seus usuários com segurança.   
Quando o usuário é cadastrado, seu Id e senha são salvos em Hash, dando mais segurança para sua aplicação.     
Caso queira, é possível testar se a autenticação está funcionando, verifique os __cookies__.

## ⚙️Tecnologias e Frameworks utilizados⚙️

- C# 
- .Net core
- Entity Framework Core
- Identity Core Extensions

# 📒Como executar o projeto📒

#### Pré-requisitos
- .Net 7.0
- Visual Studio Community || Visual Studio Code
- SQL Server

```bash
# clonar repositório:
git clone https://github.com/zMr-P/users-api.git

#Passar as migrations para seu Database
Gerênciador Nugget
{
    Update-Database
}

# Executar o projeto
ctrl-f5 || dotnet watch run 
```

# 🧑‍🔬Autor🧑‍🔬

Paulo Roberto S. Conceição

[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/zzmr-p)
