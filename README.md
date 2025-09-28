# 💳 Payment-backendApp

ASP.NET Core Web API for payment processing with **Razorpay**.  
Supports creating payment intents, retrieving transactions by Razorpay `order_xxx` ID or internal `Guid`, and saving transaction history via an in-memory repository.

---

## 🚀 Features
- Create new payment orders via Razorpay  
- Retrieve transactions by internal Guid or Razorpay order ID  
- In-memory transaction repository (can be swapped with database)  
- Clean service + repository architecture  

---

## 📦 Tech Stack
- **.NET 6 / 7** (ASP.NET Core Web API)  
- **Razorpay .NET SDK**  
- **C#**  
- In-memory repository (for development)  

---

## 📌 Endpoints

### Create Payment
**POST** `/create`

Request body:
```json
{
  "amount": 8000,
  "currency": "INR",
  "description": "TEST PAYMENT",
  "receiptEmail": "user@example.com",
  "paymentMethodId": "upi@123"
}
