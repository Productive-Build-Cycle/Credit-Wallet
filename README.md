💰Credit Wallet Api
Clean Architecture–inspired Credit Wallet API designed with clear separation of concerns, 
feature-based organization, and maintainable business logic.

The system allows users to manage a wallet, add credit, and retrieve wallet information 
in a safe and structured way.

-----------------------------------
🚀 Architecture Overview
The project follows a Clear Architecture Structure approach with a strong focus on:

- Separation of concerns
- Feature-based organization
  
----------------------------------
High Level Structure
- Data  – Persistence and database access
- Feature – Application use cases (handlers, endpoints, validators)
  
----------------------------------------
🗄Data Layer
- Entity definitions (Wallet, Transaction, etc.)
- ApplicationDbContext
- Database access
- Entity persistence
- No Bussiness Logic
  
- -------------------------------------
Feature layer - Application Logic
Each feature is self-contained and typically includes:
- Request DTO
- Response DTO
- Handler
- Validator
- Endpoint mapping

  Features:
  feature/get-user-wallet
  feature/add-credit-to-wallet
  Feature/deduct-from-wallet
  
--------------------------------------
🛠Technologies Used
-ASP.Net Minimal API
-Entity FrameWork
-Sql Server
-C#
-Dependency Injection
-Validation

----------------------------------
⚠️ Error Handling & Logging
- Proper null checks are implemented
- Meaningful HTTP status codes are returned
  
- ---------------------------------
Author Alireza Hosseini ,Zahra Havaei
