# Overview

This project demonstrates the use of **two separate access tokens** to isolate the primary authentication token from the SignalR connection token. This approach is implemented because SignalR may expose access tokens in logs, and separating concerns reduces the risk of unintentionally leaking the main authentication token.

Specifically:

- A **primary access token** is used for standard API authentication.
- A **dedicated SignalR access token** is used exclusively for establishing SignalR connections.

---

# Usage Instructions

## 1. Test Page (`/`)

The root page (`http://localhost:5182/`) is a **simple test page** that allows you to enter a SignalR access token manually and connect to the hub.  

### Steps

1. Navigate to the root page:  
   `http://localhost:5182/`

2. Enter your **SignalR access token** into the input field.

3. Click **Connect**.  

4. If successful:
   - A notification message will appear on the page.
   - Any messages sent from the hub will be displayed in the notification area.

> This page is mainly for quick testing of SignalR connectivity using a pre-obtained token.

---

## 2. Chatroom Page (`/chat`)

The `/chat` page (`http://localhost:5182/chat`) is a **fully interactive chatroom**. This page automates the token workflow and allows multiple users to send and receive messages in real-time.

### Steps

1. Navigate to the chat page:  
   `http://localhost:5182/chat`

2. Enter your **username** into the input field.

3. Click **Connect**.  

   The frontend will automatically:

   1. Send a POST request to `/auth/login?name=<username>` to obtain the **primary access token**.
   2. Use the primary token to request a **dedicated SignalR chat token** from `/auth/chat-token`.
   3. Connect to the SignalR hub using the chat token.

4. Upon successful connection:
   - Both **primary** and **chat tokens** are displayed on the page.
   - A message input box and **Send** button appear.
   - All chat messages (from yourself and other users) are displayed in the notification area in real-time.

5. To log out:
   - Click the **Logout** button.
   - This stops the SignalR connection, clears tokens, and returns the UI to the initial state.

> The chatroom page demonstrates the full token isolation workflow and real-time messaging in a secure way, without exposing the primary authentication token to SignalR.

---

## 3. Swagger Token Workflow (Optional / Advanced)

For developers using Swagger or programmatic access:

1. Send a request to `/auth/login` with your username to get the **primary token**.
2. Send a request to `/auth/chat-token` using the primary token in the Authorization header.
3. Use the returned **SignalR access token** to connect manually (as in the test page).

---

This structure ensures **clear separation** between general API authentication and SignalR-specific access, minimizing the risk of token exposure while providing a fully functional chatroom experience.
