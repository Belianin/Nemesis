import {get, post} from "./apiUtiles";

export interface Lobby {
    id: string,
    title: string,
    playersCount: number,
    host: string
}

const baseUrl = `${process.env.REACT_APP_API_URL}v1/lobbies`

export function getLobbies(): Promise<Lobby[]> {
    return get(baseUrl).then(x => x.json());
}

export interface CreateLobbyRequest {
    title: string
}

export function createLobby(request: CreateLobbyRequest): Promise<Lobby> {
    return post(baseUrl, request).then(x => x.json());
}