# API Integration Guide

## 📝 Overview

The frontend has been fully integrated with the ASP.NET Core API. All pages now communicate with the backend to:
- Load products dynamically
- Handle user registration
- Handle user login
- Add items to cart

---

## 🚀 Quick Start

### 1. **Start the API Server**

Open PowerShell and run:

```powershell
cd c:\wamp64\www\GitNguonMo\api
dotnet run --urls "http://localhost:5000"
```

The API will be available at: **http://localhost:5000**
Swagger documentation at: **http://localhost:5000/swagger**

### 2. **Open Frontend in Browser**

Use Live Server or any local server to open the HTML files:
```
http://localhost:8000 (or your configured port)
```

---

## 📦 API Endpoints

### Products

**GET /api/products**
- Returns list of all products
- Response: `[{ id, name, description, price, imageUrl, stock }, ...]`

**GET /api/products/{id}**
- Returns product details by ID
- Response: `{ id, name, description, price, imageUrl, stock }`

### Authentication

**POST /api/auth/register**
- Request body: `{ email, password, name }`
- Response: `{ token, user: { id, email, name }, message }`

**POST /api/auth/login**
- Request body: `{ email, password }`
- Response: `{ token, user: { id, email }, message }`

### Cart

**POST /api/cart/add**
- Request body: `{ productId, quantity }`
- Response: `{ message, cartItem }`

---

## 🔧 Frontend Integration Details

### 1. **API Client** (`js/api-client.js`)

Centralized client for all API calls:

```javascript
// Get all products
const products = await APIClient.getProducts();

// Get product by ID
const product = await APIClient.getProductById(1);

// Register user
await APIClient.register(email, password, name);

// Login user
await APIClient.login(email, password);

// Add to cart
await APIClient.addToCart(productId, quantity);

// Check login status
const isLoggedIn = APIClient.isLoggedIn();
const user = APIClient.getCurrentUser();
```

### 2. **Products Page** (`cat.html`)

- **Feature**: Dynamically loads products from API
- **Script**: Added JavaScript at the end to fetch and render products
- **Loading state**: Shows spinner while fetching
- **Add to cart**: Calls `addToCart()` function which hits `/api/cart/add` endpoint

```javascript
// Loads when page opens
async function loadProducts() {
  const products = await APIClient.getProducts();
  // Renders product cards dynamically
}

// Click add to cart button
async function addToCart(productId) {
  await APIClient.addToCart(productId, 1);
  alert('Đã thêm vào giỏ hàng!');
}
```

### 3. **Registration Page** (`register.html`)

- **Feature**: Form submission to API
- **Validation**: 
  - Password confirmation check
  - Required fields
- **Success**: Stores token + user info in localStorage, redirects to home
- **Error**: Displays error message

```html
<!-- Form ID: registerForm -->
<!-- Submits to: POST /api/auth/register -->
<!-- Stores in localStorage: token, user -->
```

### 4. **Login Page** (`login.html`)

- **Feature**: Form submission to API
- **Features**:
  - Password visibility toggle
  - Remember me checkbox
  - Error handling
- **Success**: Stores token + user info in localStorage, redirects to home
- **Error**: Displays error message

```html
<!-- Button ID: handleLogin() function -->
<!-- Submits to: POST /api/auth/login -->
<!-- Stores in localStorage: token, user -->
```

---

## 💾 Local Storage Usage

User authentication data is stored in browser's localStorage:

```javascript
// After successful login/registration:
localStorage.setItem('token', 'demo-token-123');
localStorage.setItem('user', JSON.stringify({ id: 1, email: 'user@example.com' }));

// Retrieve user:
const user = APIClient.getCurrentUser();
// Returns: { id: 1, email: 'user@example.com' }

// Check if logged in:
const isLoggedIn = APIClient.isLoggedIn();
```

---

## 🔄 Data Flow

