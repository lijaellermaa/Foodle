import {environment} from "../../../environments/environment";

const BASE_URL = environment.apiUrl ?? 'https://localhost:5181';
const API_V1_URL = `${BASE_URL}/api/v1`;

const v1Endpoints = {
  products: {
    get: () => `${API_V1_URL}/Products`,
    post: () => `${API_V1_URL}/Products`,
    getById: (id: string) => `${API_V1_URL}/Products/${id}`,
    put: (id: string) => `${API_V1_URL}/Products/${id}`,
    delete: (id: string) => `${API_V1_URL}/Products/${id}`,
  },
  orders: {
    get: () => `${API_V1_URL}/Orders`,
    post: () => `${API_V1_URL}/Orders`,
    getById: (id: string) => `${API_V1_URL}/Orders/${id}`,
    put: (id: string) => `${API_V1_URL}/Orders/${id}`,
    delete: (id: string) => `${API_V1_URL}/Orders/${id}`,
    getByUserIdAndStatus: () => `${API_V1_URL}/Orders/byUserIdAndStatus`,
    postPayById: (id: string) => `${API_V1_URL}/Orders/${id}/Pay`,
  },
  locations: {
    get: () => `${API_V1_URL}/Locations`,
    post: () => `${API_V1_URL}/Locations`,
    getById: (id: string) => `${API_V1_URL}/Locations/${id}`,
    put: (id: string) => `${API_V1_URL}/Locations/${id}`,
    delete: (id: string) => `${API_V1_URL}/Locations/${id}`,
  },
  orderItems: {
    get: () => `${API_V1_URL}/OrderItems`,
    post: () => `${API_V1_URL}/OrderItems`,
    getById: (id: string) => `${API_V1_URL}/OrderItems/${id}`,
    put: (id: string) => `${API_V1_URL}/OrderItems/${id}`,
    delete: (id: string) => `${API_V1_URL}/OrderItems/${id}`,
  },
  prices: {
    get: () => `${API_V1_URL}/Prices`,
    post: () => `${API_V1_URL}/Prices`,
    getById: (id: string) => `${API_V1_URL}/Prices/${id}`,
    put: (id: string) => `${API_V1_URL}/Prices/${id}`,
    delete: (id: string) => `${API_V1_URL}/Prices/${id}`,
  },
  productTypes: {
    get: () => `${API_V1_URL}/ProductTypes`,
    post: () => `${API_V1_URL}/ProductTypes`,
    getById: (id: string) => `${API_V1_URL}/ProductTypes/${id}`,
    put: (id: string) => `${API_V1_URL}/ProductTypes/${id}`,
    delete: (id: string) => `${API_V1_URL}/ProductTypes/${id}`,
  },
  restaurants: {
    get: () => `${API_V1_URL}/Restaurants`,
    post: () => `${API_V1_URL}/Restaurants`,
    getById: (id: string) => `${API_V1_URL}/Restaurants/${id}`,
    put: (id: string) => `${API_V1_URL}/Restaurants/${id}`,
    delete: (id: string) => `${API_V1_URL}/Restaurants/${id}`,
  },
  auth: {
    login: () => `${API_V1_URL}/identity/Account/LogIn`,
    register: () => `${API_V1_URL}/identity/Account/Register`,
    refreshToken: () => `${API_V1_URL}/identity/Account/RefreshToken`,
    logout: () => `${API_V1_URL}/identity/Account/Logout`,
    changeProfile: () => `${API_V1_URL}/identity/Account/ChangeProfile`,
  }
};

export const API = {
  v1: v1Endpoints,
}

export default API;
