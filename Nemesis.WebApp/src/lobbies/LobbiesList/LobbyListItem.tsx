import {Lobby} from "../../api/lobbiesApi";
import React from "react";

export const LobbyListItem: React.FC<{lobby: Lobby}> = ({lobby}) =>  {
    return <div>
        <p>{lobby.title}</p>
        <p>Host: belyanin</p>
        <p>1 / 5 users</p>
        <button>Connect</button>
    </div>
}