export const saveItem = async (key: string, item: string): Promise<void> => {
    localStorage.setItem(key, item);
}

export const getItem = async (key: string): Promise<string | null> => {
    return localStorage.getItem(key);
}

export const deleteItem = async (key: string): Promise<void> => {
    localStorage.removeItem(key);
}
