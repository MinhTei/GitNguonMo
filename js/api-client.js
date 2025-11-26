// API Client - Centralized API communication
const API_BASE_URL = 'http://localhost:5000/api';

class APIClient {
  /**
   * Fetch all products
   */
  static async getProducts() {
    try {
      const response = await fetch(`${API_BASE_URL}/products`);
      if (!response.ok) throw new Error('Failed to fetch products');
      return await response.json();
    } catch (error) {
      console.error('Error fetching products:', error);
      return [];
    }
  }

  /**
   * Fetch product by ID
   */
  static async getProductById(id) {
    try {
      const response = await fetch(`${API_BASE_URL}/products/${id}`);
      if (!response.ok) throw new Error('Product not found');
      return await response.json();
    } catch (error) {
      console.error('Error fetching product:', error);
      return null;
    }
  }

  /**
   * Register new user
   */
  static async register(email, password, name = '') {
    try {
      const response = await fetch(`${API_BASE_URL}/auth/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password, name })
      });
      const data = await response.json();
      if (!response.ok) throw new Error(data.message || 'Registration failed');
      
      // Store token and user info
      if (data.token) {
        localStorage.setItem('token', data.token);
        localStorage.setItem('user', JSON.stringify(data.user || { email }));
      }
      return data;
    } catch (error) {
      console.error('Registration error:', error.message);
      throw error;
    }
  }

  /**
   * Login user
   */
  static async login(email, password) {
    try {
      const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
      });
      const data = await response.json();
      if (!response.ok) throw new Error(data.message || 'Login failed');
      
      // Store token and user info
      if (data.token) {
        localStorage.setItem('token', data.token);
        localStorage.setItem('user', JSON.stringify(data.user || { email }));
      }
      return data;
    } catch (error) {
      console.error('Login error:', error.message);
      throw error;
    }
  }

  /**
   * Add item to cart
   */
  static async addToCart(productId, quantity = 1) {
    try {
      const response = await fetch(`${API_BASE_URL}/cart/add`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ productId, quantity })
      });
      const data = await response.json();
      if (!response.ok) throw new Error(data.message || 'Failed to add to cart');
      return data;
    } catch (error) {
      console.error('Cart error:', error.message);
      throw error;
    }
  }

  /**
   * Get current user from localStorage
   */
  static getCurrentUser() {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }

  /**
   * Logout user
   */
  static logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }

  /**
   * Check if user is logged in
   */
  static isLoggedIn() {
    return !!localStorage.getItem('token');
  }
}

// Export for use in modules
if (typeof module !== 'undefined' && module.exports) {
  module.exports = APIClient;
}
