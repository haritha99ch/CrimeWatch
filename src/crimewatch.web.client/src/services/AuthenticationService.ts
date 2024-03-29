import {deleteItem, getItem, saveItem} from './LocalFileStorageService';

const TOKEN_KEY = 'jwt';

export const saveToken = (token: string): void => {
    saveItem(TOKEN_KEY, token);
}

export const deleteToken = (): void => {
    deleteItem(TOKEN_KEY);
}

export const getBearerToken = (): { headers: { Authorization: string } } | undefined => {
    const token = getToken();
    if (token == null || token === "") return undefined;
    return {
        headers: {Authorization: `Bearer ${token}`}
    };
}

export const getToken = (): string | null => {
    return getItem<string>(TOKEN_KEY);
}
