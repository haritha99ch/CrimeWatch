import { saveItem, getItem, deleteItem } from './LocalFileStorageService';

const TOKEN_KEY = 'jwt';

export const saveToken = async (token: string): Promise<void> => {
    await saveItem(TOKEN_KEY, token);
}

export const deleteToken = async (): Promise<void> => {
    await deleteItem(TOKEN_KEY);
}

export const getBearerToken = async (): Promise<{ headers: { Authorization: string } } | undefined> => {
    const token = await getToken();
    if(token == null || token === "") return undefined;
    return {
        headers: { Authorization: `Bearer ${token}` }
    };
}

const getToken = async (): Promise<string | null> => {
    return await getItem(TOKEN_KEY);
}
