export const getToken = () => localStorage.getItem('token');
export const getRol = () => localStorage.getItem('rol');
export const logout = () => {
  localStorage.clear();
  window.location.href = '/';
};
