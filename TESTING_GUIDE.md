# 🧪 Frontend-API Integration Test Guide

## ✅ Step-by-Step Testing

### Step 1: Start the API Server

**Open PowerShell** and run:

```powershell
cd c:\wamp64\www\GitNguonMo\api
dotnet run --urls "http://localhost:5000"
```

**Expected output:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
```

**Verify API is running:**
- Open browser: http://localhost:5000/swagger
- You should see Swagger UI with all endpoints listed

---

### Step 2: Open Frontend in Browser

**Method 1: Using Live Server (Recommended)**
- Right-click on `index.html` in VS Code
- Select "Open with Live Server"
- Browser opens at `http://localhost:5500`

**Method 2: Using Another Local Server**
- Any local server works: `python -m http.server 8000`, etc.
- Just make sure CORS settings match

---

### Step 3: Test Product Loading

1. **Navigate to "Sản phẩm" (Products Page)**
   - Click menu "Sản phẩm" or go to `cat.html`
   
2. **Wait for products to load**
   - You should see a spinner initially
   - Then product cards appear with:
     - Product name (from API)
     - Price (from API)
     - "Thêm vào giỏ" button
   
3. **Verify products**
   - Should show API data (Cà chua, Dưa leo, etc.)
   - If empty or error appears, check:
     - API is running
     - Browser console (F12) for errors
     - Network tab shows `/api/products` request

---

### Step 4: Test User Registration

1. **Navigate to Register Page**
   - Click "Đăng ký" button from any page
   
2. **Fill registration form**
   - Họ và tên: `Nguyễn Văn A`
   - Số điện thoại: `0901234567`
   - Địa chỉ: `Hà Nội`
   - Email: `test@example.com`
   - Mật khẩu: `Password123`
   - Xác nhận mật khẩu: `Password123`
   - Check terms checkbox
   
3. **Click "Đăng ký"**
   - Should see success message (green box)
   - Page redirects to home after 2 seconds
   
4. **Verify registration**
   - Open F12 → Application → localStorage
   - Should see `token` and `user` saved

---

### Step 5: Test User Login

1. **Navigate to Login Page**
   - Click "Đăng nhập" button from any page
   
2. **Fill login form**
   - Email: `test@example.com` (from registration)
   - Mật khẩu: `Password123`
   - Keep "Ghi nhớ đăng nhập" checked
   
3. **Click "Đăng nhập"**
   - Should see success message (green box)
   - Page redirects to home after 2 seconds
   
4. **Verify login**
   - Open F12 → Application → localStorage
   - Should see `token` and `user` saved

---

### Step 6: Test Add to Cart

1. **Go to Products page** (cat.html)
   - Products should already be loaded
   
2. **Click "Thêm vào giỏ" button**
   - Should see alert: "Đã thêm vào giỏ hàng!"
   
3. **Check Network tab (F12)**
   - Should see `POST /api/cart/add` request
   - Response: `200 OK` with cart item data

---

## 🔍 Browser Console Testing

### Open Developer Tools
- **Windows/Linux**: Press `F12`
- **Mac**: Press `Cmd + Option + I`

### Check Console for Errors
- Go to "Console" tab
- Look for red error messages
- Common errors:
  - "Failed to fetch" → API not running
  - "CORS error" → Check API CORS settings
  - "404" → Endpoint doesn't exist

### Test API Call Manually
```javascript
// In browser console, test API calls directly:

// Test 1: Get all products
await APIClient.getProducts();

// Test 2: Get single product
await APIClient.getProductById(1);

// Test 3: Register user
await APIClient.register('test@example.com', 'password123', 'Test User');

// Test 4: Login user
await APIClient.login('test@example.com', 'password123');

// Test 5: Add to cart
await APIClient.addToCart(1, 2);
```

---

## 📊 Expected Test Results

| Test | Expected Result | Pass/Fail |
|------|-----------------|-----------|
| API Server Starts | Swagger UI loads at http://localhost:5000/swagger | ✅ |
| Products Load | Product list shows with data from API | ✅ |
| Registration | User created, redirected to home, token saved | ✅ |
| Login | User logged in, redirected to home, token saved | ✅ |
| Add to Cart | Success alert shown, API call made | ✅ |
| localStorage | Token and user info stored after login | ✅ |

---

## 🐛 Troubleshooting

### API Not Running
**Error**: "Failed to fetch" or "Cannot connect to server"
**Solution**:
```powershell
# Check if already running on port 5000
netstat -ano | findstr :5000

# Kill existing process if needed
taskkill /PID <PID> /F

# Restart API
cd api
dotnet run --urls "http://localhost:5000"
```

### CORS Error
**Error**: "Access to XMLHttpRequest at 'http://localhost:5000/...' from origin has been blocked by CORS policy"
**Solution**:
- CORS is already enabled in API (Program.cs)
- Check if API is running on correct URL
- Verify frontend is on allowed origin (localhost:8000, 5500, 3000)

### Products Not Loading
**Error**: Products page shows empty or error message
**Solution**:
1. Open F12 → Network tab
2. Check if request to `/api/products` was made
3. If no request, check if script loaded:
   - F12 → Sources → look for `api-client.js`
4. If 404 error, API endpoint might be wrong

### Registration/Login Not Working
**Error**: Form submission fails or redirects don't work
**Solution**:
1. Check F12 → Network tab for POST request
2. Check response status (should be 200)
3. Verify localStorage has token saved (F12 → Application → localStorage)
4. Check for validation errors in form

---

## 📝 Manual API Testing (Using Swagger)

**Alternative method using Swagger UI:**

1. **Open Swagger**: http://localhost:5000/swagger
2. **Test GET /api/products**
   - Click endpoint
   - Click "Try it out"
   - Click "Execute"
   - See JSON response

3. **Test POST /api/auth/register**
   - Click endpoint
   - Click "Try it out"
   - Fill request body:
     ```json
     {
       "email": "test@example.com",
       "password": "password123",
       "name": "Test User"
     }
     ```
   - Click "Execute"
   - See token response

4. **Test POST /api/auth/login**
   - Similar steps as register
   - Use same credentials

5. **Test POST /api/cart/add**
   - Fill request body:
     ```json
     {
       "productId": 1,
       "quantity": 2
     }
     ```
   - Click "Execute"

---

## ✨ Success Indicators

✅ **When everything works:**
- Products load automatically on cat.html
- Register page successfully creates users
- Login page authenticates users
- Add to cart shows success message
- Browser console has no errors
- Network tab shows successful API calls (200 status)
- localStorage contains user token after login

✅ **You know it's working if:**
1. See product names, prices from API
2. Can register with any email/password
3. Can login with registered credentials
4. Cart notifications appear
5. localStorage shows user data (F12)

---

## 🎯 Next Steps After Testing

Once all tests pass:
1. ✅ Frontend properly integrated with backend
2. ✅ Products loading dynamically
3. ✅ User authentication working
4. Ready to implement:
   - Database persistence
   - JWT token validation
   - Payment gateway
   - Order management

---

**Last Updated**: November 26, 2025
**Status**: Ready for testing
