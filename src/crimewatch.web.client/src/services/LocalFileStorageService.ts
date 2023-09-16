export const saveItem = (key: string, item: string): void => {
    localStorage.setItem(key, item);
}

export const getItem = <T>(key: string): T | null => {
    return localStorage.getItem(key) as T;
}

export const deleteItem = (key: string): void => {
    localStorage.removeItem(key);
}
