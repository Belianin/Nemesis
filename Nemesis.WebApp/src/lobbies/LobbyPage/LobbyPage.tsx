import React from "react";
import {useParams} from "react-router";

export const LobbyPage: React.FC = () => {

    const {id} = useParams();

    return <div>Lobby: {id}</div>
}