import { saveItem, getItem, deleteItem } from './LocalFileStorageService';

const TOKEN_KEY = 'jwt';

export const saveToken = async (token: string): Promise<void> => {
    await saveItem(TOKEN_KEY, token);
}

export const getToken = async (): Promise<string | null> => {
    return await getItem(TOKEN_KEY);
}

export const deleteToken = async (): Promise<void> => {
    await deleteItem(TOKEN_KEY);
}
