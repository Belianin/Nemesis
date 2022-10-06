import {Lobby} from "../../api/lobbiesApi";
import React from "react";

export const LobbyListItem: React.FC<{lobby: Lobby}> = ({lobby}) =>  {
    return <div>
        <p>{lobby.title}</p>
        <button>Connect</button>
    </div>
}