import React, {useEffect, useState} from "react";
import {getLobbies, Lobby} from "../../api/lobbiesApi";
import {LobbyListItem} from "./LobbyListItem";

export const LobbiesList: React.FC = () => {

    const [lobbies, setLobbies] = useState<Lobby[]>();

    useEffect(() => {
        getLobbies().then(setLobbies);
    }, [])

    if (!lobbies)
        return <p>"Loading"</p>

    return <ul>
        {lobbies.map(x => <li key={x.id}><LobbyListItem lobby={x}/></li>)}
    </ul>
}

