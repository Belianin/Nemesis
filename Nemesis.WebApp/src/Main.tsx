import React from "react";
import {LobbiesList} from "./lobbies/LobbiesList/LobbiesList";
import {NewLobbyForm} from "./lobbies/NewLobbyForm";

export const Main: React.FC = () => {

    return <div>
        <h1>Join one of the lobbies</h1>
        <LobbiesList />
        <hr/>
        <h1>Or create a new one</h1>
        <NewLobbyForm />
    </div>
}