### Product Loading
```
cat.html loads
  ↓
DOMContentLoaded event
  ↓
loadProducts() function executes
  ↓
APIClient.getProducts() calls GET /api/products
  ↓
API returns product array
  ↓
JavaScript renders product cards
  ↓
User sees product list
```

### User Registration
```
User fills form on register.html
  ↓
Clicks "Đăng ký" button
  ↓
registerForm submit event
  ↓
APIClient.register(email, password, name)
  ↓
POST /api/auth/register with credentials
  ↓
API returns token + user info
  ↓
Store in localStorage
  ↓
Redirect to index.html
```

### User Login
```
User fills form on login.html
  ↓
Clicks "Đăng nhập" button
  ↓
handleLogin() function executes
  ↓
APIClient.login(email, password)
  ↓
POST /api/auth/login with credentials
  ↓
API returns token + user info
  ↓
Store in localStorage
  ↓
Redirect to index.html
```

### Add to Cart
```
User clicks "Thêm vào giỏ" on product
  ↓
addToCart(productId) function executes
  ↓
APIClient.addToCart(productId, 1)
  ↓
POST /api/cart/add with productId
  ↓
API processes and responds
  ↓
Show success alert to user
```

---

## ✅ Testing Checklist

- [ ] API is running on http://localhost:5000
- [ ] `js/api-client.js` is loaded in all HTML pages
- [ ] cat.html shows products (loaded from API)
- [ ] Can register new user (email + password)
- [ ] Can login with registered credentials
- [ ] Can add item to cart
- [ ] Login/logout updates localStorage
- [ ] Page redirects work correctly

---

## 🐛 Troubleshooting

### Products not loading
- Check if API is running: `http://localhost:5000/swagger`
- Check browser console (F12) for errors
- Verify CORS is enabled (it is by default)

### Login/Register not working
- Check if API is running
- Verify email format is correct
- Check browser localStorage (F12 → Application → localStorage)
- Check console for network errors

### Add to cart not working
- Check API response in Network tab (F12)
- Verify API endpoint: `POST http://localhost:5000/api/cart/add`
- Check if payload is correct: `{ productId: 1, quantity: 1 }`

---

## 📄 Files Modified

- ✅ `js/api-client.js` - New API client library
- ✅ `cat.html` - Added product loading script
- ✅ `register.html` - Added form submission to API
- ✅ `login.html` - Added login form to API
- ✅ All pages - Added `<script src="../js/api-client.js"></script>`

---

## 🔐 Security Notes

**Current (Demo) Implementation:**
- Tokens stored in localStorage (visible to JavaScript)
- No JWT validation on frontend
- Passwords hashed with SHA256 on backend
- CORS enabled for local development

**For Production:**
- Use httpOnly cookies instead of localStorage
- Implement proper JWT validation
- Use HTTPS only
- Add request validation
- Implement rate limiting
- Add proper error handling

---

## 📞 API Response Examples

### Products Response
```json
[
  {
    "id": 1,
    "name": "Cà chua Đà Lạt",
    "description": "Cà chua tươi ngon từ Đà Lạt",
    "price": 68000,
    "imageUrl": "https://...",
    "stock": 100
  },
  ...
]
```

### Registration Response
```json
{
  "token": "demo-token-123",
  "user": {
    "id": 1,
    "email": "user@example.com",
    "name": "Nguyễn Văn A"
  },
  "message": "Đăng ký thành công"
}
```

### Login Response
```json
{
  "token": "demo-token-456",
  "user": {
    "id": 1,
    "email": "user@example.com"
  },
  "message": "Đăng nhập thành công"
}
```

### Add to Cart Response
```json
{
  "message": "Đã thêm vào giỏ hàng",
  "cartItem": {
    "productId": 1,
    "quantity": 1,
    "addedAt": "2025-11-26T10:30:00Z"
  }
}
```

---

**Last Updated**: November 26, 2025
**Status**: ✅ Frontend fully integrated with API
