This is to showcase using two different tokens to seperate main login access token from SignalR, this is because SignalR exposes access token in logs.

# Steps
1. Go to swagger page (http://localhost:5182/swagger/index.html)[http://localhost:5182/swagger/index.html]
2. Send the /auth/login request with your desired name, this would return your main access token.
3. Now send the /auth/chat-token request with your token. You can use the swagger lock icon to set bearer, just paste the token only. You now have your SignalR access token
4. Now go to the (http://localhost:5182)[http://localhost:5182], enter your chat access token in the input field and hit the button. If everthing worked, it should show an connected alert with the name you entered in the console.
