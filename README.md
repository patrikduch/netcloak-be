# 🔐 netcloak

[![License: Proprietary](https://img.shields.io/badge/License-Proprietary-red.svg)](LICENSE.md)
[![Usage: Forbidden](https://img.shields.io/badge/Usage-Forbidden-black.svg)]()

Clean Architecture .NET 8 API with Keycloak SSO integration. Foundation for multi-tenant SaaS authentication.

## ✨ Features

- 🔑 Keycloak OIDC integration
- 🏗️ Clean Architecture
- 🐳 Docker Compose ready
- 📝 Swagger with JWT auth
- 🔄 Token refresh flow

## 🛠️ Tech Stack

- .NET 8 (Minimal API + Controllers)
- Keycloak 26.0
- PostgreSQL 16
- Docker

## 🚀 Getting Started

### Prerequisites

- Docker & Docker Compose
- .NET 8 SDK

### Run
```bash
docker-compose up -d
```

- **API:** https://localhost:8082/swagger
- **Keycloak:** http://localhost:8180 (admin/admin)

## 📁 Project Structure
```
NetCloak/
├── NetCloak.API/              # Controllers, endpoints
├── NetCloak.Application/      # DTOs, interfaces
├── NetCloak.Domain/           # Entities
├── NetCloak.Infrastructure/   # Keycloak services
└── NetCloak.Persistence/      # EF Core, DbContext
```

## 🔒 License

This project is proprietary software. See [LICENSE](LICENSE.md) for full terms.

**© Patrik Duch s.r.o., IČO: 24091090**

---

> ⛔ **WARNING:** This software is for viewing and evaluation ONLY. Any use, copying, modification, or distribution is strictly forbidden. Look, don't touch! 👀🚫✋