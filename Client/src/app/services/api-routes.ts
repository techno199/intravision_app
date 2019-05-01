export const deviceId = '1';
const apiDomain = 'https://localhost:5001/api';
// Drinks
export const apiDrinkCrud = apiDomain + '/drinks';
export const apiBuyDrink = apiDomain + '/drinks/buydrink';
// Coins
export const apiCoinCrud = apiDomain + '/coins';
export const apiChangeCoinAmount = apiDomain + '/coins/changeamount';
// Auth
export const apiLogin = apiDomain + '/account/login';
export const apiLogout = apiDomain + '/account/logout';
export const apiIsLoggedIn = apiDomain + '/account/isloggedin';

