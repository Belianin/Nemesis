import {Lobby} from "../../api/lobbiesApi";
import React from "react";
import { useNavigate } from "react-router";

export const LobbyListItem: React.FC<{lobby: Lobby}> = ({lobby}) =>  {
    
    const navigate = useNavigate();
    
    return <div>
        <p>{lobby.title}</p>
        <p>Host: {lobby.host}</p>
        <p>{lobby.playersCount} / 5 players</p>
        <button onClick={() => navigate(`/lobby/${lobby.id}`)}>Connect</button>
    </div>
}